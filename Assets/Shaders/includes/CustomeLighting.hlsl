

#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED


// This is a neat trick to work around a bug in the shader graph when
// enabling shadow keywords. Created by @cyanilux
// https://github.com/Cyanilux/URP_ShaderGraphCustomLighting
// Licensed under the MIT License, Copyright (c) 2020 Cyanilux
#ifndef SHADERGRAPH_PREVIEW
    #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
    #if (SHADERPASS != SHADERPASS_FORWARD)
        #undef REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
    #endif
#endif

struct CustomLightingData{


	float3 positionWS;
	float3 normalWS;

	float3 viewDirectionWS;

	float3 albedo;

	float ambientOcclusion;

	float3 bakedGI;

	float smoothness;

	float4 shadowCoord;

	float3 objColor;

	};

	// Translate a [0, 1] smoothness value to an exponent 
    float GetSmoothnessPower(float rawSmoothness) {
    return exp2(10 * rawSmoothness + 1);
    }

	#ifndef SHADERGRAPH_PREVIEW


	float3 CustomGlobalIllumination(CustomLightingData d){


		float3 indirectDiffuse = d.albedo * d.bakedGI * d.ambientOcclusion;

		return indirectDiffuse;


		}


	float3 CustomLightHandling(CustomLightingData d,Light light){


		float3 radiance = light.color *( light.distanceAttenuation  * light.shadowAttenuation );



		float diffuse =  saturate(dot(d.normalWS, light.direction));

		float diffuse1 = (1.0f - saturate(dot(d.normalWS, light.direction)));

		float3 col = lerp ( d.objColor,d.albedo,diffuse);

	    float specularDot = saturate(dot(d.normalWS, normalize(light.direction + d.viewDirectionWS)));
		float specular = pow(specularDot, GetSmoothnessPower(d.smoothness)) * diffuse ;

		float3 color = d.albedo  *radiance * col;

		return color ;
		}

		#endif


	float3 CalculateCustomLighting(CustomLightingData d){
		
		#ifdef SHADERGRAPH_PREVIEW

		    // In preview, estimate diffuse + specular
		float3 lightDir = float3(0.5, 0.5, 0);

		float intensity = saturate(dot(d.normalWS, lightDir)) +
		   pow(saturate(dot(d.normalWS, normalize(d.viewDirectionWS + lightDir))), GetSmoothnessPower(d.smoothness));

			return d.albedo * intensity;

		#else

		Light  mainLight  = GetMainLight(d.shadowCoord,d.positionWS,1);


		MixRealtimeAndBakedGI(mainLight,d.normalWS,d.bakedGI);

		float3 color = CustomGlobalIllumination(d);

		color += CustomLightHandling(d,mainLight);


		#ifdef _ADDITIONAL_LIGHTS

		uint numAdditionalLight = GetAdditionalLightsCount();

		for (uint lightI = 0;lightI < numAdditionalLight ; lightI++){

			Light light  = GetAdditionalLight(lightI,d.positionWS,1);


			color += CustomLightHandling(d,light);



			}


		#endif


		return color ;

		#endif

		}




		void CalculateCustomLighting_float(float3 Position
			,float3 Normal,float3 ViewDirection
			,float3 Albedo ,float Smoothness ,float AmbientOcclusion,float2 LightmapUV,float3 ObjColor
			, out float3 Color){

			CustomLightingData d;

			d.albedo = Albedo;

			d.ambientOcclusion = AmbientOcclusion;

			d.normalWS = Normal;

			d.positionWS = Position;

			d.viewDirectionWS = ViewDirection;
			d.objColor = ObjColor;
			d.smoothness = Smoothness;


		#ifdef SHADERGRAPH_PREVIEW
    // In preview, there's no shadows or bakedGI
    d.shadowCoord = 0;
  
#else
    // Calculate the main light shadow coord
    // There are two types depending on if cascades are enabled
    float4 positionCS = TransformWorldToHClip(Position);
    #if SHADOWS_SCREEN
        d.shadowCoord = ComputeScreenPos(positionCS);
    #else
        d.shadowCoord = TransformWorldToShadowCoord(Position);
    #endif


			float3 lightmapUV;

			OUTPUT_LIGHTMAP_UV(LightmapUV,unity_LightmapST,lightmapUV);


			float3 vertexSH;

			OUTPUT_SH(Normal,vertexSH);

			d.bakedGI  = SAMPLE_GI(lightmapUV,vertexSH,Normal);

				#endif

			Color = CalculateCustomLighting(d);

			}



#endif
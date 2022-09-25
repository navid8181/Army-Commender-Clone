using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{

    [SerializeField] private float startLifeTime = 2;

    private  ParticleSystem ParticleSystem;


    public ParticleSystem GetParticle() => ParticleSystem;

    private void Awake()
    {
        ParticleSystem = GetComponent<ParticleSystem>();

    
    }

    public float getDuration()
    {
        ParticleSystem[] particles =  GetComponentsInChildren<ParticleSystem>();

        float max = ParticleSystem.main.startLifetime.constant;


        for (int i = 0; i < particles.Length; i++)
        {
            if (max < particles[i].main.startLifetime.constant)
            {
                max = particles[i].main.startLifetime.constant;
            }
        }

        return max;
    }
    private void Update()
    {
        var particle = ParticleSystem.main;

       

        particle.startLifetime = startLifeTime;
        //.startLifetime = startLifeTime;
    }

    public void Stop()
    {
        ParticleSystem.Stop();

    }

    public void Play()
    {


        //if (particleSystem.isPlaying)
        //    particleSystem.Stop();

 

   
            Debug.Log("ParticleSystem.Play(true);");
            ParticleSystem.Play(true);
        


        
    }

    public void SetStartLifeTime(float value) => startLifeTime = value;

    public void SetAvtive(bool value) => ParticleSystem.gameObject.SetActive(value);

}

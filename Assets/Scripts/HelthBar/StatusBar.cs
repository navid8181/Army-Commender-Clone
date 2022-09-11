using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class StatusBar : MonoBehaviour
{

    private Material material;

    private float fill {  get;  set; }

    public float speed = 1;

 [Range(0.0f,1.0f)] public  float tareget = 0;

    public UnityEvent OnStatusBarCompleate;

   
    public void SetFill(float value)
    {
        float clamp01Vlaue = Mathf.Clamp01(value);

        tareget = clamp01Vlaue;


    }
    private void Awake()
    {
      //  OnStatusBarCompleate = new UnityEvent();

        // material = GetComponent<Renderer>().sharedMaterial;

        material = GetComponent<Renderer>().materials[0];

        // tareget = 0;
        fill = tareget;
        material.SetFloat("_removeSegment", fill);
    }

    private void Update()
    {


        fill = Mathf.Lerp(fill, tareget, speed * Time.deltaTime);
        if (tareget >= 1)
            if (tareget - fill < 0.01f) fill = tareget;


        material.SetFloat("_removeSegment", fill);


        if ( fill == 1)
        {


            OnStatusBarCompleate?.Invoke();
        }
    }
}
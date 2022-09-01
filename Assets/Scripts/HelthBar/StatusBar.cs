using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class StatusBar : MonoBehaviour
{

    private Material material;

    private float fill {  get;  set; }

    public float speed = 1;

    float tareget = 0;

    public UnityEvent OnStatusBarCompleate;
    public void SetFill(float value)
    {
        float clamp01Vlaue = Mathf.Clamp01(value);

        tareget = clamp01Vlaue;


    }
    private void Awake()
    {
       

#if UNITY_EDITOR
        material = GetComponent<Renderer>().sharedMaterial;
#else
        material = GetComponent<Renderer>().materials[0];
#endif
    }

    private void Update()
    {


        fill = Mathf.Lerp(fill, tareget, speed * Time.deltaTime);
        if (tareget >= 1)
            if (tareget - fill < 0.01f) fill = tareget;


        material.SetFloat("_removeSegment", fill);


        if (tareget == fill)
        {
            OnStatusBarCompleate?.Invoke();
        }
    }
}
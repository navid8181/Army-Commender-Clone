using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HelthBarController : MonoBehaviour
{

    private Material material;

    public float fill { get; private set; }

    public float speed = 1;

    float tareget = 0;

    public void SetFill(float value)
    {
        float clamp01Vlaue = Mathf.Clamp01(value);

        tareget = clamp01Vlaue;


    }
    private void Awake()
    {
        material = GetComponent<Renderer>().materials[0];

    }

    private void Update()
    {


        fill = Mathf.Lerp(fill, tareget, speed * Time.deltaTime);
        if (tareget >= 1)
            if (tareget - fill < 0.01f) fill = tareget;


        material.SetFloat("_removeSegment", fill);
    }
}
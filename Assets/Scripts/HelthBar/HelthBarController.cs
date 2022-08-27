using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HelthBarController : MonoBehaviour
{

    private Material material;

    [Range(0.0f, 1.0f)] public float fill = 1;
    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        
    }

    private void Update()
    {
        material.SetFloat("_removeSegment",fill);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScaledObject : MonoBehaviour
{

    private bool isSclaed = false;

    public float speed = 5;
    private void OnEnable()
    {
        isSclaed = true;
    }

    private void OnDisable()
    {
        isSclaed = false;

        transform.localScale = Vector3.zero;
        
    }

    Vector3 deffultScale;
    private void Awake()
    {
        deffultScale = transform.localScale;
    }


    private void Update()
    {
        if (!isSclaed) return;

        transform.localScale = Vector3.Lerp(transform.localScale, deffultScale, speed * Time.deltaTime);

        if (deffultScale.sqrMagnitude - transform.localScale.sqrMagnitude  <= 0.01f)
        {
            transform.localScale = deffultScale;
            isSclaed = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Catapult : MonoBehaviour
{

    public Transform target;

    public PointCurveHandler curveHandler;

    public JoyStick JoyStick;

    public TextMeshPro txt_Status;

    public float speed = 3;

    public float t;
    public bool CanMove { get; set; } = false;


    private DateTime timeToUse;

    public bool refreah;
    private void Awake()
    {
        timeToUse = DateTime.UtcNow;
    }
    private void Update()
    {

        if (refreah)
        {
            refreah = false;

            timeToUse = DateTime.UtcNow;

           timeToUse =  timeToUse.AddMinutes(30D);
        }

        if (timeToUse <= DateTime.UtcNow)
        {
            txt_Status.text = "Use";
        }
        else
        {

            StartCoroutine(ShowTime());
        }


        if (!CanMove) return;

        Vector3 targetRot = target.position;

        Vector3 dire = targetRot - transform.position;

        dire.y = 0;


        dire.Normalize();

        transform.rotation = Quaternion.LookRotation(dire,Vector3.up);


        Vector3 input = JoyStick.getRawInput();

        input.Normalize();


        target.transform.Translate(input * speed * Time.deltaTime, Space.World);

    }


    IEnumerator ShowTime()
    {
        yield return new WaitForSeconds(1);

        var diff = (timeToUse - DateTime.UtcNow);

        txt_Status.text = diff.Hours + ":" + diff.Minutes + " : " + diff.Seconds;
    }
    private void OnDrawGizmos()
    {
        if(curveHandler != null)
        Gizmos.DrawWireCube(curveHandler.getPoint(t), Vector3.one);
    }
}

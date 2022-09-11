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

    public bool CanUse { get; private set; } = false;
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

            AddTime(30);
        }

        if (timeToUse <= DateTime.UtcNow)
        {
            txt_Status.text = "Use";
            CanUse = true;
        }
        else
        {
            CanUse = false;
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



    public void AddTime(double munite)
    {
        timeToUse = DateTime.UtcNow;

        timeToUse = timeToUse.AddMinutes(munite);
    }

    IEnumerator ShowTime()
    {


        var diff = (timeToUse - DateTime.UtcNow);

        txt_Status.text = diff.Hours.ToString("00") + " : " + diff.Minutes.ToString("00") + " : " + diff.Seconds.ToString("00");

        yield return new WaitForSeconds(1);
    }
    private void OnDrawGizmos()
    {
        if(curveHandler != null)
        Gizmos.DrawWireCube(curveHandler.getPoint(t), Vector3.one);
    }
}

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

    public float t { get; set; }

    public Bomb bomb;

    public bool CanUse { get;  set; } = false;
    public bool CanMove { get; set; } = false;


    private DateTime timeToUse;

    public bool refreah { get; set; }

    private bool startExplosion = false;

    private Animator animator;

    private void Awake()
    {

        animator = GetComponent<Animator>();

        timeToUse = DateTime.UtcNow;
    }
    private void Update()
    {

        if (refreah)
        {
            refreah = false;

            AddTime(0.5f);
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



        HandleAttack();


        if (!CanMove) return;

        Vector3 targetRot = target.position;

        Vector3 dire = targetRot - transform.position;

        dire.y = 0;


        dire.Normalize();

        transform.rotation = Quaternion.LookRotation(dire,Vector3.up);


        Vector3 input = new Vector3(JoyStick.getRawInput().x, 0, JoyStick.getRawInput().y);

        input.Normalize();


        target.transform.Translate(input * speed * Time.deltaTime, Space.World);

    }


   private void HandleAttack()
    {
        if (!startExplosion) return;

        bomb.gameObject.SetActive(true);
        bomb.transform.position = curveHandler.getPoint(t);
        animator.SetBool("Attack", true);

        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Attack"))
        {
            if (stateInfo.normalizedTime >= 0.8f)
            {


                t = Mathf.Lerp(t, 0, 2 * Time.deltaTime);


                if (t<= 0.1f)
                {
                    bomb.Explosion();
                    animator.SetBool("Attack", false);
                    startExplosion = false;
                    return;

                }


            }
        }
    


    }

    public void Attack()
    {
        curveHandler.GetComponent<LineRenderer>().enabled = false;
        startExplosion = true;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    private new Rigidbody rigidbody;
    private Animator anim;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }


    private void FixedUpdate()
    {

      
    }


    public void SetBoolAnimiton(string name,bool value)
    {
        anim.SetBool(name, value);
    }
    public void SetFloatAnimiton(string name, float value)
    {
        anim.SetFloat(name, value);
    }


    public void Move(Vector3 dire,float speed,float rotationSpeed)
    {
        rigidbody.MovePosition(transform.position + dire * speed * Time.fixedDeltaTime);
        Quaternion quat = transform.localRotation;
        if (dire.magnitude != 0)
            quat = Quaternion.Slerp(quat, Quaternion.LookRotation(dire * rotationSpeed * Time.fixedDeltaTime, Vector3.up), rotationSpeed * Time.fixedDeltaTime);


        rigidbody.MoveRotation(quat);
    }



}

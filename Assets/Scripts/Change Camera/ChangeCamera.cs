using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{



    public static void Change(Camera from,Camera to)
    {

        from.gameObject.SetActive(false);

        to.gameObject.SetActive(true); 
    }


}

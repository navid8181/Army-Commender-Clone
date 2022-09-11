using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BansheeGz.BGSpline.Curve;
using BansheeGz.BGSpline.Components;

public class PointCurveHandler : MonoBehaviour
{


    public  BGCurve bgCurve;

    private BGCcMath BGCcMath;

    public Transform firtstPoint, secondPoint;

    private void Awake()
    {
        BGCcMath = bgCurve.GetComponent<BGCcMath>();
    }

    private void Update()
    {
        bgCurve.Points[1].PositionWorld = firtstPoint.position;
        bgCurve.Points[0].PositionWorld = secondPoint.position;

        
 
    }

    public Vector3 getPoint(float t)
    {

        if (BGCcMath == null) BGCcMath = bgCurve.GetComponent<BGCcMath>();

        float distance = BGCcMath.GetDistance() * t;

        return BGCcMath.CalcPositionByDistance(distance);
    }
}

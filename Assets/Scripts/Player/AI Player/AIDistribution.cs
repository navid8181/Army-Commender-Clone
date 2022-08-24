using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDistribution : MonoBehaviour
{

    

    public Transform taregt;

    [SerializeField] private int with = 4;
    [SerializeField] private Vector3 offfset = Vector3.one;


    public AiBase[] aiBase;

    public int getWith { get { return with; } }
    public Vector3 getOfffset { get { return offfset; } }

    public int getLenght { get { return aiBase.Length; } }

    private void FixedUpdate()
    {
        Utility. RectangleDistribute(aiBase,taregt,with,offfset);
    }

    private void CircleDistribute( IDistributable[] aiBases)
    {
        for (int i = 0; i < aiBases.Length; i++)
        {
            float angle = (i / (float)aiBases.Length) * 360;

            Vector3 dire = taregt.position + Utility.angleToDireXZ(angle, 2);

            // dire.y = 0;
            dire += Vector3.up / 2f;


            aiBases[i].SetTraget(dire);
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDistribution : MonoBehaviour
{

    

    public Transform taregt;

    [SerializeField] private int with = 4;
    [SerializeField] private Vector3 offfset = Vector3.one;


    private List<AiBase> aiBase;

    public int getWith { get { return with; } }
    public Vector3 getOfffset { get { return offfset; } }

    public int getLenght { get { return aiBase.Count; } }



    private void Awake()
    {
        aiBase = new List<AiBase>();
    }

    private void FixedUpdate()
    {
       
    }


    public int getIndex(AiBase ai)
    {
       return aiBase.IndexOf(ai);
    }
    public void UpdateIndex(int lastRemoveIndex)
    {
        for (int i = lastRemoveIndex; i < aiBase.Count; i++)
        {
            aiBase[i].SetCurrentIndex(i);
        }
    }

    public void RectangleDistribute(int i)
    {
         Utility. RectangleDistribute(aiBase.ToArray(),taregt,with,offfset,i);
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

    public void SetAi( AiBase ai)
    {
        ai.SetCurrentIndex(aiBase.Count);
        aiBase.Add(ai);

 
    }

    public void RemoveAi( AiBase ai)
    {
        aiBase.Remove(ai);
        UpdateIndex(getIndex(ai));
    }

}

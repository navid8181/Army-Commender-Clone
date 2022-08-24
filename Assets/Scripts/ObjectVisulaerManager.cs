using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectVisulaerManager : MonoBehaviour
{
    public List<ObjectRectangleVisualzer> objectRectangleVisualzers;

    public AIDistribution aiDistribution;

    public ObjectRectangleVisualzer prefab;


    private void Awake()
    {
        objectRectangleVisualzers = new List<ObjectRectangleVisualzer>();
        ApplyVisualer();
    }

    public void ApplyVisualer()
    {
        objectRectangleVisualzers.Clear();
        for (int i = 0; i < aiDistribution.aiBase.Length; i++)
        {
            ObjectRectangleVisualzer obV = Instantiate(prefab);

            objectRectangleVisualzers.Add(obV);
        }

       
    }

    private void Update()
    {
        Utility.RectangleDistribute(objectRectangleVisualzers.ToArray(), transform, aiDistribution.getWith, aiDistribution.getOfffset);
    }
}

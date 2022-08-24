using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Pool Object",menuName = "Pool/create Pool Object")]
public class PoolObject : ScriptableObject 
{
    public string Name;
    public GameObject prefab;
    public int totalInstance = 10;

  
}

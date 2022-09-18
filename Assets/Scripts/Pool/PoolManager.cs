using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[System.Serializable]
public class PoolObjects
{
    private string name;
    private Queue<GameObject> objects;


    public PoolObjects(string name)
    {
        objects = new Queue<GameObject>();
        this.name = name;
    }
    public string getName() => this.name;

    public void AddPool(GameObject pool) => this.objects.Enqueue(pool);

    public GameObject removePool() => this.objects.Count > 0 ? this.objects.Dequeue() : null;


}



public class PoolManager : MonoBehaviour
{


    #region static

    public  const string goldCoin = "gold coin";
    public  const string ironCoin = "iron coin";

    public  const string sowrdManAI = "sword man AI";

    public const string objectVisualer = "Visualer";

    public const string weaponAmmo = "Weapon Ammo";
    #endregion


    public PoolObject[] poolObjectsData;

    private PoolObjects[] poolObjects;

    private Dictionary<string, int> poolObjectsIndex;

    private void Awake()
    {

        poolObjects = new PoolObjects[poolObjectsData.Length];
        poolObjectsIndex = new Dictionary<string, int>();

        for (int i = 0; i < poolObjectsData.Length; i++)
        {
         
            poolObjectsIndex.Add(poolObjectsData[i].Name, i);

            poolObjects[i] = new PoolObjects(poolObjectsData[i].Name);
            for (int j = 0; j < poolObjectsData[i].totalInstance; j++)
            {
                GameObject poolClone = Instantiate(poolObjectsData[i].prefab);

                poolClone.name = poolObjectsData[i].Name;

        


                poolClone.SetActive(false);

                poolObjects[i].AddPool(poolClone);

            }



        }
    }





    public GameObject requestPool(string name)
    {

        if (name == PoolManager.objectVisualer)
        {

        }
    

        int index = poolObjectsIndex.GetValueOrDefault(name,-1); 

        GameObject current = poolObjects[index].removePool();

        if (current != null) current.SetActive(true);

        return current;
    }


    public void BackToPool(GameObject pool)
    {
        int index = poolObjectsIndex.GetValueOrDefault(pool.name, -1);
        if (index == -1) return ;

        pool.SetActive(false);

        poolObjects[index].AddPool(pool);
    }
}

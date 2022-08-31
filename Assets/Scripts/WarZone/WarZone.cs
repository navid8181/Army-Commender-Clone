using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public class WarZone : MonoBehaviour
{

    public List<EnemyBase> enemyBases = new List<EnemyBase>();

    private BoxCollider boxCollider;
    private Rigidbody rigid;
    public List<Transform> players;

    private void Awake()
    {
        players = new List<Transform>();
      
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;

        rigid = GetComponent<Rigidbody>();

        rigid.isKinematic = true;
    }

    private void Update()
    {
        for (int i = 0; i < enemyBases.Count; i++)
        {
            if (enemyBases[i].Health <= 0)
            {
                enemyBases.RemoveAt(i);
            }

            if (players.Count == 0)
            {
                if (i< enemyBases.Count)
                enemyBases[i].target = null;
            }
        }

        for (int i = 0; i < players.Count; i++)
        {
            IDamageable aiBase = players[i].GetComponent<IDamageable>();

            if (aiBase!= null && aiBase.Health <= 0)
            {
                players.RemoveAt(i);
            }

        }

        if (players.Count > enemyBases.Count)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (enemyBases.Count == 0) break;

                int enemyIndex = i% enemyBases.Count;

               AiBase aiBace = players[i].GetComponent<AiBase>();

                if (aiBace != null)
                    aiBace.SetAttackTarget(enemyBases[enemyIndex]);
                else
                    enemyBases[enemyIndex].target = players[i];
            }
        }
        else if (players.Count < enemyBases.Count)
        {
            for (int i = 0; i < enemyBases.Count; i++)
            {
                if (players.Count == 0) break;

                int playerIndex  = i% players.Count;

                AiBase aibase = players[playerIndex].GetComponent<AiBase>();

               if (aibase != null )
                    aibase.SetAttackTarget(enemyBases[i]);
               else
                    enemyBases[i].target = players[playerIndex];

            }
        }
        else
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (enemyBases.Count == 0) break;

              

                AiBase aiBace = players[i].GetComponent<AiBase>();

                if (aiBace != null)
                    aiBace.SetAttackTarget(enemyBases[i]);
                else
                    enemyBases[i].target = players[i];
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AiBase aiBase = other.GetComponent<AiBase>();

        if (aiBase != null) players.Add(other.transform);
        else
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
                players.Add(player.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (players.Contains(other.transform)) { players.Remove(other.transform); }
    }

}

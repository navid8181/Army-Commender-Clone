using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapone Object", menuName = "Weapone/create new Weapone Object")]
public class Weapone : ScriptableObject
{

    public float maxDistanceToAttack = 1.5f;

    public float timeToAttack = 0.6f;

    public float damge = 25;


}

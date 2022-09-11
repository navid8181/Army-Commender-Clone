using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{

    public float timeToExplosion = 2;

    public float radiusExplosion = 3;

    public float damage = 200;

    public ParticleSystemController particleSystemController;

    public LayerMask enemyLayerMask;


    private bool init;


    private Rigidbody rigidBody;

    private Timer timer;

    private void Awake()
    {
        timer = new Timer(timeToExplosion);
        rigidBody = GetComponent<Rigidbody>();

        rigidBody.isKinematic = true;

    }

    private void Start()
    {
 
    }


    private void Update()
    {
        if (init)
        {
            rigidBody.isKinematic = false;

            timer.Init(() =>
            {
                init = false;
                particleSystemController.Play();

                Collider[] col = Physics.OverlapSphere(transform.position, radiusExplosion, enemyLayerMask);

                for (int i = 0; i < col.Length; i++)
                {
                    col[i].GetComponent<IDamageable>().ApplyDamage(damage);
                }


                StartCoroutine(DisableBomb());
            });
        }
    }


    IEnumerator DisableBomb()
    {
        yield return new WaitForSeconds(particleSystemController.getDuration());

        rigidBody.isKinematic = true;
        gameObject.SetActive(false);
    }

    public void Explosion() => init = true;



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusExplosion);
    }


}


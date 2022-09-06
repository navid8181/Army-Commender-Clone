using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{

    [SerializeField] private float startLifeTime = 2;

    private new ParticleSystem particleSystem;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }


    private void Update()
    {
        var particle = particleSystem.main;

        particle.startLifetime = startLifeTime;
        //.startLifetime = startLifeTime;
    }

    public void Play()
    {
        particleSystem.Play();
    }

    public void SetStartLifeTime(float value) => startLifeTime = value;

    public void SetAvtive(bool value) => particleSystem.gameObject.SetActive(value);

}

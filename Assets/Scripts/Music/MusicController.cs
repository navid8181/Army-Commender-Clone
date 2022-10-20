using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{

    public AudioClip[] audioClips;
 
    private AudioSource musicSource;



    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.playOnAwake = false;

        musicSource.loop = false;

        musicSource.spatialBlend = 1;
    }

    [ContextMenu("play Audio")]
    public void Play()
    {
        musicSource.clip = audioClips[Random.Range(0, audioClips.Length)];

        musicSource.Play();
    }


}

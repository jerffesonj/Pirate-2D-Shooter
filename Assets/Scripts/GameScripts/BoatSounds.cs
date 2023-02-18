using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class with the boat souns
public class BoatSounds : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] AudioClip shot;

    [SerializeField] AudioClip chaserExplosion;
    [SerializeField] AudioClip boatDeath;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    public void PlayShotSound()
    {
       audioSource.PlayOneShot(shot, 0.3f);
    }
    public void PlayChaserAttackSound()
    {
        audioSource.PlayOneShot(chaserExplosion, 0.7f);
    }
    public void PlayBoatDeathSound()
    {
        audioSource.PlayOneShot(boatDeath, 0.7f);
    }
}

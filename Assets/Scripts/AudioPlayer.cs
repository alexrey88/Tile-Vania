using System;
using UnityEngine;

public class AudioPlayer : Singleton<AudioPlayer>
{
    [SerializeField] AudioClip bouncingClip;
    [SerializeField][Range(0f, 1f)] float bouncingVolume;

    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0f, 1f)] float shootingVolume;

    [SerializeField] AudioClip enemyDeadClip;
    [SerializeField][Range(0f, 1f)] float enemyDeadVolume;

    [SerializeField] AudioClip coinPickupClip;
    [SerializeField][Range(0f, 1f)] float coinPickupVolume;

    void PlayClip(AudioClip audioClip, float volume)
    {
        if (audioClip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(audioClip, cameraPos, volume);
        }
        else
        {
            Debug.LogWarning("Trying to play a null AudioClip.");
        }
    }

    public void PlayBouncingClip()
    {
        PlayClip(bouncingClip, bouncingVolume);
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayEnemyDeadClip()
    {
        PlayClip(enemyDeadClip, enemyDeadVolume);
    }

    public void PlayCoinPickupClip()
    {
        PlayClip(coinPickupClip, coinPickupVolume);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class DragonsAudioManager : MonoBehaviour
{
    [SerializeField] AudioClip flapWingsSound;
    [SerializeField] AudioClip[] roarSounds;
    [SerializeField] AudioClip longRoarSound;
    [SerializeField] AudioClip fistSound;
    [SerializeField] AudioClip damageSound;
    [SerializeField] AudioClip tailAttackSound;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayWingFlapAudio(float delay)
    {
        if (audioSource.isPlaying == false)
        {
            audioSource.clip = flapWingsSound;
            // play audio after a sort delay
            audioSource.PlayDelayed(delay);
        }
    }

    public void PlayRoarAudio()
    {       
        // pick a random roar sound from the array
        int n = Random.Range(0, roarSounds.Length);
        audioSource.clip = roarSounds[n];
        // play audio after a sort delay
        audioSource.PlayDelayed(0.3f);
    }

    public void PlayLongRoarAudio()
    {
        audioSource.clip = longRoarSound;
        // play audio after a sort delay
        audioSource.PlayDelayed(0.6f);
    }

    public void PlayFistAudio()
    {
        audioSource.clip = fistSound;
        // play audio after a sort delay
        audioSource.PlayDelayed(0.8f);
    }

    public void PlayDamageAudio()
    {
        audioSource.clip = damageSound;
        // play audio after a sort delay
        audioSource.PlayDelayed(0.5f);
    }

    public void PlayTailAttackAudio()
    {
        audioSource.clip = tailAttackSound;
        // play audio after a sort delay
        audioSource.Play();
    }
}

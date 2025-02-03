using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunderstorm : MonoBehaviour
{
    [SerializeField] AudioClip natureSounds;

    [SerializeField] GameObject thunders;
    [SerializeField] Light sceneLight;
    [SerializeField] AudioClip[] thunderSounds;

    AudioSource audioSource;

    void Start()
    {
        // get AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {        
        // if scene is dark enough
        if (sceneLight.intensity < 0.6)
        {
            thunders.SetActive(true);
            // if nothing is playing
            //if (audioSource.isPlaying == false)
            if (audioSource.clip == natureSounds || audioSource.isPlaying == false)
            {
                // pick a clip randomly and play it
                int n = Random.Range(0, thunderSounds.Length);
                audioSource.clip = thunderSounds[n];
                audioSource.PlayOneShot(audioSource.clip);
            }
        }
        // if scene is light enough
        if (sceneLight.intensity > 0.6)
        {
            thunders.SetActive(false);
            if (audioSource.isPlaying == false)
            {
                audioSource.clip = natureSounds;
                audioSource.Play();
            }
            
        }
    }
}

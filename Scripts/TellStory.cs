using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TellStory : MonoBehaviour
{
    AudioSource audioSource;
    bool alreadyPlayed;
    
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && alreadyPlayed == false)
        {
            audioSource.Play();
            alreadyPlayed = true;
        }
    }
}

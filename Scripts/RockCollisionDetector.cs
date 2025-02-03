using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollisionDetector : MonoBehaviour
{
    [SerializeField] GameObject rockHandler;

    Rigidbody rb;
    AudioSource audioSource;

    // to see through throwRocks script if a rock has hit the ground
    public bool hitTheGround = false;
    bool alreadyPlayed = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitTheGround = true;
        if (alreadyPlayed == false)
        {
            audioSource.Play();
            alreadyPlayed = true;
        }
        
    }
    
}

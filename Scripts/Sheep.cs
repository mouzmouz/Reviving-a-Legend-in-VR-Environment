using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    private Animator anim;
    string animName;
    AnimatorClipInfo[] currenAnimInfo;

    AudioSource audioSource;

    void Start()
    {
        anim = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        currenAnimInfo = this.anim.GetCurrentAnimatorClipInfo(0);
        animName = currenAnimInfo[0].clip.name;

        int randomNumber = Random.Range(-1, 6);
        anim.SetInteger("randomNumber", randomNumber);

        if (animName == "IdleBaaa")
        {
            audioSource.Play();
        }

    }
}

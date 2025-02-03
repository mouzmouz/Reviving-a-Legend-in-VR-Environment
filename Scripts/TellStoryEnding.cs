using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TellStoryEnding : MonoBehaviour
{
    AudioSource audioSource;
    bool alreadyPlayed = false;
    
    Animator tymfiAnim;
    AnimatorClipInfo[] currentTymfiAnimInfo;
    string tymfiAnimName;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.root.tag);
        if (other.transform.root.tag == "Tymfi Dragon") 
        {
            tymfiAnim = GameObject.Find("Tymfi Dragon").GetComponent<Animator>();
            currentTymfiAnimInfo = this.tymfiAnim.GetCurrentAnimatorClipInfo(0);
            tymfiAnimName = currentTymfiAnimInfo[0].clip.name;

            if (tymfiAnimName == "WD_Fly_Forward" && tymfiAnim.GetBool("GoHome") == true && alreadyPlayed == false)
            {
                alreadyPlayed = true;
                audioSource.Play();
            }
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TellFightStory : MonoBehaviour
{
    [SerializeField] GameObject smolikasDragon;

    [SerializeField] GameObject tymfiDragon;
    Animator tymfiAnim;
    AnimatorClipInfo[] currentTymfiAnimInfo;
    string tymfiAnimName;

    [SerializeField] GameObject firstRock;

    AudioSource audioSource;
    [SerializeField] AudioClip[] clips;
    bool alreadyPlayed_0 = false;
    bool alreadyPlayed_1 = false;
    bool alreadyPlayed_2 = false;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && alreadyPlayed_0 == false)
        {
            audioSource.clip = clips[0];
            audioSource.Play();
            alreadyPlayed_0 = true;
        }
    }

    void Update()
    {
        if (tymfiDragon.activeSelf == true)
        {
            tymfiAnim = tymfiDragon.GetComponent<Animator>();
            currentTymfiAnimInfo = this.tymfiAnim.GetCurrentAnimatorClipInfo(0);
            tymfiAnimName = currentTymfiAnimInfo[0].clip.name;

            // when tymfi dragon goes to unroot the first try tell whats going on
            if (alreadyPlayed_0 == true && alreadyPlayed_1 == false && tymfiAnimName == "WD_Fly_Forward")
            {
                audioSource.clip = clips[1];
                audioSource.Play();
                alreadyPlayed_1 = true;
            }

            if (alreadyPlayed_1 == true && alreadyPlayed_2 == false && firstRock.activeSelf == true)
            {
                Debug.Log("AA");
                audioSource.clip = clips[2];
                audioSource.PlayDelayed(4f);
                alreadyPlayed_2 = true;
            }

        }
    }

}

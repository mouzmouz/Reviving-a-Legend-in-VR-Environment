using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmolikasDragonAnimationManager : MonoBehaviour
{
    public Vector3 smolikasStartLocation;

    Animator anim;
    AnimatorClipInfo[] currentAnimInfo;
    string animName;

    Rigidbody rb;

    [SerializeField] GameObject landLocation;
    //[SerializeField] GameObject otherDragon;
    float changeDirectionSpeed = 0.1f;

    bool alreadyLanded;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        smolikasStartLocation = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Land Area"&& alreadyLanded == false)
        {
            alreadyLanded = true;
            anim.SetBool("LookAtOtherDragon", true);
        }
        if (other.tag == "Land" && anim.GetBool("LookAtOtherDragon") == true)
        {
            anim.SetBool("LookAtOtherDragon", false);
            anim.SetBool("Land", true);
        }
    }

    void Update()
    {
        currentAnimInfo = this.anim.GetCurrentAnimatorClipInfo(0);
        animName = currentAnimInfo[0].clip.name;

        if (animName.Contains("Fly") || animName == "WD_TakeOff")
        {
            rb.useGravity = false;
        }
        else
        {
            rb.useGravity = true;
            rb.AddForce(Physics.gravity * rb.mass);
        }

        if (animName == "WD_Fly_Forward" && anim.GetBool("LookAtOtherDragon") == false && anim.GetBool("GoHome") == false)
        {
            // change direction
            Vector3 targetLandDirection = landLocation.transform.position - transform.position;
            Quaternion newDirection = Quaternion.LookRotation(targetLandDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, changeDirectionSpeed);

            //move twards land location
            transform.position = Vector3.MoveTowards(transform.position, landLocation.transform.position, 5f);
        }

        if ((anim.GetBool("LookAtOtherDragon") == true && animName == "WD_Fly_Stand") || anim.GetBool("Land") == true)
        {
            //fall to the ground
            rb.useGravity = true;
            rb.AddForce(Physics.gravity * rb.mass);
        }
                
        
    }
}

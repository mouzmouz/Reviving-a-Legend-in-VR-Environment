using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonsFight : MonoBehaviour
{
    [SerializeField] GameObject tymfiDragon;
    [SerializeField] GameObject smolikasDragon;

    [SerializeField] GameObject tymfiDragonSleepPlace;

    Animator smolikasAnim;
    AnimatorClipInfo[] currentSmolikasAnimInfo;
    string smolikasAnimName;

    Animator tymfiAnim;
    AnimatorClipInfo[] currentTymfiAnimInfo;
    string tymfiAnimName;

    Rigidbody smolikasRb;
    Rigidbody tymfiRb;
    
    float changeDirectionSpeed = 0.1f;
    // to calcualte seconds later
    float timer = 0.0f;

    void Start()
    {
        smolikasAnim = smolikasDragon.GetComponent<Animator>();
        tymfiAnim = tymfiDragon.GetComponent<Animator>();

        smolikasRb = smolikasDragon.GetComponent<Rigidbody>();
        tymfiRb = tymfiDragon.GetComponent<Rigidbody>();
    }

    void Update()
    {
        currentSmolikasAnimInfo = this.smolikasAnim.GetCurrentAnimatorClipInfo(0);
        smolikasAnimName = currentSmolikasAnimInfo[0].clip.name;

        currentTymfiAnimInfo = this.tymfiAnim.GetCurrentAnimatorClipInfo(0);
        tymfiAnimName = currentTymfiAnimInfo[0].clip.name;

        // right before Smolikas Dragon lands, he starts looking at Tymfi Dragon and then lands
        if (smolikasAnim.GetBool("LookAtOtherDragon") == true)
        {
            LookAtOtherDragon(smolikasDragon, tymfiDragon);
           
        }

        // as Tymfi Dragon Lands, she starts looking at Smolikas dragon
        if (tymfiAnim.GetBool("LookAtOtherDragon") == true)
        {
            LookAtOtherDragon(tymfiDragon, smolikasDragon);
        }
        
        if (smolikasAnimName == "WD_Jump_In_Place")
        {
            tymfiAnim.SetBool("LookAtOtherDragon", false);
        }
        if (smolikasAnimName == "WD_Attack_Tail_Left")
        {
            tymfiAnim.SetBool("GotHit", true);
        }

        if (smolikasAnimName == "WD_TakeOff")
        {
            // stop looking at Tymfi dragon
            smolikasAnim.SetBool("LookAtOtherDragon", false);
            // no longer pushing the dragon to the ground
            smolikasAnim.SetBool("Land", false);

            // Tymfi dragon stops looking at Smolikas dragon
            //tymfiAnim.SetBool("LookAtOtherDragon", false);
        }

        // Smolikas dragon gets higher while he gets away
        if (smolikasAnimName == "WD_Fly_Stand")
        {
            tymfiAnim.SetBool("SmolikasGoesAway", true);
            smolikasDragon.transform.position += smolikasDragon.transform.up * Time.deltaTime;
        }

        // Tymfi dragon gets higher while watching Smolikas dragon go away
        if (tymfiAnimName == "WD_Fly_Stand" && tymfiAnim.GetBool("SmolikasGoesAway") == true)
        {
            tymfiDragon.transform.position += smolikasDragon.transform.up * Time.deltaTime;
            //LookAtOtherDragon(tymfiDragon, smolikasDragon);
            tymfiAnim.SetBool("LookAtOtherDragon", true);
        }

        // time for Smolikas Dragon to go home
        if (smolikasAnimName == "WD_Fly_Left")
        {
            float seconds = DoCircle(smolikasDragon, 0.8f, 100);
            if (seconds == 10)
            {
                smolikasAnim.SetBool("GoHome", true);
            }
        }
        
        if (smolikasAnim.GetBool("GoHome") == true)
        {
            smolikasAnim.SetBool("LookAtOtherDragon", false);
            // go back
            Vector3 smolikasHome = smolikasDragon.GetComponent<SmolikasDragonAnimationManager>().smolikasStartLocation;

            // change direction
            Vector3 newTargetDirection = smolikasHome - smolikasDragon.transform.position;
            Quaternion newDirection = Quaternion.LookRotation(newTargetDirection);
            smolikasDragon.transform.rotation = Quaternion.Slerp(smolikasDragon.transform.rotation, newDirection, changeDirectionSpeed);

            smolikasDragon.transform.position = Vector3.MoveTowards(smolikasDragon.transform.position, smolikasHome, 2.0f);

            if (tymfiAnim.GetBool("GoToSleep") == true)
            {
                // Smolikas dragon is no longer needed in the scene
                Destroy(smolikasDragon);
            }
        }

        if (smolikasAnimName == "WD_Fly_Forward" && smolikasAnim.GetBool("GoHome") == true && tymfiAnim.GetBool("GoToSleep") == false)
        {
            // Tymfi dragon goes home too
            tymfiAnim.SetBool("GoHome", true);
            tymfiAnim.SetBool("LookAtOtherDragon", false);

            //Vector3 tymfiHome = tymfiDragon.GetComponent<TymfiDragonAnimationManager>().tymfiStartLocation;
            Vector3 tymfiHome = tymfiDragonSleepPlace.transform.position;

            // change direction
            Vector3 newTargetDirection = tymfiHome - tymfiDragon.transform.position;
            Quaternion newDirection = Quaternion.LookRotation(newTargetDirection);
            tymfiDragon.transform.rotation = Quaternion.Slerp(tymfiDragon.transform.rotation, newDirection, changeDirectionSpeed);

            tymfiDragon.transform.position = Vector3.MoveTowards(tymfiDragon.transform.position, tymfiHome, 0.5f);
      
        }
    }

    void LookAtOtherDragon(GameObject dragon, GameObject otherDragon)
    {
        Vector3 targetDragonDirection = otherDragon.transform.position - dragon.transform.position;
        Quaternion newDirection = Quaternion.LookRotation(targetDragonDirection);
        dragon.transform.rotation = Quaternion.Slerp(dragon.transform.rotation, newDirection, changeDirectionSpeed);
    }

    float DoCircle(GameObject dragon,float rotation, float speed)
    {
        timer += Time.deltaTime;
        int seconds = (int)(timer % 60);

        // find the current rotation of the eagle
        float rot_y = transform.eulerAngles.y;
        // rotate it a little
        rot_y -= rotation;
        dragon.transform.eulerAngles = new Vector3(0f, rot_y, 0f);
        // move the eagle to the direcation it is facing
        dragon.transform.position += transform.forward * Time.deltaTime * speed;
        return seconds;
    }


}

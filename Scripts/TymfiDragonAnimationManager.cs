using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TymfiDragonAnimationManager : MonoBehaviour
{
    [SerializeField] GameObject otherDragon;
    Animator otherDragonAnim;
    AnimatorClipInfo[] currentOtherDragonAnimInfo;
    string otherDragonAnimName;

    [SerializeField] GameObject[] trees_arr;
    GameObject targetTree;
    public int treesUnrooted;
    //to manage the unrooted trees from other scripts
    public static TymfiDragonAnimationManager instace;

    Rigidbody rb;

    Animator anim;
    AnimatorClipInfo[] currentAnimInfo;
    string animName;

    public Vector3 tymfiStartLocation;
    [SerializeField] GameObject landLocation;

    // the speed that the dragon turns around to get more trees 
    float changeDirectionSpeed = 0.1f;
    // to calcualte seconds later
    float timer = 0.0f;

    bool alreadyLanded = false;

    void Start()
    {
        //to manage the unrooted trees from other scripts
        instace = this;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        otherDragonAnim = otherDragon.GetComponent<Animator>();

        // get the position where the dragon is at the begining
        tymfiStartLocation = gameObject.transform.position;

        treesUnrooted = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tree")
        {
            // time to unroot the tree
            anim.SetBool("Unroot", true);
        }
        if (other.tag == "Finish" && anim.GetBool("FlyBack") == true)
        {
            // time to throw tree
            anim.SetBool("ThrowTree", true);
        }
        // time to land
        if (other.tag == "Land Area" && alreadyLanded == false)
        {
            // so that if the dragon touches the land areas
            alreadyLanded = true;
            anim.SetBool("ReadyToFight", false);
            // begin landings
            anim.SetBool("LookAtOtherDragon", true);
        }
        if (other.tag == "Land" && anim.GetBool("LookAtOtherDragon") == true)
        {
            // finally land
            anim.SetBool("Land", true);
        }
        if (other.tag == "Tymfi Home" && anim.GetBool("GoHome") == true)
        {
            anim.SetBool("GoToSleep", true);
            Vector3 eulerRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, -60.0f, 0);
        }

        if (other.tag == "Land" && anim.GetBool("GoToSleep") == true)
        {
            anim.SetBool("SleepNow", true);
            enabled = false;
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

        if (animName == "WD_Fly_Forward" && anim.GetBool("FlyBack") == false && otherDragon!=null && otherDragon.activeSelf == false)
        {
            //int treesUnrooted = GameObject.FindGameObjectWithTag("Tree").GetComponent<TreeAnimationManager>().treesUnrooted;
            if (treesUnrooted < trees_arr.Length) {
                targetTree = trees_arr[treesUnrooted];
                Debug.Log(targetTree);
                
                // change direction
                Vector3 targetTreeDirection = targetTree.transform.position - transform.position;
                Quaternion newDirection = Quaternion.LookRotation(targetTreeDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, changeDirectionSpeed);

                //move towards tree
                 transform.position = Vector3.MoveTowards(transform.position, targetTree.transform.position, 3f);
             }
            else
            {
                anim.SetBool("Annoyed", true);
            }

        }

        if (anim.GetBool("Unroot") == true && animName == "WD_Attack_Fly_Legs")
        {
            anim.SetBool("FlyBack", true);
            anim.SetBool("Unroot", false);
        }

        if (animName == "WD_Fly_Forward" && anim.GetBool("FlyBack") == true)
        {
            // fly back
            // change direction
            Vector3 newTargetDirection = tymfiStartLocation - transform.position;
            Quaternion newDirection = Quaternion.LookRotation(newTargetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, changeDirectionSpeed);

            // move towards start position and a little higher
            Vector3 endLocation = new Vector3(tymfiStartLocation.x, tymfiStartLocation.y + 20, tymfiStartLocation.z);
            transform.position = Vector3.MoveTowards(transform.position, endLocation, 3f);
        }

        if (anim.GetBool("ThrowTree") == true && animName == "WD_Attack_Fly_Legs")
        {
            //get another tree
            anim.SetBool("FlyBack", false);
            
        }
        
        if (animName == "WD_Fly_Stand")
        {
            anim.SetBool("ThrowTree", false);
        }

        if (anim.GetBool("Annoyed") == true && animName == "WD_Fly_Left")
        {
            float seconds = DoCircle(0.2f, 50.0f);
            if (seconds == 10)
            {
                anim.SetBool("ReadyToFight", true);
                // time for the Smolikas dragon to come to Tymfi
                otherDragon.SetActive(true);
            }
        }

        if (anim.GetBool("ReadyToFight") == true && anim.GetBool("LookAtOtherDragon") == false)
        {
            anim.SetBool("Annoyed", false);

            // change direction to land location
            Vector3 targetLandDirection = landLocation.transform.position - transform.position;
            Quaternion newDirection = Quaternion.LookRotation(targetLandDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, changeDirectionSpeed);

            //move towards land location
            transform.position = Vector3.MoveTowards(transform.position, landLocation.transform.position, 3f);
        }
        
    }

   float DoCircle(float rotation, float speed)
    {
        timer += Time.deltaTime;
        int seconds = (int)(timer % 60);

        // find the current rotation of the eagle
        float rot_y = transform.eulerAngles.y;
        // rotate it a little
        rot_y -= rotation;
        transform.eulerAngles = new Vector3(0f, rot_y, 0f);
        // move the eagle to the direcation it is facing
        transform.position += transform.forward * Time.deltaTime * speed;
        
        return seconds;
    }
}

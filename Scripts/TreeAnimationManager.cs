using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAnimationManager : MonoBehaviour
{
    [SerializeField] GameObject dragon;
    
    bool treeWasUnrooted;

    Animation anim;
    Rigidbody rb;

    Animator dragonAnim;
    AnimatorClipInfo[] currentDragonAnimInfo;
    string dragonAnimName;

    [SerializeField] GameObject DragonsFeet;

    float timer = 0.0f;

    //public int treesUnrooted;

    void Start()
    {
        anim = GetComponent<Animation>();
        rb = gameObject.GetComponent<Rigidbody>();
                
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Tymfi Dragon")
        {
            // deactivate collider
            gameObject.GetComponent<Collider>().enabled = false;
                        
            // play unrooting animation
            //anim.Play();
            TymfiDragonAnimationManager.instace.treesUnrooted += 1;

            treeWasUnrooted = true;
        }       
    }
    
    void Update()
    {
        if (dragon.activeSelf == true)
        {
            dragonAnim = dragon.GetComponent<Animator>();
            currentDragonAnimInfo = this.dragonAnim.GetCurrentAnimatorClipInfo(0);
            dragonAnimName = currentDragonAnimInfo[0].clip.name;

            // only if the tree at which the script is attached is unrooted
            if (treeWasUnrooted == true && dragonAnim.GetBool("FlyBack") == true)
            {
                // tree has to follow the legs of the dragon to make illusion that the dragon is holding it
                float treeHeight = GetComponent<MeshRenderer>().bounds.size.y;
                transform.position = new Vector3(DragonsFeet.transform.position.x + 1, DragonsFeet.transform.position.y - treeHeight + 1, DragonsFeet.transform.position.z + 1);
            }
        }
        
    }

    void FixedUpdate()
    {
        if (dragon.activeSelf == true)
        {
            dragonAnim = dragon.GetComponent<Animator>();
            currentDragonAnimInfo = this.dragonAnim.GetCurrentAnimatorClipInfo(0);
            dragonAnimName = currentDragonAnimInfo[0].clip.name;

            if (treeWasUnrooted == true && dragonAnim.GetBool("ThrowTree") == true && dragonAnimName == "WD_Attack_Fly_Legs")
            {
                timer += Time.deltaTime;
                int seconds = (int)(timer % 60);

                rb.useGravity = true;
                rb.isKinematic = false;

                Vector3 forceDirection = new Vector3(1, 0.5f, 0);
                rb.AddForce(forceDirection * 500);

                Destroy(gameObject, 3);

            }
        }
    }
}

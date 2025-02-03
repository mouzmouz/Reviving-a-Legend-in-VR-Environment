using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRock : MonoBehaviour
{
    // this script is assigned to the receiver of the thrown rock 
    // because the rocks are inactive at first so the script wouldn't run

    [SerializeField] GameObject rock;
    [SerializeField] float force;
    [SerializeField] GameObject treeThrown;

    Rigidbody rbRock;
    AudioSource audioSource;

    Vector3 dir;
    float dirX;
    GameObject target;

    bool hitTheGround;
        
    void Start()
    {
        rbRock = rock.GetComponent<Rigidbody>();
        audioSource = rock.GetComponent<AudioSource>();
    }

    void Update()
    {
        // if the rock hit the ground hitTheGround = true
        hitTheGround = rock.GetComponent<RockCollisionDetector>().hitTheGround;
    }

    void FixedUpdate()
    {
        Debug.Log(hitTheGround);
        if (treeThrown == null && hitTheGround == false)
        {
            rock.SetActive(true);
            //the game object is the rock's target
            dirX = transform.position.x - rock.transform.position.x;
            dir = new Vector3(dirX, 0, 0).normalized;
            rbRock.AddForce(dir * force);
            rbRock.AddForce(Physics.gravity * rbRock.mass);
        }
        if (hitTheGround == true)
        {
            rbRock.AddForce(Physics.gravity*rbRock.mass); //gravity * mass = weight
        }
    }
}

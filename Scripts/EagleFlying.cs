using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleFlying : MonoBehaviour
{

    private Animator anim;
   
    float rot_y;
    
    [SerializeField] float rotation;
    [SerializeField] float movementSpeed;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        // find the current rotation of the eagle
        rot_y = transform.eulerAngles.y;
        // rotate it a little
        rot_y -= rotation;
        transform.eulerAngles = new Vector3(0f, rot_y, 0f); 
        // move the eagle to the direcation it is facing
        transform.position += transform.forward * Time.deltaTime * movementSpeed;

        int randomNumber = Random.Range(0, 10);

        anim.SetInteger("randomNumber", randomNumber);


    }
 
}

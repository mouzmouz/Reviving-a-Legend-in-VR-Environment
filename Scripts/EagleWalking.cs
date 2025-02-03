using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleWalking : MonoBehaviour
{
    private Animator anim;
    string animName;
    AnimatorClipInfo[] currenAnimInfo;
    
    float z;

    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        int randomNumber = Random.Range(0, 10);
        anim.SetInteger("randomNumber", randomNumber);

        currenAnimInfo = this.anim.GetCurrentAnimatorClipInfo(0);
        animName = currenAnimInfo[0].clip.name;

        // if eagle is currenty walking move in the z axis
        if (animName == "walk")
        {
            z = pos.z + 0.003f;
            gameObject.transform.position = new Vector3(pos.x, pos.y, z);

        }
    }
}

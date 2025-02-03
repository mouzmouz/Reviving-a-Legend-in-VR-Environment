using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliPasaAnimationManager: MonoBehaviour
{
    [SerializeField] GameObject dragon;
    [SerializeField] GameObject aliPasaTools;
    [SerializeField] GameObject Thunders;
    
    private Animator anim;
    float timer = 0.0f;
   
    void Start()
    { 
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (anim.isActiveAndEnabled == true)
        {
            
            timer += Time.deltaTime;
            int seconds = (int)(timer % 60);
            
            // after 15 sec ali pasas and his team see the dragon and run away
            if (seconds == 25)
            {
                // ali pasa goes away
                anim.SetBool("Scared", true);

                // leave tools
                // make their tools visible
                aliPasaTools.SetActive(true);
            }
            if (seconds == 55)
            {
                // the dragon is visible so that the player sees it later
                dragon.SetActive(true);
                // ali pasa doesn't serve a perpose anymore so destroy
                Destroy(gameObject);
            }
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDragonAnimationHandler: MonoBehaviour
{
    [SerializeField] Animator dragonAnim;
    
    IEnumerator Waiter()
    {
        yield return new WaitForSecondsRealtime(30);
        dragonAnim.SetBool("Awake", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.tag == "Player")
        {
            StartCoroutine(Waiter());
        }
    }
}

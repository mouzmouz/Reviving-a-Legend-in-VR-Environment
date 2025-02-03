using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliPasaStory : MonoBehaviour
{
    GameObject[] alipasa;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.tag == "Player")
        {
            alipasa = GameObject.FindGameObjectsWithTag("Ali Pasa");
            for (int i=0; i<alipasa.Length; i++)
            {
                alipasa[i].GetComponent<Animator>().enabled = true;
            }
        }
    }
}

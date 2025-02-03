using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TymfiDragonDamageAudio : StateMachineBehaviour
{
    DragonsAudioManager otherScript;

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        otherScript = GameObject.Find("Tymfi Dragon").GetComponent<DragonsAudioManager>();
        otherScript.PlayDamageAudio();

    }
}

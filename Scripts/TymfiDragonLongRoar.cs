using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TymfiDragonLongRoar : StateMachineBehaviour
{
    DragonsAudioManager otherScript;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        otherScript = GameObject.Find("Tymfi Dragon").GetComponent<DragonsAudioManager>();
        otherScript.PlayLongRoarAudio();

    }
}

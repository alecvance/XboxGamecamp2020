﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingSound : StateMachineBehaviour
{

    public AudioClip audioClip;
    [Range(0.0f, 1.0f)]
    [SerializeField]
    public float volume = 1.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       AudioSource audio = animator.gameObject.GetComponent<AudioSource>();
        audio.volume = volume;
        audio.clip = audioClip;
       audio.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

   // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioSource audio = animator.gameObject.GetComponent<AudioSource>();

        if(audio.clip == audioClip){
            audio.Stop();

        }
 
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

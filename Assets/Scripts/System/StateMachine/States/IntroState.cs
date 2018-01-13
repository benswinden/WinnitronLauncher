﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// The behaviour attached to the Intro state in the Animator component of the GM GameObject.
/// 
/// The functions of this script get called using the Animator transitions and conditions
/// set within Unity's built in Animator.  Please see the Animator component of the GM 
/// GameObject to find out more.
/// </summary>
public class IntroState : State
{
    public VideoClip introClip;
    public AudioClip audioClip;

    private bool introLoaded;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo info, int layerIndex)
    {
        base.OnStateEnter(animator, info, layerIndex);

        //Make sure the jukebox is off
        helper.jukebox.SetActive(false);

        introLoaded = false;

        if (GM.Instance.data.introVideo != null && GM.Instance.data.introVideo != "")
            GM.Instance.video.PlayVideo(GM.Instance.data.introVideo, false, audioClip);
        else
            animator.SetTrigger("NextState");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo info, int layerIndex)
    {
        
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo info, int layerIndex)
    {
        if (GM.Instance.video.player.isPrepared)
        {
            introLoaded = true;
        }

        if (introLoaded && !GM.Instance.video.player.isPlaying)
            animator.SetTrigger("NextState");
    }

}
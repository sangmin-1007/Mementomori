using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class UI_EndingScene : UI_Base<UI_EndingScene> 
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject _EndingCredit;
    private Animator animator;
    private static readonly int playedFinish = Animator.StringToHash("playedFinish");



    private void Start()
    {
        _EndingCredit.SetActive(false);
        animator = GetComponentInChildren<Animator>();

    }


    void Update()
    {
       

        if (!videoPlayer.isPlaying)
        {
            _EndingCredit.SetActive(true);
            animator.SetBool(playedFinish, true);
        }
     
    }
}

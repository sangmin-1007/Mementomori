using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UI_EndingScene : UI_Base<UI_EndingScene> 
{
    [SerializeField] private RawImage cinematicMovie;
    [SerializeField] private RectTransform backGround;

    [SerializeField] private Text playTimeText;
    [SerializeField] private Text deathText;

    [SerializeField] private Text endText;

    private float time;

    private void Start()
    {
        PlayInfoText();
        StartCoroutine(FadeInOutMovie());
    }

    private IEnumerator FadeInOutMovie()
    {
        cinematicMovie.DOFade(1f, 2f);
        yield return new WaitForSeconds(24f);

        cinematicMovie.DOFade(0f, 2f);
        yield return new WaitForSeconds(2f);

        cinematicMovie.gameObject.SetActive(false);

        backGround.DOAnchorPosY(1400f, 30f);
        yield return new WaitForSeconds(30f);


        endText.DOFade(1f, 8f);
        yield return new WaitForSeconds(8f);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        yield break;
    }

    private void PlayInfoText()
    {
        time = Managers.GameManager.totalPlayTime;

        var seconds = TimeSpan.FromSeconds(time);

        playTimeText.text = $"당신의 플레이 타임은\n {seconds.Days}일 {seconds.Hours}시간, {seconds.Minutes}분 이며";
        deathText.text = $"{Managers.UserData.playerDeathCount}번 죽으셨습니다.";
    }
}

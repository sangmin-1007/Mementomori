using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_GameClear : UI_Base<UI_GameClear>
{
    [SerializeField] private Image backGround;
    [SerializeField] private Button nextButton;

    [SerializeField] private CanvasGroup resultCanvasGroup;

    [SerializeField] private TextMeshProUGUI gameClearText;


    private void Start()
    {
        nextButton.onClick.AddListener(OnClickNextButton);
        StartCoroutine(GameClearCoroutine());
    }

    private void OnDisable()
    {
        DestroyUI();
    }

    private IEnumerator GameClearCoroutine()
    {


        backGround.DOFade(1f, 2f);
        yield return new WaitForSeconds(2f);

        StartCoroutine(TypingText());
        yield return new WaitForSeconds(4f);
        gameClearText.DOFade(0f, 2f);
        yield return new WaitForSeconds(2f);

        resultCanvasGroup.DOFade(1f, 2f);
        yield break;


    }

    private void OnClickNextButton()
    {
        Debug.Log("EndingScene");
    }

    private IEnumerator TypingText()
    {
        gameClearText.text = "Game Clear";
        TMPDoText(gameClearText, 2f);

        yield break;
    }

    private void TMPDoText(TextMeshProUGUI text, float duration)
    {
        text.maxVisibleCharacters = 0;
        DOTween.To(x => text.maxVisibleCharacters = (int)x, 0f, text.text.Length, duration);
    }
}

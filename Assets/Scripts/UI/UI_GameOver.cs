using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UI_GameOver : UI_Base<UI_GameOver>
{
    [SerializeField] private Image backGround;
    [SerializeField] private Button LobbyButton;

    [SerializeField] private CanvasGroup resultCanvasGroup;

    [SerializeField] private TextMeshProUGUI gameOverText;

    private void Start()
    {
        LobbyButton.onClick.AddListener(OnClickLobbyButton);
        StartCoroutine(GameOverCoroutine());
    }

    private void OnClickLobbyButton()
    {
        DestroyUI();
        Managers.UI_Manager.ShowLoadingUI("LobbyScene-KSM");
    }


    private IEnumerator GameOverCoroutine()
    {
        

        backGround.DOFade(1f, 2f);
        yield return new WaitForSeconds(2f);

        StartCoroutine(TypingText());
        yield return new WaitForSeconds(4f);
        gameOverText.DOFade(0f, 2f);
        yield return new WaitForSeconds(2f);

        resultCanvasGroup.DOFade(1f, 2f);
        yield break;


    }

    private IEnumerator TypingText()
    {
        gameOverText.text = "Game Over";
        TMPDoText(gameOverText, 2f);

        yield break;
    }

    private void TMPDoText(TextMeshProUGUI text, float duration)
    {
        text.maxVisibleCharacters = 0;
        DOTween.To(x => text.maxVisibleCharacters = (int)x, 0f, text.text.Length, duration);
    }
}

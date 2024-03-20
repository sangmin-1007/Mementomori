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
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Image[] itemSprite;

    private void Start()
    {
        LobbyButton.onClick.AddListener(OnClickLobbyButton);
        Timer();

        for(int i = 0; i < itemSprite.Length; i++)
        {
            if (Managers.DataManager.playerItemData.Count == 0)
                break;

            itemSprite[i].sprite = Managers.DataManager.playerItemData[i].Sprite;

            if (itemSprite[i].sprite != null)
            {
                itemSprite[i].gameObject.SetActive(true);
            }
        }

        StartCoroutine(GameOverCoroutine());
    }

    private void OnClickLobbyButton()
    {
        Managers.UI_Manager.ShowLoadingUI("LobbyScene");
    }

    private void OnDisable()
    {
        DestroyUI();
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

    private void Timer()
    {

        float time = Managers.GameManager.timer;


        int min = Mathf.Max(0, (int)time / 60);
        int sec = Mathf.Max(0, (int)time % 60);

        timeText.text = "Time : " + min.ToString("D2") + ":" + sec.ToString("D2");
    }
}

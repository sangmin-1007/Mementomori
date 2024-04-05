using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UI_StartScene : UI_Base<UI_StartScene>
{

    [Header("бс Text")]
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private GameObject buttonObject;

    [Header("бс CanvasGroup")]
    [SerializeField] private CanvasGroup buttonGroup;


    private void Start()
    {
        Show();
    }

    public void OnDisable()
    {
        DestroyUI();
    }

    private void Show()
    {
        var seq = DOTween.Sequence();
        seq.Append(title.DOFade(1f, 2f)).SetDelay(4f);
        

        seq.Play().OnComplete(() => OnStartText());
    }

    private void OnStartText()
    {
        buttonObject.SetActive(true);
        buttonGroup.DOFade(1f, 1f);
    }

    public void OnClickOptionButton()
    {
        Managers.UI_Manager.ShowUI<UI_Option>();
    }

    public void OnClickExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnClickStartButton()
    {
        Managers.UI_Manager.ShowUI<UI_SelectData>();
    }
}

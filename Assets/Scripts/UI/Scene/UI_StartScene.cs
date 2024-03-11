using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UI_StartScene : UI_Base<UI_StartScene>
{ 
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI startText;

    bool isOnText = false;

    private void Start()
    {
        Show();
    }

    private void Update()
    {
        if (isOnText)
        {
            if (Input.anyKeyDown)
            {
                Debug.Log("���̵�");
            }
        }
    }

    public void OnDisable()
    {
        isOnText = false;
    }

    private void Show()
    {
        var seq = DOTween.Sequence();
        seq.Append(title.DOFade(1f, 2f)).SetDelay(4f);
        

        seq.Play().OnComplete(() => OnStartText());
    }

    private void OnStartText()
    {
        startText.DOFade(1f, 1f).SetLoops(-1, LoopType.Restart);
        
        isOnText = true;
    }
}

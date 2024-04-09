using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Tutorial : UI_Base<UI_Tutorial>
{
    [SerializeField] private GameObject page1;
    [SerializeField] private GameObject page2;

    private int pageCount = 0;

    public override void OnEnable()
    {
        base.OnEnable();
        Managers.UserData.isTutorial = true;
    }

    public void Update()
    {
        if(Input.anyKeyDown)
        {
            pageCount++;
        }

        NextPage();
    }

    private void NextPage()
    {
        if(pageCount == 0)
        {
            page1.SetActive(true);
        }
        else if(pageCount == 1)
        {
            page1.SetActive(false);
            page2.SetActive(true);
        }
        else
        {
            CloseUI();
        }
    }
}

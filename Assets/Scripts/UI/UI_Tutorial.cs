using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Tutorial : UI_Base<UI_Tutorial>
{
    [SerializeField] private GameObject page1;
    [SerializeField] private GameObject page2;
    [SerializeField] private Button goPage2Button;
    [SerializeField] private Button goPage1Button;



    public override void OnEnable()
    {
        base.OnEnable();
        GoPage1();
        Managers.UserData.isTutorial = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Managers.UI_Manager.IsActive<UI_Tutorial>())
            {
                CloseUI();
                Managers.UI_Manager.HideUI<UI_Option>();
            }
        }
    }



    public void GoPage2()
    {
         page1.SetActive(false);
         page2.SetActive(true);
    }

    public void GoPage1()
    {
        page1.SetActive(true);
        page2.SetActive(false);

    }

    public void Exit()
    {
        CloseUI();
    }

   





}

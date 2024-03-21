using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSM_Test : MonoBehaviour
{
    //private void Start()
    //{
    //    Managers.UI_Manager.ShowUI<UI_StartScene>();
    //}
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            Managers.UI_Manager.ShowUI<UI_GameClear>();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            Managers.UI_Manager.ShowUI<UI_GameOver>();
        }
    }
}

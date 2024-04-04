using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SelectData : UI_Base<UI_SelectData>
{
    [SerializeField] private GameObject[] saveText; // 저장된 데이터가 있을때 표시될 텍스트
    [SerializeField] private GameObject[] emptyText; // 저장된 데이터가 없을때 표시될 텍스트

    [SerializeField] private TextMeshProUGUI[] lastPlaytimeText; // 마지막으로 플레이한 시간을 표시하기 위한 텍스트
    [SerializeField] private TextMeshProUGUI[] deathText; // 저장된 데이터의 플레이어가 총 몇번 죽었는지 표시하기 위한 텍스트

    [SerializeField] private Button[] trashButton;

    private bool[] savefile = new bool[3]; // 세이브 파일 존재유무 확인

    private void Start()
    {
        DataSlotSetting();
    }

    public void SaveSlotButton(int number)
    {
        Managers.DataManager.nowSlot = number;

        if (savefile[number])
        {
            Managers.DataManager.LoadDataSetting();
            Managers.UI_Manager.ShowLoadingUI("LobbyScene");
        }
        else
        {
            FirstGameStart();
        }
    }

    public void DeleteSaveDataButton(int number)
    {
        Managers.DataManager.nowSlot = number;
        if (savefile[number])
        {
            Managers.DataManager.SaveDataDelete();
            DataSlotSetting();
        }
        else
            return;
    }

    public void FirstGameStart()
    {
        if (!savefile[Managers.DataManager.nowSlot]) // 현재 슬롯의 데이터가 없다면
        {
            Managers.UserData.playerGold = 100000; // 초기에 플레이어 골드를 1000골드를 지급해주고
            Managers.DataManager.Save(); // 현재 정보를 저장해준다
        }
        Managers.UI_Manager.ShowLoadingUI("LobbyScene");
    }

    private void DataSlotSetting()
    {
        for (int i = 0; i < savefile.Length; i++)
        {
            if (File.Exists(Managers.DataManager.path + $"{i}"))
            {
                savefile[i] = true;
                Managers.DataManager.nowSlot = i;
                Managers.DataManager.Load();
                saveText[i].SetActive(true);
                trashButton[i].gameObject.SetActive(true);
                emptyText[i].SetActive(false);

                lastPlaytimeText[i].text = DateTime.Parse(Managers.DataManager.nowPlayerData.dateTime).ToString("yyyy-MM-dd HH:mm:ss");
                deathText[i].text = Managers.DataManager.nowPlayerData.playerDeathCount.ToString("00");
            }
            else
            {
                trashButton[i].gameObject.SetActive(false);
                savefile[i] = false;
                saveText[i].SetActive(false);
                emptyText[i].SetActive(true);
            }
        }

        Managers.DataManager.DataClear();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SelectData : UI_Base<UI_SelectData>
{
    [SerializeField] private GameObject[] saveText;
    [SerializeField] private GameObject[] emptyText;

    [SerializeField] private TextMeshProUGUI[] lastPlaytimeText;
    [SerializeField] private TextMeshProUGUI[] deathText;

    [SerializeField] private Button[] trashButton;

    private bool[] savefile = new bool[3];

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
            Managers.PlayerEquipStatsManager.EquipItemStatsUpdate();
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
        if (!savefile[Managers.DataManager.nowSlot])
        {
            Managers.UserData.playerGold = 500;
            Managers.DataManager.Save(); 
        }
        Managers.UI_Manager.ShowUI<UI_StartMovie>();
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SelectData : UI_Base<UI_SelectData>
{
    [SerializeField] private GameObject[] saveText; // ����� �����Ͱ� ������ ǥ�õ� �ؽ�Ʈ
    [SerializeField] private GameObject[] emptyText; // ����� �����Ͱ� ������ ǥ�õ� �ؽ�Ʈ

    [SerializeField] private TextMeshProUGUI[] lastPlaytimeText; // ���������� �÷����� �ð��� ǥ���ϱ� ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI[] deathText; // ����� �������� �÷��̾ �� ��� �׾����� ǥ���ϱ� ���� �ؽ�Ʈ

    [SerializeField] private Button[] trashButton;

    private bool[] savefile = new bool[3]; // ���̺� ���� �������� Ȯ��

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
        if (!savefile[Managers.DataManager.nowSlot]) // ���� ������ �����Ͱ� ���ٸ�
        {
            Managers.UserData.playerGold = 100000; // �ʱ⿡ �÷��̾� ��带 1000��带 �������ְ�
            Managers.DataManager.Save(); // ���� ������ �������ش�
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

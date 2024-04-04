using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class UI_SelectData : UI_Base<UI_SelectData>
{
    [SerializeField] private GameObject[] saveText; // ����� �����Ͱ� ������ ǥ�õ� �ؽ�Ʈ
    [SerializeField] private GameObject[] emptyText; // ����� �����Ͱ� ������ ǥ�õ� �ؽ�Ʈ

    [SerializeField] private TextMeshProUGUI[] lastPlaytimeText; // ���������� �÷����� �ð��� ǥ���ϱ� ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI[] deathText; // ����� �������� �÷��̾ �� ��� �׾����� ǥ���ϱ� ���� �ؽ�Ʈ


    private bool[] savefile = new bool[3]; // ���̺� ���� �������� Ȯ��

    private void Start()
    {
        for(int i = 0; i < savefile.Length; i++)
        {
            if (File.Exists(Managers.DataManager.path + $"{i}"))
            {
                savefile[i] = true;
                Managers.DataManager.nowSlot = i;
                Managers.DataManager.Load();
                saveText[i].SetActive(true);
                emptyText[i].SetActive(false);

                lastPlaytimeText[i].text = Managers.DataManager.nowPlayerData.dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                deathText[i].text = Managers.DataManager.nowPlayerData.playerDeathCount.ToString("00");
            }
            else
            {
                savefile[i] = false;
                saveText[i].SetActive(false);
                emptyText[i].SetActive(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class UI_SelectData : UI_Base<UI_SelectData>
{
    [SerializeField] private GameObject[] saveText; // 저장된 데이터가 있을때 표시될 텍스트
    [SerializeField] private GameObject[] emptyText; // 저장된 데이터가 없을때 표시될 텍스트

    [SerializeField] private TextMeshProUGUI[] lastPlaytimeText; // 마지막으로 플레이한 시간을 표시하기 위한 텍스트
    [SerializeField] private TextMeshProUGUI[] deathText; // 저장된 데이터의 플레이어가 총 몇번 죽었는지 표시하기 위한 텍스트


    private bool[] savefile = new bool[3]; // 세이브 파일 존재유무 확인

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

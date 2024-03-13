using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LobbyScene : MonoBehaviour
{
    [SerializeField] private TilemapCollider2D shopTrigger;
    [SerializeField] private TilemapCollider2D doorTrigger;
    [SerializeField] private Collider2D storageTrigger;
    [SerializeField] private DOTweenSettings settings;

    private bool isShop;
    private bool isDoor;
    private bool isStorage;

    private void Update()
    {
        OnInteraction();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == shopTrigger)
        {
            isShop = true;
        }
        else if(collision == doorTrigger)
        {
            isDoor = true;
        }
        else if(collision ==  storageTrigger)
        {
            isStorage = true;
        }
    }

    private void OnInteraction()
    {
        if(isShop)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("¼¥");
            }
        }
        else if(isDoor)
        {
            Managers.UI_Manager.ShowLoadingUI("LobbyScene-KSM");
        }
        else if(isStorage)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Ã¢°í");
            }
        }
    }
}

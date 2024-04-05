using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InteractType
{
    Shop,
    Storage,
    Door,
    GameMapReposition
}
public class LobbyInteract : MonoBehaviour
{
    public InteractType interactType;

    public void Interaction()
    {
        switch(interactType)
        {
            case InteractType.Shop:
                Managers.UI_Manager.ShowUI<UI_Shop>();
            break;

            case InteractType.Storage:
                Managers.UI_Manager.ShowUI<UI_Storage>();
            break;

            case InteractType.Door:
                Managers.DataManager.Save();
                Managers.UI_Manager.ShowLoadingUI("GameScene");
            break;
            default:
                break;

        }
    }
}

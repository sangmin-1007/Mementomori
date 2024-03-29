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
public class LobbyIneract : MonoBehaviour
{
    public InteractType interactType;

    public void Interaction()
    {
        switch(interactType)
        {
            case InteractType.Shop:
                    Debug.Log("¼¥");
            break;

            case InteractType.Storage:
                Managers.UI_Manager.ShowUI<UI_Storage>();
            break;

            case InteractType.Door:

                Managers.UI_Manager.ShowLoadingUI("GameScene");

            break;

            default:
                break;

        }
    }
}

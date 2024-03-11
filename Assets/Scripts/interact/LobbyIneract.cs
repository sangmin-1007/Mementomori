using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InteractType
{
    Shop,
    Storage,
    Door
}
public class LobbyIneract : MonoBehaviour
{
    public InteractType interactType;



    public void Interaction()
    {
        switch(interactType)
        {
            case InteractType.Shop:

                Debug.Log("��");

            break;

            case InteractType.Storage:

                Debug.Log("â��");

            break;

            case InteractType.Door:

                Managers.UI_Manager.ShowLoadingUI("LobbyScene-KSM");

            break;

            default:
                break;

        }
    }
}
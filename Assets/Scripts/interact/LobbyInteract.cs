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
                Managers.SoundManager.Play("Effect/StoreBell", Sound.Effect);
                if (Managers.UI_Manager.IsActive<UI_Shop>())
                {
                    Managers.UI_Manager.HideUI<UI_Shop>();
                }
                else
                    Managers.UI_Manager.ShowUI<UI_Shop>();
            
                
                break;

            case InteractType.Storage:
                if (Managers.UI_Manager.IsActive<UI_Storage>())
                {
                    Managers.UI_Manager.HideUI<UI_Storage>();
                }
                else
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

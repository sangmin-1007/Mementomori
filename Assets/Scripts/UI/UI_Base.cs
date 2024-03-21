using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Base<T> : MonoBehaviour
{
    public bool IsEnabled { get; private set; } = true;

    public virtual void OnEnable()
    {
        OpenUI();
    }

    public virtual void OpenUI()
    {
        IsEnabled = true;
        gameObject.SetActive(true);
    }

    public virtual void CloseUI()
    {
        gameObject.SetActive(false);
        IsEnabled = false;
    }

    public virtual void DestroyUI()
    {
        Managers.UI_Manager.DestroyUI<T>();
    }
}

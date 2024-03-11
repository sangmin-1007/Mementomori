using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers instance;

    public static Managers Instance { get { Initilize(); return instance; } }

    // Manager
    private UI_Manager _uiManager;

    // Manager Singletone
    public static UI_Manager UI_Manager => Instance._uiManager;



    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Excute()
    {
        Initilize();
    }

    private static void Initilize()
    {
        if(instance == null)
        {
            GameObject go = GameObject.Find("@AllManagers");

            if(go == null)
            {
                go = new GameObject("@AllManagers");
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            instance = go.GetComponent<Managers>();

            //TODO Manager registration
            if(!go.TryGetComponent(out instance._uiManager))
            {
                instance._uiManager = go.AddComponent<UI_Manager>();
            }
        }
    }
}

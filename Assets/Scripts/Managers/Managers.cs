using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers instance;

    public static Managers Instance { get { Initilize(); return instance; } }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Excute()
    {
        Initilize();
    }

    private static void Initilize()
    {
        if(instance == null)
        {
            GameObject allManagers = GameObject.Find("@AllManagers");

            if(allManagers == null)
            {
                allManagers = new GameObject("@AllManagers");
                allManagers.AddComponent<Managers>();
            }

            DontDestroyOnLoad(allManagers);
            instance = allManagers.GetComponent<Managers>();

            //TODO Manager registration

        }
    }
}

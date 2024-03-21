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
    private GameSceneManager _gameSceneManager;
    private LobbySceneManager _LobbySceneManager;
    private GameManager _gameManager;
    private SceneLoader _sceneLoader;
    private DataManager _dataManager;
    private ItemObjectPool _itemObjectPool;

    // Manager Singletone
    public static UI_Manager UI_Manager => Instance._uiManager;
    public static GameManager GameManager => Instance._gameManager;
    public static LobbySceneManager LobbySceneManager => Instance._LobbySceneManager;
    public static GameSceneManager GameSceneManager => Instance._gameSceneManager;
    public static SceneLoader SceneLoader => Instance._sceneLoader;
    public static DataManager DataManager => Instance._dataManager;

    public static ItemObjectPool ItemObjectPool => Instance._itemObjectPool;


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

            if(!go.TryGetComponent(out instance._gameManager))
            {
                instance._gameManager = go.AddComponent<GameManager>();
            }

            if (!go.TryGetComponent(out instance._gameSceneManager))
            {
                instance._gameSceneManager = go.AddComponent<GameSceneManager>();
            }

            if (!go.TryGetComponent(out instance._LobbySceneManager))
            {
                instance._LobbySceneManager= go.AddComponent<LobbySceneManager>();
            }

            if (!go.TryGetComponent(out instance._sceneLoader))
            {
                instance._sceneLoader = go.AddComponent<SceneLoader>();
            }

            if(!go.TryGetComponent(out instance._dataManager))
            {
                instance._dataManager = go.AddComponent<DataManager>();
            }

            if(!go.TryGetComponent(out instance._itemObjectPool))
            {
                Instance._itemObjectPool = go.AddComponent<ItemObjectPool>();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneNumber
{
    Title = 0,
    Lobby,
    Game,
    Ending,
}

public class SceneLoader : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadScene(SceneNumber scene)
    {
        SceneManager.LoadSceneAsync((int)scene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch(scene.buildIndex)
        {
            // StartScene
            case 0:
                Managers.UI_Manager.ShowUI<UI_StartScene>();
                break;
            // LobbyScene
            case 1:
                Managers.LobbySceneManager.IntializeLobbyScene();
                break;
            // GameScene
            case 2:
                Managers.GameSceneManager.InitializeGameScene();
                break;
            //EndingScene
            case 3:
                break;
        }
    }
}

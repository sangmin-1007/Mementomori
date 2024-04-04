using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float timer;
    public float totalPlayTime;


    public void Ontimer()
    {
        timer += Time.deltaTime;
    }

    public void ResetTimer()
    {
        timer = 0f;
    }

    private void Update()
    {
        totalPlayTime += Time.unscaledDeltaTime;
    }

    public void GameClear()
    {
        Managers.UI_Manager.ShowUI<UI_GameClear>();
        
    }

    public void GameOver()
    {
        Managers.UserData.playerDeathCount++;
        Managers.UI_Manager.ShowUI<UI_GameOver>();
        Managers.SoundManager.Play("Effect/GameOver", Sound.Effect);
    }
}

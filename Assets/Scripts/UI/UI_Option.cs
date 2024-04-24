using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Option : UI_Base<UI_Option>
{
    [SerializeField] private Button saveButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button quitGameButton;
    [SerializeField] private Button lobbyButton;
    [SerializeField] private Button tutorialButton;

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Slider masterSlider;

    private GameObject player;


    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(ChangeMasterSound);
        bgmSlider.onValueChanged.AddListener(ChangeBgmSound);
        effectSlider.onValueChanged.AddListener(ChangeEffectSound);
   
    }

    public override void OnEnable()
    {
        base.OnEnable();

        Time.timeScale = 0f;

        masterSlider.value = Managers.UserData.Master_VOLUME_KEY;
        bgmSlider.value = Managers.UserData.BGM_VOLUME_KEY;
        effectSlider.value = Managers.UserData.Effect_VOLUME_KEY;

        ButtonActiveControll();

    }

    public void OnDisable()
    {
        Time.timeScale = 1f;
    }

    void ChangeMasterSound(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        Managers.UserData.Master_VOLUME_KEY = volume;

        PlayerPrefs.SetFloat("Master", Managers.UserData.Master_VOLUME_KEY);
     
    }

    void ChangeBgmSound(float volume)
    {

        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        Managers.UserData.BGM_VOLUME_KEY = volume;

        PlayerPrefs.SetFloat("BGM", Managers.UserData.BGM_VOLUME_KEY);
    }

    void ChangeEffectSound(float volume)
    {
        audioMixer.SetFloat("Effect", Mathf.Log10(volume) * 20);
        Managers.UserData.Effect_VOLUME_KEY = volume;

        PlayerPrefs.SetFloat("Effect", Managers.UserData.Effect_VOLUME_KEY);
    }


   

    public void OnClickOptionExitButton()
    {
        CloseUI();
    }

    public void OnClickQuitGameButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnClickSaveButton()
    {
        Managers.DataManager.Save();
    }

    public void OnClickTutorialButton()
    {
        OnClickOptionExitButton();
        Managers.UserData.isTutorial = true;
        Managers.UI_Manager.ShowUI<UI_Tutorial>();
    }

    public void OnClickLobbyButton()
    {
        Time.timeScale = 1f;
        Managers.UI_Manager.ShowLoadingUI("LobbyScene");

        player = Managers.GameSceneManager.Player;
        HealthSystem healthSystem = player.GetComponent<HealthSystem>();

        healthSystem.ChangeHealth(-10000f);

    }

    private void ButtonActiveControll()
    {
        if(SceneManager.GetActiveScene().name == "StartScene")
        {
            tutorialButton.gameObject.SetActive(false);
            saveButton.gameObject.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            quitGameButton.gameObject.SetActive(false);
            lobbyButton.gameObject.SetActive(true);
        }
        else
        {
            quitGameButton.gameObject.SetActive(true);
            lobbyButton.gameObject.SetActive(false);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_Option : UI_Base<UI_Option>
{
    [SerializeField] private Button saveButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button quitGameButton;


    [SerializeField] private AudioMixer MasterMixer;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixer sfxMixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider masterSlider;

    private const string BGM_VOLUME_KEY = "BGMVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";
    private const string MASTER_VOLUME_KEY = "MasterVolume";


    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(ChangeMasterSound);
        bgmSlider.onValueChanged.AddListener(ChangeBgmSound);
        sfxSlider.onValueChanged.AddListener(ChangeSfxSound);
   
    }

    public override void OnEnable()
    {
        base.OnEnable();
        bgmSlider.value = PlayerPrefs.GetFloat(BGM_VOLUME_KEY);
        sfxSlider.value = PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
        masterSlider.value = PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);

    }

    void ChangeMasterSound(float volume)
    {
        //sfxMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }

    void ChangeBgmSound(float volume)
    {

        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }

    void ChangeSfxSound(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
        PlayerPrefs.Save();
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




 
}

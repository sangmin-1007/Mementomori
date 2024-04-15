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



    [SerializeField] private AudioMixer audioMixer;


    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Slider masterSlider;

    private const string BGM_VOLUME_KEY = "BGMVolume";
    private const string Effect_VOLUME_KEY = "EffectVolume";
    private const string Master_VOLUME_KEY = "MasterVolume";


    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(ChangeMasterSound);
        bgmSlider.onValueChanged.AddListener(ChangeBgmSound);
        effectSlider.onValueChanged.AddListener(ChangeEffectSound);
   
    }

    public override void OnEnable()
    {
        base.OnEnable();
        bgmSlider.value = PlayerPrefs.GetFloat(BGM_VOLUME_KEY);
        effectSlider.value = PlayerPrefs.GetFloat(Effect_VOLUME_KEY);
        masterSlider.value = PlayerPrefs.GetFloat(Master_VOLUME_KEY);

    }

    void ChangeMasterSound(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(Master_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }

    void ChangeBgmSound(float volume)
    {

        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }

    void ChangeEffectSound(float volume)
    {
        audioMixer.SetFloat("Effect", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(Effect_VOLUME_KEY, volume);
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

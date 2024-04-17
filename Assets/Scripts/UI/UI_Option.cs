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
    [SerializeField] private Button tutorialButton;





    [SerializeField] private AudioMixer audioMixer;


    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Slider masterSlider;




    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(ChangeMasterSound);
        bgmSlider.onValueChanged.AddListener(ChangeBgmSound);
        effectSlider.onValueChanged.AddListener(ChangeEffectSound);
   
    }

    public override void OnEnable()
    {
        base.OnEnable();

        masterSlider.value = Managers.UserData.Master_VOLUME_KEY;
        bgmSlider.value = Managers.UserData.BGM_VOLUME_KEY;
        effectSlider.value = Managers.UserData.Effect_VOLUME_KEY;
   

    }

    void ChangeMasterSound(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        Managers.UserData.Master_VOLUME_KEY = volume;
        
     
    }

    void ChangeBgmSound(float volume)
    {

        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        Managers.UserData.BGM_VOLUME_KEY = volume;

    }

    void ChangeEffectSound(float volume)
    {
        audioMixer.SetFloat("Effect", Mathf.Log10(volume) * 20);
        Managers.UserData.Effect_VOLUME_KEY = volume;
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






}

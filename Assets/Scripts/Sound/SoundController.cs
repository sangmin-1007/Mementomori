using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioMixer MasterMixer;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixer sfxMixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider masterSlider;


    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(ChangeMasterSound);
        bgmSlider.onValueChanged.AddListener(ChangeBgmSound);
        sfxSlider.onValueChanged.AddListener(ChangeSfxSound);
    }

    void ChangeMasterSound(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        sfxMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    void ChangeBgmSound(float volume)
    {

        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    void ChangeSfxSound(float volume)
    {
        sfxMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }
}

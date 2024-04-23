using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public enum Sound
{
    Bgm,
    Effect,
    MaxCount,
}


public class SoundManager :MonoBehaviour
{
 
    
    AudioSource[] audioSources = new AudioSource[(int)Sound.MaxCount];
    Dictionary<string,AudioClip> audioClips = new Dictionary<string,AudioClip>();
    AudioMixer audioMixer;

    AudioMixerGroup[] masterMixer;
    AudioMixerGroup[] bgmMixer;
    AudioMixerGroup[] effectMixer;

   


    public void Awake()
    {
        audioMixer = Resources.Load<AudioMixer>("Sounds/AudioMixer");
        masterMixer = audioMixer.FindMatchingGroups("Master");
        bgmMixer = audioMixer.FindMatchingGroups("BGM");
        effectMixer = audioMixer.FindMatchingGroups("Effect");
        string[] soundNames = System.Enum.GetNames(typeof(Sound));
        for (int i = 0; i < soundNames.Length - 1; i++)
        {
            GameObject go = new GameObject { name = soundNames[i] };
            audioSources[i] = go.AddComponent<AudioSource>();
            audioSources[i].volume = 0.7f;
            DontDestroyOnLoad(go);
        }

        ChangeVolume();
    }

    public void Play(string path, Sound type = Sound.Effect)
    {
       
        if (path.Contains("Sounds/") == false)
            path =$"Sounds/{path}";

        
        if(type == Sound.Bgm)
        {
           
            AudioClip audioclip = Resources.Load<AudioClip>(path);
            if(audioclip == null )

            {
               
                Debug.Log("AudioClip Missing ! {path} ");
                return;
            }
            AudioSource audioSource = audioSources[(int)Sound.Bgm];
         

            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioclip;

            audioSource.outputAudioMixerGroup = bgmMixer[0];
           
          
            audioSource.Play();
            audioSources[(int)Sound.Bgm].loop = true;
        }
        
        else if(type == Sound.Effect)
        {
           
            AudioClip audioclip = GetOrAddAudioClip(path);
            if (audioclip == null)
            {
                Debug.Log("EffectAudioClip Missing ! {path} ");
                return;
            }


            AudioSource audioSource = audioSources[(int)Sound.Effect];
            audioSource.outputAudioMixerGroup = effectMixer[0];
            audioSource.PlayOneShot(audioclip);
       


        }
        
    }


    AudioClip GetOrAddAudioClip(string path) 
    {
        AudioClip audioClip = null;
        if (audioClips.TryGetValue(path, out audioClip) == false) 
        {
            audioClip = Resources.Load<AudioClip>(path);
            audioClips.Add(path, audioClip); 
        } 
            return audioClip;

    }

    public void AudioClear() 
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
        audioClips.Clear();
    }

    public void ChangeVolume()
    {
        masterMixer[0].audioMixer.SetFloat("Master", Mathf.Log10(Managers.UserData.Master_VOLUME_KEY) * 20);
        bgmMixer[0].audioMixer.SetFloat("BGM", Mathf.Log10(Managers.UserData.BGM_VOLUME_KEY) * 20);
        effectMixer[0].audioMixer.SetFloat("Effect", Mathf.Log10(Managers.UserData.Effect_VOLUME_KEY) * 20);
    }

 
}




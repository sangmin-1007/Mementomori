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
    AudioMixerGroup bgmMixer;
    AudioMixerGroup sfxMixer;


    public void Awake()
    {
      
        bgmMixer = Resources.Load<AudioMixerGroup>("Sounds/AudioMixer");
        sfxMixer = Resources.Load<AudioMixerGroup>("Sounds/SfxMixer");
        string[] soundNames = System.Enum.GetNames(typeof(Sound));
        for (int i = 0; i < soundNames.Length - 1; i++)
        {
            GameObject go = new GameObject { name = soundNames[i] };
            audioSources[i] = go.AddComponent<AudioSource>();
            DontDestroyOnLoad(go);
        }
     
   
    }

    public void Play(string path, Sound type = Sound.Effect, float volume=1.0f)
    {
        
        audioSources[(int)Sound.Bgm].loop = true;
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
            audioSource.outputAudioMixerGroup = bgmMixer;
           

             audioSource.volume = volume;
          

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
     
            audioSource.volume = volume;
            audioSource.PlayOneShot(audioclip);
            audioSource.outputAudioMixerGroup = sfxMixer;


        }
        
    }


    AudioClip GetOrAddAudioClip(string path) //위에 Effect부분을 계속해서 Path로 불러오면 비효율일까봐
    {
        AudioClip audioClip = null;
        if (audioClips.TryGetValue(path, out audioClip) == false) //만약 오디오클립을 찾는중에 path 가없으면 
        {
            audioClip = Resources.Load<AudioClip>(path);
            audioClips.Add(path, audioClip); // 딕셔너리에 추가
        } 
            return audioClip;//만약 오디오클립을 찾는중에 path 가있으면 그 오디오클립을 틀어주고

    }

    public void AudioClear() // 딕셔너리에 있는 클립들 초기화 (무한 씬이동으로 누적되면.. 터지기방지)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
        audioClips.Clear();
    }

 
}




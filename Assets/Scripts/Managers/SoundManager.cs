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


    AudioClip GetOrAddAudioClip(string path) //���� Effect�κ��� ����ؼ� Path�� �ҷ����� ��ȿ���ϱ��
    {
        AudioClip audioClip = null;
        if (audioClips.TryGetValue(path, out audioClip) == false) //���� �����Ŭ���� ã���߿� path �������� 
        {
            audioClip = Resources.Load<AudioClip>(path);
            audioClips.Add(path, audioClip); // ��ųʸ��� �߰�
        } 
            return audioClip;//���� �����Ŭ���� ã���߿� path �������� �� �����Ŭ���� Ʋ���ְ�

    }

    public void AudioClear() // ��ųʸ��� �ִ� Ŭ���� �ʱ�ȭ (���� ���̵����� �����Ǹ�.. ���������)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
        audioClips.Clear();
    }

 
}




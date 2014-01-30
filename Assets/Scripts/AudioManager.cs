using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    #region singleton code
    private static AudioManager ms_Instance;
    private bool _IsEnabled = true;
    public static AudioManager Instance
    {
        get
        {
            if (ms_Instance == null)
            {
                ms_Instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;
                if (ms_Instance == null)
                {
                    GameObject instance = new GameObject();
                    instance.AddComponent("AudioManager");
                    instance.name = "Audio Manager";
                    ms_Instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;
                }
            }
            return ms_Instance;
        }
    }
    #endregion

    private List<AudioSource> m_AudioSources = new List<AudioSource>();
    public static bool IsMute = false;

    public void ToggleAudio(bool pEnabled)
    {
        _IsEnabled = pEnabled;
    }

    public void UpdateMuteState()
    {
        if (IsMute)
        {
            AudioListener.volume = 1.0f;
            IsMute = false;
        }   
        else
        {
            AudioListener.volume = 0.0f;
            IsMute = true;
        }
        

        foreach(AudioSource source in m_AudioSources)
        {
            source.mute = IsMute;
        }
    }

    public void PlaySound(string name)
    {
        if (_IsEnabled)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = Resources.Load("Audio/" + name) as AudioClip;
            audioSource.volume = 1.0f;

            audioSource.mute = IsMute;
            audioSource.Play();

            m_AudioSources.Add(audioSource);
        }
    }

    public void PlaySound(string name, float pitch)
    {
        if (_IsEnabled)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = Resources.Load("Audio/" + name) as AudioClip;
            audioSource.volume = 1.0f;

            audioSource.mute = IsMute;
            audioSource.pitch = pitch;
            audioSource.Play();

            m_AudioSources.Add(audioSource);
        }
    }

    public void PlaySound(string name, float pitch, float pVolume)
    {
        if (_IsEnabled)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = Resources.Load("Audio/" + name) as AudioClip;
            audioSource.volume = pVolume;

            audioSource.mute = IsMute;
            audioSource.pitch = pitch;
            audioSource.Play();

            m_AudioSources.Add(audioSource);
        }
    }

    public void PlaySound(string name, float pitch, float pVolume, bool pLoop)
    {
        if (_IsEnabled)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = Resources.Load("Audio/" + name) as AudioClip;
            audioSource.volume = pVolume;

            audioSource.mute = IsMute;
            audioSource.pitch = pitch;
            audioSource.loop = pLoop;
            audioSource.Play();

            m_AudioSources.Add(audioSource);
        }
    }

    public void StopSound(string name)
    {
        List<AudioSource> stoppedAudioSources = m_AudioSources.FindAll(source => source.isPlaying == true);
        foreach (AudioSource source in stoppedAudioSources)
        {
            if (source.clip.name == name)
                source.Stop();
        }
    }

    public void StopAllSounds()
    {
        List<AudioSource> stoppedAudioSources = m_AudioSources.FindAll(source => source.isPlaying == true);
        foreach (AudioSource source in stoppedAudioSources)
        {
            m_AudioSources.Remove(source);
            Destroy(source);
        }
    }

    /// <summary>
    /// You can only update pitch if the source is loopable.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="pitch"></param>
    public void UpdatePitch(string name, float pitch)
    {
        foreach (AudioSource source in m_AudioSources)
        {
            if (source.clip.name == name && source.loop)
            {
                source.pitch = pitch;
            }
        }
    }
    public float GetSoundPitch(string name)
    {
        foreach (AudioSource source in m_AudioSources)
        {
            if (source.clip.name == name && source.loop)
            {
                return source.pitch;
            }
        }
        return 1.0f;
    }

    public void ToggleMute(bool pMuted)
    {
        foreach (AudioSource source in m_AudioSources)
        {
            IsMute = pMuted;
            if (pMuted) source.volume = 0.0f;
            else source.volume = 1.0f;
        }
    }

    private void Update()
    {
        List<AudioSource> stoppedAudioSources = m_AudioSources.FindAll(source => source.isPlaying == false);
        foreach (AudioSource source in stoppedAudioSources)
        {
            m_AudioSources.Remove(source);
            Destroy(source);
        }
    }

    /*
    void OnDestroy()
    {
        
        List<AudioSource> stoppedAudioSources = m_AudioSources.FindAll(source => source.isPlaying == false);
        foreach (AudioSource source in stoppedAudioSources)
        {
            m_AudioSources.Remove(source);
            Destroy(source);
        }

        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor) return;

        //Destroy(ms_Instance);
        //ms_Instance = null;
        
    }
     * */
}
using System.Collections;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sfxSounds;
    public AudioSource sfxSource;
    private bool isSFXPlaying = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlaySFX(string name, bool withoutRepeat)
    {
        if (withoutRepeat)
        {
            if (isSFXPlaying)
            {
                return;
            }
        }
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.LogError("Sound not found");
        }
        else
        {
            isSFXPlaying = true;
            sfxSource.PlayOneShot(s.clip);
            StartCoroutine(ResetSFXPlayingStatus(s.clip.length));
        }
    }

    public void StopSFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.LogError("Sound not found");
        }
        else
        {
            sfxSource.Stop();
            isSFXPlaying = false;
        }
    }

    private IEnumerator ResetSFXPlayingStatus(float delay)
    {
        yield return new WaitForSeconds(delay);
        isSFXPlaying = false;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}

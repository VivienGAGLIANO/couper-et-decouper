using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource sfxSource;

    private bool firstSourcePlaying;

    public void Awake()
    {
        if(instance)
        {
            Debug.Log("Il y a déjà une instance de SoundManager " + name);
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            musicSource = this.gameObject.AddComponent<AudioSource>();
            musicSource2 = this.gameObject.AddComponent<AudioSource>();
            sfxSource = this.gameObject.AddComponent<AudioSource>();
            
            musicSource.loop = true;
            musicSource2.loop = true;
        }
    }

    public void PlayMusic(AudioClip musicClip)
    {
        AudioSource activesource = (firstSourcePlaying) ? musicSource : musicSource2;
        
        activesource.clip = musicClip;
        activesource.volume = 1;
        activesource.Play();
    }

    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f)
    {
        AudioSource activeSource = (firstSourcePlaying) ? musicSource : musicSource2;
        
        StartCoroutine(UpdateMusicWithFade(activeSource,newClip,transitionTime));
    }

    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f)

    {
        AudioSource activeSource = (firstSourcePlaying) ? musicSource : musicSource2;
        AudioSource newSource = (firstSourcePlaying) ? musicSource2 : musicSource;
        
        firstSourcePlaying = !firstSourcePlaying;

        newSource.clip = musicClip;
        newSource.Play();

        StartCoroutine(UpdateMusicWithCrossFade(activeSource,newSource,transitionTime));
    }
    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        if (!activeSource.isPlaying)
            activeSource.Play();

        float t = 0.0f;

        for (t= 0; t< transitionTime; t+=Time.deltaTime)
        {
            activeSource.volume = (1-(t/transitionTime));
            yield return null;
        }
        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();

        for (t= 0; t< transitionTime; t+=Time.deltaTime)
        {
            activeSource.volume = (t/transitionTime);
            yield return null;
        }
    }
    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime)
    {
        float t = 0f;

        for (t= 0f; t<= transitionTime; t+=Time.deltaTime)
        {
            original.volume = (1-(t/transitionTime));
            newSource.volume = (t / transitionTime);
            yield return null;
        }

        original.Stop();
    }
   
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip,volume);
    }


    public void SetMusicVolume(float volume)
    {
        Debug.Log("[SoundManager] : Volume général = " + volume);
        musicSource.volume = volume;
        musicSource2.volume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        Debug.Log("[SoundManager] : Volume SFX = " + volume);
        sfxSource.volume = volume;
    }

    public void ChangeLoop(bool value)
    {
        musicSource.loop = value;
        musicSource2.loop = value; 
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    [Header("SoundData")]
    [SerializeField] AudioDetil_SO audioDetil_SO;
    [SerializeField] SceneMuice_SO sceneMuice_SO;
    [Header("AudioSource")]
    [SerializeField]AudioSource musicSource;
    [SerializeField]AudioSource ambientSource;
    [Header("AudioMixer")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioMixerSnapshot normalSnapShot;
    [SerializeField] AudioMixerSnapshot ambientOnlySnapShot;
    [SerializeField] AudioMixerSnapshot muteSnapShot;
    [Header("PlayMusicTime")]
    [SerializeField] float playDelayTime;
    Coroutine soundRoutine;
    private void OnEnable()
    {
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.PlaySoundEvent += OnPlaySoundEvent;
    }
    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.PlaySoundEvent -= OnPlaySoundEvent;
    }
    private void Start()
    {
        string currentScene = "Menu";
        SceneMusicDetials sceneMusic = sceneMuice_SO.GetSceneMusicDetials(currentScene);
        if (sceneMusic == null) return;
        AudioDitals ambient = audioDetil_SO.GetAudioDitals(sceneMusic.ambientName);
        AudioDitals music = audioDetil_SO.GetAudioDitals(sceneMusic.musicName);

        if (soundRoutine != null)
            StopCoroutine(soundRoutine);
        soundRoutine = StartCoroutine(PlaySoundRoutine(ambient, music));
    }
    void OnPlaySoundEvent(AudioName audioName)
    {
        AudioDitals audioDitals = audioDetil_SO.GetAudioDitals(audioName);
        if(audioDitals != null)
            EventHandler.CallInitSoundEffectEvent(audioDitals);
    }
    void OnAfterSceneLoadedEvent()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneMusicDetials sceneMusicDetials = sceneMuice_SO.GetSceneMusicDetials(currentScene);
        if (sceneMusicDetials == null) return;
        AudioDitals ambient = audioDetil_SO.GetAudioDitals(sceneMusicDetials.ambientName);
        AudioDitals music = audioDetil_SO.GetAudioDitals(sceneMusicDetials.musicName);
        if (soundRoutine != null)
            StopCoroutine(soundRoutine);
        soundRoutine = StartCoroutine(PlaySoundRoutine(ambient, music));
    }
    IEnumerator PlaySoundRoutine(AudioDitals ambient,AudioDitals music)
    {
        PlayAmbientClip(ambient);
        yield return new WaitForSeconds(playDelayTime);
        PlayMusicClip(music);
    }
    void PlayMusicClip(AudioDitals music)
    {
        audioMixer.SetFloat("MusicVolume", ConcertSoundVolume(music.volume));
        musicSource.clip = music.audioClip;
        if (musicSource.isActiveAndEnabled)
            musicSource.Play();
        normalSnapShot.TransitionTo(1f);
    }
    void PlayAmbientClip(AudioDitals ambient)
    {
        audioMixer.SetFloat("AmbientVolume", ConcertSoundVolume(ambient.volume));
        ambientSource.clip = ambient.audioClip;
        if(ambientSource.isActiveAndEnabled)
            ambientSource.Play();
        ambientOnlySnapShot.TransitionTo(1f);
    }
    float ConcertSoundVolume(float amount)
    {
        return (amount * 100 - 80);
    }

}

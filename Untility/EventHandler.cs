using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHandler
{
    public static event Action MiniGameFinishEvent;
    public static void CallMiniGameFinishedEvent()
    {
        MiniGameFinishEvent?.Invoke();
    }
    public static event Action<string, string> SceneTransionEvent;
    public static void CallSceneTransionEvent(string from, string to)
    {
        SceneTransionEvent?.Invoke(from, to);
    }
    public static event Action AfterSceneLoadedEvent;
    public static  void CallAfterSceneLoadedEvent()
    {
        AfterSceneLoadedEvent?.Invoke();
    }
    public static event Action StartNewGameEvent;
    public static void CallStartNewGameEvent()
    {
        StartNewGameEvent?.Invoke();
    }
    public static event Action<AudioName> PlaySoundEvent;
    public static void CallPlaySoundEvent(AudioName audioName)
    {
        PlaySoundEvent?.Invoke(audioName);
    }
    public static event Action<AudioDitals> InitSoundEffectEvent;
    public static void CallInitSoundEffectEvent(AudioDitals audioDitals)
    {
        InitSoundEffectEvent?.Invoke(audioDitals);
    }
}

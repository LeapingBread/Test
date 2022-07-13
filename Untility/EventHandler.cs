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
}

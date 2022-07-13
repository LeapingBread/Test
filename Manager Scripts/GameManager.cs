using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnFishedEvent;

    private void OnEnable()
    {
        EventHandler.MiniGameFinishEvent += OnMiniGameFinishEvent;
    }
    private void OnDisable()
    {
        EventHandler.MiniGameFinishEvent -= OnMiniGameFinishEvent;
    }
    void OnMiniGameFinishEvent()
    {
        OnFishedEvent?.Invoke();
       
    }
}

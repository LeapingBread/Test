using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChange : MonoBehaviour
{
    [SerializeField] string from;
    [SerializeField] string to;
    public void NextLevel()
    {
        EventHandler.CallSceneTransionEvent(from,to);
    }
}

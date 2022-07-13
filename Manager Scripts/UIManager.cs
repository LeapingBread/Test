using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   public void BackToMainMenu()
    {
        
        EventHandler.CallSceneTransionEvent(SceneManager.GetActiveScene().name, "Menu");
        //SaveLoadManager.Instance.Save();

    }
    public void StartNewGame()
    {
        EventHandler.CallStartNewGameEvent();
    }
    public void LoadGame()
    {
        SaveLoadManager.Instance.Load();
    }
    public void Quit()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] AudioName buttonSoundFX;
   public void BackToMainMenu()
    {
        if (buttonSoundFX != AudioName.None)
            EventHandler.CallPlaySoundEvent(buttonSoundFX);
        EventHandler.CallSceneTransionEvent(SceneManager.GetActiveScene().name, "Menu");
        //SaveLoadManager.Instance.Save();

    }
    public void StartNewGame()
    {
        if (buttonSoundFX != AudioName.None)
            EventHandler.CallPlaySoundEvent(buttonSoundFX);
        EventHandler.CallStartNewGameEvent();
    }
    public void LoadGame()
    {
        if (buttonSoundFX != AudioName.None)
            EventHandler.CallPlaySoundEvent(buttonSoundFX);
        SaveLoadManager.Instance.Load();
    }
    public void Quit()
    {
        if (buttonSoundFX != AudioName.None)
            EventHandler.CallPlaySoundEvent(buttonSoundFX);
        Application.Quit();
    }
}

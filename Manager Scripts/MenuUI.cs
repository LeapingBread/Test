using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] AudioName buttonSoundFX;
    public void BackToMainMenu()
    {
        if (buttonSoundFX != AudioName.None)
            EventHandler.CallPlaySoundEvent(buttonSoundFX);
        EventHandler.CallSceneTransionEvent(SceneManager.GetActiveScene().name, "Menu");
       // SaveLoadManager.Instance.Save();

    }
}

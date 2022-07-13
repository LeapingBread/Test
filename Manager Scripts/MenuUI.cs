using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void BackToMainMenu()
    {
        
        EventHandler.CallSceneTransionEvent(SceneManager.GetActiveScene().name, "Menu");
       // SaveLoadManager.Instance.Save();

    }
}

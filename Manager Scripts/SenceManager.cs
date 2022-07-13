using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SenceManager : MonoBehaviour,Isaveable
{
    [SerializeField]CanvasGroup faderCanvasGroup;
    bool isFading;
    [SerializeField] float fadeDuartion;
    private void Start()
    {
        Isaveable saveable = this;
        saveable.SaveableRegister();
        //(SceneTrasionRoutain(string.Empty, "Menu"));
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
    }
    private void OnEnable()
    {
        EventHandler.SceneTransionEvent += OnSceneTransionEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }
    private void OnDisable()
    {
        EventHandler.SceneTransionEvent -= OnSceneTransionEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }
    void OnStartNewGameEvent()
    {
        StartCoroutine(SceneTrasionRoutain("Menu", "L1"));
    }
    void OnSceneTransionEvent(string from, string to)
    {
        StartCoroutine(SceneTrasionRoutain(from, to));
    }
    IEnumerator SceneTrasionRoutain(string from, string to)
    {
        yield return FadingRoutain(1);//black
        if (from != string.Empty)
        {
            SaveLoadManager.Instance.Save();
            yield return SceneManager.UnloadSceneAsync(from);
        }
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount-1);
        SceneManager.SetActiveScene(newScene);
        EventHandler.CallAfterSceneLoadedEvent();
        yield return FadingRoutain(0);//white
    }
    IEnumerator FadingRoutain(int targetAlpha)
    {
        isFading = true;
        faderCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(faderCanvasGroup.alpha - targetAlpha) / fadeDuartion;
        while(!Mathf.Approximately(faderCanvasGroup.alpha,targetAlpha))
        {
            faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha,targetAlpha,speed*Time.deltaTime);
            yield return null;
        }
        faderCanvasGroup.blocksRaycasts = false;
        isFading = false;
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData data = new GameSaveData();
        data.currentScene = SceneManager.GetActiveScene().name;
        return data;
    }

    public void RestorGameData(GameSaveData data)
    {
        StartCoroutine(SceneTrasionRoutain("Menu", data.currentScene));
    }
}

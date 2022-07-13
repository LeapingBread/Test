using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    string jsonFolder;
    List<Isaveable>saveableList = new List<Isaveable>();
    Dictionary<string, GameSaveData> saveDataDict = new Dictionary<string, GameSaveData>();
    public void Register(Isaveable isaveable)
    {
        saveableList.Add(isaveable);
    }
    protected override void Awake()
    {
        base.Awake();
        jsonFolder = Application.persistentDataPath + "/SAVE/";
        Debug.Log(Application.persistentDataPath);
    }
    private void OnEnable()
    {
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
        
    }
    private void OnDisable()
    {
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
        
    }
    
    void OnStartNewGameEvent()
    {
        var saveDataPath = jsonFolder + "data.sav";
        if (File.Exists(saveDataPath))
            File.Delete(saveDataPath);
    }
    public void Save()
    {
        saveDataDict.Clear();
        foreach (var saveable in saveableList)
        {
            saveDataDict.Add(saveable.GetType().Name, saveable.GenerateSaveData());
        }
        var saveDataPath = jsonFolder + "data.sav";
        var jsonData = JsonConvert.SerializeObject(saveDataDict, Formatting.Indented);
        if(!File.Exists(saveDataPath))
        {
            Directory.CreateDirectory(jsonFolder);
        }
        File.WriteAllText(saveDataPath, jsonData);
        
    }
    public void Load()
    {
        var saveDataPath = jsonFolder + "data.sav";
        if (!File.Exists(saveDataPath)) return; 
        var stringData = File.ReadAllText(saveDataPath);
        var jsonData = JsonConvert.DeserializeObject<Dictionary<string, GameSaveData>>(stringData);
        foreach (var saveable in saveableList)
        {
            saveable.RestorGameData(jsonData[saveable.GetType().Name]);
        }
    }
}

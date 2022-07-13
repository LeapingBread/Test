using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SceneMusic_SO",menuName ="SceneMusic_SO")]
public class SceneMuice_SO : ScriptableObject
{
    public List<SceneMusicDetials> musicDetials = new List<SceneMusicDetials>();
    public SceneMusicDetials GetSceneMusicDetials(string sceneName)
    {
        return musicDetials.Find(s => s.sceneName == sceneName);
    }
}
[System.Serializable]
public class SceneMusicDetials
{
    public string sceneName;
    public AudioName musicName;
    public AudioName ambientName;
}

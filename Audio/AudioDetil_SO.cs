using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "AudioDetials",menuName = "AudioDetials")]
public class AudioDetil_SO : ScriptableObject
{
    public List<AudioDitals> audioDitalsList;
    public AudioDitals GetAudioDitals (AudioName audioName)
    {
       return audioDitalsList.Find(a => a.audioName == audioName);
    }
}
[System.Serializable]
public class AudioDitals
{
    public AudioName audioName;
    public AudioClip audioClip;
    public float volume;
    public float pitch;
}

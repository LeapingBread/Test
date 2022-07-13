using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundItem : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public void SetSound(AudioDitals audioDitals)
    {
        audioSource.clip = audioDitals.audioClip;
        audioSource.volume = audioDitals.volume;
        audioSource.pitch = audioDitals.pitch;
    }
}

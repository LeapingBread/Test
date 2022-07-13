using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] GameObject[] poolPerfeb;
    Queue<GameObject> soundQueue = new Queue<GameObject>();
    private void OnEnable()
    {
        EventHandler.InitSoundEffectEvent += OnInitSoundEffect;
    }
    private void OnDisable()
    {
        EventHandler.InitSoundEffectEvent -= OnInitSoundEffect;
    }
    void CreatSoundQueue()
    {
        var parent = new GameObject(poolPerfeb[0].name).transform;
        parent.SetParent(transform);
        for(int i =0;i<20;i++)
        {
            GameObject newObj = Instantiate(poolPerfeb[0], parent);
            newObj.SetActive(false);
            soundQueue.Enqueue(newObj);
        }
    }
    GameObject GetQueueObject()
    {
        if (soundQueue.Count < 2)
            CreatSoundQueue();
        return soundQueue.Dequeue();
    }
    void OnInitSoundEffect(AudioDitals audioDitals)
    {
        var obj = GetQueueObject();
        obj.GetComponent<SoundItem>().SetSound(audioDitals);
        obj.SetActive(true);
        StartCoroutine(DisableSound(obj, audioDitals.audioClip.length));
    }
    IEnumerator DisableSound(GameObject obj,float duration)
    {
        yield return new WaitForSeconds(duration);
        obj.SetActive(false);
        soundQueue.Enqueue(obj);
    }
}

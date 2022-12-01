using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomAudioClip : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> clips = default;

    new AudioSource audio;

    public void PlayRandom()
    {
        int index = Random.Range(0, clips.Count);
        AudioClip clip = clips[index];
        // Debug.Log(clip);
        audio.PlayOneShot(clip);
    }

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
}

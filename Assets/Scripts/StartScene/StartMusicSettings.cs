using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMusicSettings : MonoBehaviour
{
     AudioSource AudioSource;
     Music music;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        music = FindAnyObjectByType<Music>();
    }

    // Update is called once per frame
    void Update()
    {
        AudioSource.volume = music.MusicValue;
    }
}

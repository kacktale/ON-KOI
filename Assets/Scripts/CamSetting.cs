using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CamSetting : MonoBehaviour
{
    public Music Music;
    public PostProcessVolume PostProcessVolume;
    public Bloom bloom;

    // Start is called before the first frame update
    void Start()
    {
        Music = FindAnyObjectByType<Music>();
        PostProcessVolume.profile.TryGetSettings(out bloom);
    }

    // Update is called once per frame
    void Update()
    {
        PostProcessVolume.weight = Music.PostValue;
        bloom.intensity.value = Music.BloomValue;
    }
}

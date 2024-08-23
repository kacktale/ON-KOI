using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLightDefault : MonoBehaviour
{
    public Music Music;
    public Slider Slider;

    public void Awake()
    {
        Music = FindAnyObjectByType<Music>();
    }

    public void SetDefault()
    {
        Music.BloomValue = 5.11f;
        Slider.value = 5.11f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPannul : MonoBehaviour
{
    public Slider slider;
    public Music Music;

    public void MusicVol()
    {
        Music.MusicValue = slider.value;
    }
    public void SoundEffectVol()
    {
        Music.SoundEffectValue = slider.value;
    }
}

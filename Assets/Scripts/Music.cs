using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public float MusicValue = 1f;
    public float SoundEffectValue = 1f;
    public float PostValue = 1f;
    public float BloomValue = 5.11f;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get;private set; }
    private AudioSource audios;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        audios = GetComponent<AudioSource>();
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void AudioPlay(AudioClip clip)
    {
        audios.PlayOneShot(clip);
    }
}
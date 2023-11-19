using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameSoundManager : MonoBehaviour
{
    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        this.mixer.SetFloat("MasterAudioMixer", PlayerPrefs.GetFloat("MasterAudioMixer"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("MasterAudioMixer");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateMusicVolume()
    {
        PlayerPrefs.SetFloat("MasterAudioMixer", volumeSlider.value);
    }
}

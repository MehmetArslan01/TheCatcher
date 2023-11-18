using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;

    // Start is called before the first frame update
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

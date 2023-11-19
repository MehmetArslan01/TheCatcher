using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource audioSource;

    [SerializeField]
    private AudioSource soundEffectObject;

    private void Awake()
    {
        // TODO: Create singleton instance
        if (Instance == null) { 
            Instance = this; 
            DontDestroyOnLoad(gameObject);
            }else{
                Destroy(gameObject);
            }
    }

    public void PlaySound(AudioClip clip, Transform spawn, float volume)
    {
        // TODO: Create SoundEffectObject instance and play soundeffect
        this.audioSource = Instantiate(soundEffectObject, spawn.position, Quaternion.identity);
        this.audioSource.clip = clip;
        this.audioSource.volume = volume;
        this.audioSource.Play();
        Destroy(this.audioSource.gameObject, this.audioSource.clip.length);
    }
}

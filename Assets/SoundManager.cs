using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

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
        AudioSource audioSource = Instantiate(soundEffectObject, spawn.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
}

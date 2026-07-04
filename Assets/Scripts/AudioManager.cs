using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [Header("Audio Sources")]
    public AudioSource backgroundMusicSource;
    public AudioSource sfxSource;
    
    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip collisionSound;
    public AudioClip gameOverSound;
    public AudioClip winSound;
    public AudioClip coinSound;
    
    [Header("Settings")]
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;
    [Range(0f, 1f)]
    public float sfxVolume = 1f;
    
    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        // Setup audio sources if not assigned
        if (backgroundMusicSource == null)
        {
            backgroundMusicSource = gameObject.AddComponent<AudioSource>();
            backgroundMusicSource.loop = true;
        }
        
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.loop = false;
        }
    }
    
    void Start()
    {
        PlayBackgroundMusic();
    }
    
    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && backgroundMusicSource != null)
        {
            backgroundMusicSource.clip = backgroundMusic;
            backgroundMusicSource.volume = musicVolume;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
            Debug.Log("Background music started");
        }
        else
        {
            Debug.LogWarning("Background music or audio source is not assigned!");
        }
    }
    
    public void StopBackgroundMusic()
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.Stop();
        }
    }
    
    public void PlayCollisionSound()
    {
        if (collisionSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(collisionSound, sfxVolume);
            Debug.Log("Collision sound played");
        }
        else
        {
            Debug.LogWarning("Collision sound or SFX source is not assigned!");
        }
    }
    
    public void PlayGameOverSound()
    {
        if (gameOverSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(gameOverSound, sfxVolume);
            Debug.Log("Game over sound played");
        }
        else
        {
            Debug.LogWarning("Game over sound or SFX source is not assigned!");
        }
    }
    
    public void PlayWinSound()
    {
        if (winSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(winSound, sfxVolume);
            Debug.Log("Win sound played");
        }
        else
        {
            Debug.LogWarning("Win sound or SFX source is not assigned!");
        }
    }
    
    public void PlayCoinSound()
    {
        if (coinSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(coinSound, sfxVolume);
            Debug.Log("Coin collected!");
        }
        else
        {
            Debug.LogWarning("Coin sound or SFX source is not assigned!");
        }
    }
    
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.volume = musicVolume;
        }
    }
    
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }
}
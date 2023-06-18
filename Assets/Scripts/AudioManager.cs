using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
            }
                return instance;
        }
    }
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSoure;
    [SerializeField] AudioSource SFXSource;
    [Header("Audio Clip")]

    public AudioClip backGround;
    public AudioClip jump;
    public AudioClip btnPress;
    public AudioClip over;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        musicSoure.clip = backGround;
        musicSoure.Play();

    }
    public void OnJump()
    {
        SFXSource.clip = jump;
        SFXSource.Play();
    }
    public void OnPressBtn()
    {
        SFXSource.clip = btnPress;
        SFXSource.Play();
    }
    public void OnGameover()
    {
        SFXSource.clip = over;
        SFXSource.Play();
    }
    public void PauseBackgroundMusic(bool active)
    {
        if (active)
        musicSoure.Pause();
        else musicSoure.Play();
    }
    
}


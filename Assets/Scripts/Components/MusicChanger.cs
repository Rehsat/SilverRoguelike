using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    private AudioSource _musicSource;
    public float dasdal;
    private void Awake()
    {
        _musicSource = FindObjectOfType<AudioSource>();
    }
    public void ChangeMusic(AudioClip newMusic)
    {
        _musicSource.clip = newMusic;
        _musicSource.Play();
    }
}

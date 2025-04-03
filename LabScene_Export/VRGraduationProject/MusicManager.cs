using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance{get ; private set;}

    [SerializeField] private AudioClip _schoolBellRing;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        Instance = this;
    }

    private void Start()
    {
        _audioSource.Play();
    }

    /// <summary>
    /// 將音樂更換成學校鐘聲
    /// </summary>
    public void ChangeAudio()
    {
        _audioSource.clip = _schoolBellRing;

        _audioSource.Play();
    }
}

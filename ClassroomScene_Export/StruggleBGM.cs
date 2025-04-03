using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StruggleBGM : MonoBehaviour
{
    public static StruggleBGM Instance {get ; private set;}

    /// <summary>
    /// 音訊來源
    /// </summary>
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        Instance = this;

        if(_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }

    public void Play()
    {
        _audioSource.Play();
    }

    public void Stop()
    {
        _audioSource.Stop();
    }
}

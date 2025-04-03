using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandAnime : MonoBehaviour
{
    public static WaveHandAnime Instance{get ; private set ;}

    private Animator animator;
    private const string WAVE_HAND = "Interact";

    private void Awake(){
        Instance = this;
        animator = GetComponent<Animator>();
    }

    private void Start(){
        Debug.Log(animator);
    }

    public static void WAVEHAND_STATIC(){
        Instance.WAVEHAND();
    }

    private void WAVEHAND(){
        animator.SetTrigger(WAVE_HAND);
    }
}

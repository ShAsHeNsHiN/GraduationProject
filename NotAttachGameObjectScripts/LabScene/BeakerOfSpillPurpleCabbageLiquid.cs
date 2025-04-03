using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeakerOfSpillPurpleCabbageLiquid : MonoBehaviour
{
    public static BeakerOfSpillPurpleCabbageLiquid Instance{get ; private set;}

    private new ParticleSystem particleSystem;

    private bool filterPurpleCabbageLiquid;

    private float filterTimes = 100f;

    private void Awake()
    {
        Instance = this;

        filterPurpleCabbageLiquid = false;

        particleSystem = GetComponent<ParticleSystem>();

        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(filterPurpleCabbageLiquid)
        {
            particleSystem.Stop();
        }

        else
        {
            if(transform.rotation.x > -.5f && transform.rotation.x < .7f)
            {
            // Debug.Log("true");
                if(!particleSystem.isPlaying)
                {
                    // Debug.Log("is your problem , fuck!");
                    particleSystem.Play();
                }
            }

            else
            {
                particleSystem.Stop();
            }
        }
    }

    private void OnParticleTrigger()
    {
        filterTimes--;

        if(filterTimes < 0)
        {
            filterPurpleCabbageLiquid = true;
        }
    }

    public bool FilterPurpleCabbageLiquid()
    {
        return filterPurpleCabbageLiquid;
    }

    public void SpillPurpleCabbageLiquidActive()
    {
        gameObject.SetActive(true);
    }
}

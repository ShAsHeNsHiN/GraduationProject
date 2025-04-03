using System.Collections.Generic;
using UnityEngine;

public class DropperOfSpillPurpleCabbageLiquid : MonoBehaviour
{
    public static DropperOfSpillPurpleCabbageLiquid Instance{get; private set;}

    private new ParticleSystem particleSystem;

    private bool touchSolution;

    private float releaseTimes = 100f;

    private ParticleSystem.TriggerModule jj;

    private void Awake()
    {
        Instance = this;

        particleSystem = GetComponent<ParticleSystem>();

        touchSolution = false;
    }

    private void Start()
    {
        // when release the liquid to detection solution , enable the collison
        // var root = particleSystem.collision;
        // root.enabled = true;
        
    }

    private void Update()
    {
        // if(transform.rotation.z > -.36f && transform.rotation.z < .36f){
        //     // Debug.Log("true");
        //     if(!particleSystem.isPlaying){
        //         // Debug.Log("is your problem , fuck!");
        //         particleSystem.Play();
        //     }
        // }
        // else{
        //     particleSystem.Stop();
        // }
    }

    public ParticleSystem GetParticleSystem()
    {
        return particleSystem;
    }

    private void OnParticleTrigger()
    {
        touchSolution = true;

        var jj = particleSystem.trigger;

        jj.colliderQueryMode = ParticleSystemColliderQueryMode.One;

        List<ParticleSystem.Particle> particles = new();

        particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter , particles , out var colliderData);
    }

    private void OnParticleCollision(GameObject other)
    {
        print(other);
    }

    public bool LiquidTouchSolution()
    {
        return touchSolution;
    }

    public bool LiquidRelease()
    {
        touchSolution = false;

        return touchSolution;
    }
}

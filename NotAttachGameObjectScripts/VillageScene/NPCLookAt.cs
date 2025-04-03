using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class NPCLookAt : MonoBehaviour
{
    [SerializeField] private Rig rig;
    [SerializeField] private Transform targetTransform;

    private RigBuilder rigBuilder;

    private bool isLookingAtPosition;

    private float targetWeight;

    private Action<bool> enableRigBuilder;

    private float timer = 1f;

    private void Awake(){
        rigBuilder = GetComponent<RigBuilder>();
        
    }

    private void Start(){
        // rigBuilder.enabled = false;
    }

    private void OnTriggerEnter(Collider other){
        if(other.GetComponent<MeshCollider>()){
            Debug.Log("HI");
        }
    }

    private void Update(){
        timer -= Time.deltaTime;
        if(timer <= 0f){
            rigBuilder.enabled = true;
        }

        targetWeight = isLookingAtPosition ? 1f : 0f;
        float lerpSpeed = 2f;
        rig.weight = Mathf.Lerp(rig.weight , targetWeight , Time.deltaTime * lerpSpeed);
    }

    public void LookAtUpperBodyPosition(Vector3 lookAtUpperBodyPosition){
        isLookingAtPosition = true;
        rig.weight = 0;
        targetTransform.position = lookAtUpperBodyPosition;
        // rig.weight = Mathf.Lerp(rig.weight , 1 , Time.deltaTime * 10f);
    }

    // public void LookAtLowerBodyPosition(Vector3 lookAtLowerPosition){
    //     isLookingAtPosition = true;
    //     rig.weight = 0;
    //     targetTransform.position = lookAtLowerPosition;
    //     // rig.weight = Mathf.Lerp(rig.weight , 1 , Time.deltaTime * 10f);
    // }
}

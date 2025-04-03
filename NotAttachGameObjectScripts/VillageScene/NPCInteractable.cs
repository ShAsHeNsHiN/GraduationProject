using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class NPCInteractable : MonoBehaviour , IInteractable
{
    private NPCLookAt nPCLookAt;
    [SerializeField] private Transform chatBubbleInstantiate;

    [Multiline]
    [SerializeField] private string interactText;

    private void Awake(){
        nPCLookAt = GetComponent<NPCLookAt>();
    }

    public void Interact(Transform interactorTransform){
        // Debug.Log(GameAsset.Instance.GetPfChatBubble());
        // ChatBubble for Robot(local position)
        // ChatBubble.Create(transform , new Vector3(-0.83f , 1.7899f , 0.03f) , ChatBubble.Emotion.Happy , "Hello" , .2f);
        // ChatBubble for Remy
        ChatBubble.Create(chatBubbleInstantiate , Vector3.zero , ChatBubble.Emotion.Happy , "Hello" , .2f);

        float playerHeight = .5f;
        nPCLookAt.LookAtUpperBodyPosition(interactorTransform.position + Vector3.up * playerHeight);
        // nPCLookAt.LookAtLowerBodyPosition(interactorTransform.position - Vector3.up * playerHeight);
    }

    public string GetInteractText(){
        return interactText;
    }

    public Transform GetTransform(){
        return transform;
    }
}

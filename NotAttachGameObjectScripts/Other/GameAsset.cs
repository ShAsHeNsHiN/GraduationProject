using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GameAsset : MonoBehaviour
{
    public static GameAsset Instance{get ; private set ;}

    [SerializeField] private Transform pfChatBubble;

    private void Awake()
    {
        Instance = this;
    }

    public Transform GetPfChatBubble()
    {
        return pfChatBubble;
    }
}

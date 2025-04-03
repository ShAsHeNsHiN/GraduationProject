using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSixthDice : MonoBehaviour
{
    public static ControlSixthDice Instance{get ; private set;}

    [Header("Sixth Dice Base Transform")]
    [SerializeField] private Transform _baseTransform;
    [SerializeField] private Material _baseMaterial;
    [SerializeField] private Material _sealedMaterial;

    private const float DESIRED_DURATION = 1f;

    private const string POKEBUTTON = "PokeButton";

    private float _elapsedTime;

    public List<ReleaseSealed> PokeButtonReleaseList {get ; private set;} = new();

    private MeshRenderer _meshRenderer;

    public event Action OnDiceCanBePickedUp;

    private void Awake()
    {
        Instance = this;

        _meshRenderer = _baseTransform.GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        foreach (Transform child in transform)
        {
            if(child.CompareTag(POKEBUTTON))
            {
                PokeButtonReleaseList.Add(child.GetComponent<ReleaseSealed>());
            }
        }

        _meshRenderer.sharedMaterial.color = _sealedMaterial.color;

        OnDiceCanBePickedUp += Handle_NpcTalkingSixthDice;

        WallQuiz.Instance.OnAllQuestionClear += () => 
        {
            Destroy(gameObject);
        };
    }

    private void Update()
    {
        if(AllPokeButtonIsActive())
        {
            _elapsedTime += Time.deltaTime;

            float percentageComplete = _elapsedTime / DESIRED_DURATION;

            if(percentageComplete <= 1)
            {
                _meshRenderer.sharedMaterial.color = Color.Lerp(_sealedMaterial.color , _baseMaterial.color , percentageComplete);
            }
            
            else
            {
                // After color changed
                OnDiceCanBePickedUp?.Invoke();

                // *停止 Update()
                enabled = false;
            }
        }
    }

    private void Handle_NpcTalkingSixthDice()
    {
        StartCoroutine(NpcTalkingSixthDice());
    }

    private IEnumerator NpcTalkingSixthDice()
    {
        NPCUI_LabScene.Instance.NpcTalking(EPlayingProgress.PickingSixthDiceFirst , .1f);
        yield return null;
    }

    private bool AllPokeButtonIsActive()
    {
        bool allPokeButtonActive = true;

        foreach (var item in PokeButtonReleaseList)
        {
            allPokeButtonActive = item.SuccessfulActive;

            if(!allPokeButtonActive)
            {
                break;
            }
        }

        return allPokeButtonActive;
    }

    private void OnDestroy()
    {
        OnDiceCanBePickedUp = null;
    }
}

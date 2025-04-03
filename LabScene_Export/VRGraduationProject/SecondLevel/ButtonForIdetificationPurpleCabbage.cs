using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class ButtonForIdetificationPurpleCabbage : XRSimpleInteractable
{
    public static ButtonForIdetificationPurpleCabbage Instance{get ; private set;}

    [SerializeField] private List<GameObject> _visualGameObject;

    public event Action OnClickButton;

    protected override void Awake()
    {
        base.Awake();

        Instance = this;

        Hide();
    }

    private void Start()
    {
        ControlSixthDice.Instance.OnDiceCanBePickedUp += Show;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        OnClickButton?.Invoke();

        NPCUI_LabScene.Instance.NpcTalking(EPlayingProgress.QuizAppearFirst , .1f , SecondDialogue);

        base.OnSelectEntered(args);
    }

    private void SecondDialogue()
    {
        StartCoroutine(NPCUI_LabScene.Instance.ContinueTalking(EPlayingProgress.QuizAppearSecond , .1f , .5f , ThirdDialogue));
    }

    private void ThirdDialogue()
    {
        StartCoroutine(NPCUI_LabScene.Instance.ContinueTalking(EPlayingProgress.QuizAppearThird , .1f , .5f));
    }

    private void Show()
    {
        _visualGameObject.ForEach(item =>
        {
            item.SetActive(true);
        });
    }

    private void Hide()
    {
        _visualGameObject.ForEach(item =>
        {
            item.SetActive(false);
        });
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        OnClickButton = null;
    }
}

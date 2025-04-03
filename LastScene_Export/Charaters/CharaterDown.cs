using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

public class CharaterDown : Charater
{
    public static CharaterDown Instance { get; private set; }

    private readonly List<string> _charaters = new();

    protected override void Awake()
    {
        base.Awake();

        Instance = this;
    }

    protected override void Start()
    {
        base.Start();

        OnTypingEnd += Handle_TypingEnd;
    }

    public override IEnumerator TypingDialogue()
    {
        TypingStart();

        string charater = _charaterDialogueList.Dequeue();

        _charaters.Add(charater);

        CharaterChecked(Enum.Parse<ECharater>(charater));

        string dialogue = _charaterDialogueList.Dequeue();

        for (int i = 0; i < dialogue.Length; i++)
        {
            _charaterText.text += dialogue[i];
            yield return new WaitForSeconds(_typingSpeed);
        }

        TypingEnd();
    }

    private void Handle_TypingEnd()
    {
        int sameDialogueIndex = 4;

        if(_charaters.Count != sameDialogueIndex)
        {
            LeftKey.Instance.OnKeyPressed -= DialogueController.Instance.Handle_ShowDialogue;

            LeftKey.Instance.OnKeyPressed += DialogueController.Instance.Handle_SwitchFragment;

            RightKey.Instance.OnKeyPressed -= DialogueController.Instance.Handle_ShowDialogue;

            RightKey.Instance.OnKeyPressed += DialogueController.Instance.Handle_SwitchFragment;
        }
    }
}

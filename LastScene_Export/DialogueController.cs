using UnityEngine;
using System;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance { get; private set;}

    private Charater _currentCharater = default;

    public event Action OnDialogueEnd;

    public event Action OnSwitchFragmentFinished;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnDialogueEnd += Handle_DialogueEnd;

        LeftKey.Instance.OnKeyPressed += Handle_ShowDialogue;

        RightKey.Instance.OnKeyPressed += Handle_ShowDialogue;

        OnSwitchFragmentFinished += Handle_SwitchFragmentFinished;
    }

    private void CharaterSpeaking(Charater charater)
    {
        _currentCharater = charater;

        charater.Show();

        StartCoroutine(charater.TypingDialogue());
    }

    /// <summary>
    /// 顯示角色對話
    /// </summary>
    public void Handle_ShowDialogue()
    {
        bool charaterTopDialogueListEmpty = CharaterTop.Instance.CharaterDialogueList.Count == 0;

        bool charaterDownDialogueListEmpty = CharaterDown.Instance.CharaterDialogueList.Count == 0;

        if(charaterTopDialogueListEmpty && charaterDownDialogueListEmpty)
        {
            CharaterSpeaking(CharaterCenter.Instance);
        }

        else
        {
            // 首次執行角色對話
            if(_currentCharater == null)
            {
                CharaterSpeaking(CharaterTop.Instance);
            }

            // 非首次執行角色對話
            // *這裡是用切換的方式進行角色對話
            else
            {
                if(_currentCharater == CharaterDown.Instance)
                {
                    CharaterSpeaking(CharaterTop.Instance);
                }

                else
                {
                    CharaterSpeaking(CharaterDown.Instance);
                }
            }
        }

        bool charaterCenterDialogueListEmpty = CharaterCenter.Instance.CharaterDialogueList.Count == 0;

        if(charaterTopDialogueListEmpty && charaterCenterDialogueListEmpty && charaterDownDialogueListEmpty)
        {
            // 對話結束
            OnDialogueEnd?.Invoke();
        }
    }

    /// <summary>
    /// 下一段對話轉場特效
    /// </summary>
    public void Handle_SwitchFragment()
    {
        SwitchFragment();
    }

    /// <summary>
    /// 對話結束時要把訂閱的對話事件移除
    /// </summary>
    private void Handle_DialogueEnd()
    {
        LeftKey.Instance.OnKeyPressed -= Handle_ShowDialogue;

        RightKey.Instance.OnKeyPressed -= Handle_ShowDialogue;
    }

    private void HideCharaters(params Charater[] charaters)
    {
        foreach (var item in charaters)
        {
            item.Hide();
        }
    }

    private void Handle_SwitchFragmentFinished()
    {
        LeftKey.Instance.OnKeyPressed += Handle_ShowDialogue;

        LeftKey.Instance.OnKeyPressed -= Handle_SwitchFragment;

        RightKey.Instance.OnKeyPressed += Handle_ShowDialogue;

        RightKey.Instance.OnKeyPressed -= Handle_SwitchFragment;
    }

    /// <summary>
    /// 下一段對話轉場特效
    /// </summary>
    private void SwitchFragment()
    {
        // FaderScreen.Instance.FadeIn();

        // yield return new WaitForSeconds(FaderScreen.Instance.EffectDuration);

        // *哪時有空再把 FadeIn 補回來好了

        HideCharaters(CharaterTop.Instance , CharaterDown.Instance , CharaterCenter.Instance);

        FaderScreen.Instance.FadeOut();

        OnSwitchFragmentFinished?.Invoke();
    }

    // *在轉場的過程，控制按鈕會呈現:隱藏>>>>>顯示>>>>隱藏>>>>>顯示。正常來說，我只想要:隱藏>>>>顯示這樣子

    private void OnDestroy()
    {
        OnDialogueEnd = null;
        OnSwitchFragmentFinished = null;
    }
}

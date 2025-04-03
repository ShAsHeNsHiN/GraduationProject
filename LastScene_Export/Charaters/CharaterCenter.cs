public class CharaterCenter : Charater
{
    public static CharaterCenter Instance { get; private set; }

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

    private void Handle_TypingEnd()
    {
        LeftKey.Instance.OnKeyPressed -= DialogueController.Instance.Handle_ShowDialogue;

        LeftKey.Instance.OnKeyPressed += DialogueController.Instance.Handle_SwitchFragment;

        RightKey.Instance.OnKeyPressed -= DialogueController.Instance.Handle_ShowDialogue;

        RightKey.Instance.OnKeyPressed += DialogueController.Instance.Handle_SwitchFragment;
    }
}

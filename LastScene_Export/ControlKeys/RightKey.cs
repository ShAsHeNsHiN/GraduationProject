public class RightKey : ControlKey
{
    public static RightKey Instance {get ; private set;}

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DialogueController.Instance.OnDialogueEnd += Handle_UpdateKeyText;

        DialogueController.Instance.OnDialogueEnd += () => 
        {
            OnKeyPressed += Handle_PlayAgain;
        };
    }

    protected override void Handle_UpdateKeyText()
    {
        transform.Find(LEAVE_KEY_TEXT).GetComponent<TMPro.TextMeshProUGUI>().text = "重新開始";
    }

    private void Handle_PlayAgain()
    {
        Loader.Load(Loader.Scene.ClassroomScene);
    }
}

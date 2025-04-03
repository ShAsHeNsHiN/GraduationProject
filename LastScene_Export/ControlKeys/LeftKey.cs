using UnityEditor;
using UnityEngine;

public class LeftKey : ControlKey
{
    public static LeftKey Instance {get ; private set;}

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DialogueController.Instance.OnDialogueEnd += Handle_UpdateKeyText;

        DialogueController.Instance.OnDialogueEnd += () => 
        {
            OnKeyPressed += Handle_GameEnd;
        };
    }

    protected override void Handle_UpdateKeyText()
    {
        transform.Find(LEAVE_KEY_TEXT).GetComponent<TMPro.TextMeshProUGUI>().text = "結束遊玩";
    }

    private void Handle_GameEnd()
    {
        Application.Quit();

        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #endif
    }
}

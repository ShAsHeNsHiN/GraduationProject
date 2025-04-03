using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class TheStoryWriter : MonoBehaviour
{
    public static TheStoryWriter Instance{get ; private set ;}

    // [SerializeField] private TextMeshProUGUI theStoryText;
    [SerializeField] private RectTransform backgroundRectTransform;
    [SerializeField] private AdvancedText advancedText;
    [SerializeField] private Button quicklyDisplayTextButton;

    private int index;
    private bool typing;
    private string[] TheStoryWord = new string []
    {
       "我叫做阿泓，是個小五生",
       "現在正在面臨一場我最不擅長的自然科考試",
       "因此我昨晚還多複習幾遍",
       "可惜…",
       "我依舊有兩題不會寫",
       "一題叫「黃金的價值」",
       "一題叫「紫甘藍與變色」" ,
       "而且桄添老師都再三強調會考了",
       "我竟然在這時掉鏈子" ,
       "好不甘心啊…" ,
       "………" ,
    //    "對了!" ,
    //    "既然我都這麼努力複習了" ,
    //    "說不定能在記憶(夢境)中找到解答!" ,
    //    "*雖然桄添老師也曾講過" ,
    //    "記憶有時也可能是虛假的" ,
    //    "但此刻我也只能相信自己了!" ,
    //    "這一刻，阿泓開始神遊放空" ,
    //    "逐漸進入到自己的夢境(記憶)中…"
    };
    private float loadingTimer = 3f;
    private bool sceneComplete;

    private void Awake(){
        Instance = this;
        quicklyDisplayTextButton.onClick.AddListener(() => {
            if(typing){
                // Quickly Display
                // Debug.Log("IsMeTyping");
                advancedText.ShowWholeText();
                typing = false;
            }
            else{
                // Typing the word
                // Debug.Log("IsMeElse");
                Resizing(TheStoryWord[index]);
                advancedText.ShowTextByTyping();
                index++;
                typing = true;
            }
        });
    }

    private void Start(){
        Resizing(TheStoryWord[index]);
        advancedText.ShowTextByTyping();
        typing = true;
    }

    private void Update(){
        if(index >= TheStoryWord.Length && !typing){
            quicklyDisplayTextButton.onClick.RemoveAllListeners();
            GoToNextScene.Instance.Show();
        }

        if(!typing){
            Time.timeScale = 1f;
        }
    }

    private void Hide(){
        backgroundRectTransform.gameObject.SetActive(false);
    }

    private void Show(){
        gameObject.SetActive(true);
    }

    public bool EndTyping(){
        return typing = false;
    }

    private void Resizing(string text){
        advancedText.SetText(text);

        advancedText.ForceMeshUpdate();

        Vector2 textSize = advancedText.GetRenderedValues(false);

        Vector2 paddingSize = new(150,50);

        backgroundRectTransform.sizeDelta = textSize + paddingSize ;
    }
}

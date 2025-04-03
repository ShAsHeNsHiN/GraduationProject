using UnityEngine;
using System.Collections;

public class NPCUI_SecondScene : NPCUI
{
    private static NPCUI_SecondScene instance;
    public static NPCUI_SecondScene Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindAnyObjectByType<NPCUI_SecondScene>();
            }

            return instance;
        }
    }

    private TextWriterForTMPCanvas.TextWriterSingle _textWriterSingle;

    private float _transitionTimer = 1f;

    private bool _isPlaying;

    public int StoryContentLength => _storyContent.Length;

    private readonly string[] _storyContent =
    {
       "我叫做阿泓",
       "是位小五生" ,
       "此時正在期末考",
       "科目是我最不擅長的自然" ,
       "因此昨晚還多複習幾遍",
       "可惜…",
       "我仍然有兩題不會寫",
       "一題叫「黃金的價值」",
       "一題叫「紫甘藍與變色」" ,
       "桄添老師都再三強調會考了",
       "我竟然在這時掉鏈子" ,
       "好不甘心啊!!" ,
       "………" ,
    };

    private readonly string _shockingWord = "咦，怎麼突然看不見前方了!?";

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        ControlNpc(EActiveState.隱藏);
    }

    public void NpcTalking(int storyContentIndex)
    {
        ControlNpc(EActiveState.顯示);

        bool isStoryNotEnd = storyContentIndex < _storyContent.Length;

        if(isStoryNotEnd && !_isPlaying)
        {
            _isPlaying = true;

            TextWriterForTMPCanvas.AddWriter_Static(_message , _storyContent[storyContentIndex] , .1f , _messageBoxRectTransform , true , () => 
            {
                _isPlaying = false;

                StoryEndChecked(storyContentIndex);
            });
        }
    }

    private void StoryEndChecked(int storyContentIndex)
    {
        // *「^1」為取得陣列中最後一個元素
        if(_storyContent[storyContentIndex] == _storyContent[^1])
        {
            StartCoroutine(NpcTalkingLastDialogue());

            FaderScreen.Instance.FadeIn();
        }
    }

    private void Update()
    {
        if(_textWriterSingle != null && _textWriterSingle.Finish())
        {
            _transitionTimer -= Time.deltaTime;

            if(_transitionTimer < 0)
            {
                Loader.Load(Loader.Scene.LabScene);
            }
        }
    }

    /// <remarks>*_textWriterSingle 會在這邊被賦予值，Update() 裡的程式會開始執行</remarks>
    private IEnumerator NpcTalkingLastDialogue()
    {
        // I need half time to display charater emotion
        yield return new WaitForSecondsRealtime(FaderScreen.Instance.EffectDuration / 2);

        _textWriterSingle = TextWriterForTMPCanvas.AddWriter_Static(_message , _shockingWord , .1f , _messageBoxRectTransform , true , () => 
        {
            StruggleBGM.Instance.Stop();
        });
    }
}

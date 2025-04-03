using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCUI_LabScene : NPCUI
{
    private static NPCUI_LabScene instnace;
    public static NPCUI_LabScene Instance
    {
        get
        {
            if(instnace == null)
            {
                instnace = FindAnyObjectByType<NPCUI_LabScene>();
            }
            
            return instnace;
        }
    }

    private readonly string [] _npcMessage = 
    {
        // 初次進入場景
        "歡迎來到夢驗，阿泓!" ,
        "這裡是你曾經的記憶" , 

        // 給使用者提示
        "黃金，黃金，在哪裡?~" , 
        "沒有砝碼，真傷腦筋…" ,
        "拾取成功!~" ,
        "量筒需要水喔~" , 
        "切菜時間!" ,
        "手上好像出現一個東西!?" ,

        // 關卡一對話
        "請問，哪塊黃金的價值比較高?" ,
        "鑑定中…" ,
        "恭喜答對!" ,
        "不對哦。" ,

        "上方的六色骰好像可以拾取了!~" ,

        // 關卡二對話
        "右邊出現了一個考驗" ,
        "而且按鈕似乎被啟動了" ,
        "試試看用它來答題吧" ,

        "正解!" ,
        "錯了哦。"
    };

    private readonly Dictionary<EPlayingProgress , string> _npcMessageDictionary = new();

    protected override void Awake()
    {
        base.Awake();

        ControlNpc(EActiveState.隱藏);

        InitializeNpcMessageDictionary();
    }

    private void Start()
    {
        StartCoroutine(FadeOutEnd());
    }

    private void InitializeNpcMessageDictionary()
    {
        _npcMessageDictionary.Clear();

        for (int i = 0; i < _npcMessage.Length; i++)
        {
            _npcMessageDictionary.Add((EPlayingProgress)i , _npcMessage[i]);
        }
    }

    private IEnumerator FadeOutEnd()
    {
        yield return new WaitForSeconds(3f);

        NpcTalking(EPlayingProgress.StartFirst , .1f , SecondDialogue);
    }

    private void SecondDialogue()
    {
        StartCoroutine(ContinueTalking(EPlayingProgress.StartSecond , .1f , .5f , () => 
        {
            StartCoroutine(HideDelay(.5f));
        }));
    }

    /// <summary>
    /// 想要接續上一段對話可使用
    /// </summary>
    /// <param name="nextProgress">下一段對話</param>
    /// <param name="typingSpeed">打字速度</param>
    /// <param name="pauseTimer">停頓時間</param>
    /// <param name="onComplete">當文字顯示完畢時執行的回呼函式</param>
    /// <returns></returns>
    public IEnumerator ContinueTalking(EPlayingProgress nextProgress , float typingSpeed , float pauseTimer , Action onComplete = default)
    {
        yield return new WaitForSeconds(pauseTimer);
        
        NpcTalking(nextProgress , typingSpeed , onComplete);
    }
    
    private IEnumerator HideDelay(float waitingSecond)
    {
        yield return new WaitForSeconds(waitingSecond);

        ControlNpc(EActiveState.隱藏);
    }
    
    /// <summary>
    /// 用 NPC 根據使用者的操作給出回應
    /// </summary>
    /// <param name="ePlayingProgress">使用者的操作</param>
    /// <param name="typingSpeed">打字速度</param>
    /// <param name="onComplete">當文字顯示完畢時執行的回呼函式</param>
    public void NpcTalking(EPlayingProgress ePlayingProgress , float typingSpeed , Action onComplete = default)
    {
        ControlNpc(EActiveState.顯示);

        TextWriterForTMPCanvas.AddWriter_Static(_message , _npcMessageDictionary[ePlayingProgress] , typingSpeed , _messageBoxRectTransform , true , onComplete);
    }
}
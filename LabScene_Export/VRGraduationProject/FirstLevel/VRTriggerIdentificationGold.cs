using UnityEngine;
using TMPro;
using System;

public class VRTriggerIdentificationGold : MonoBehaviour
{
    private static VRTriggerIdentificationGold instance;
    public static VRTriggerIdentificationGold Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindAnyObjectByType<VRTriggerIdentificationGold>();
            }

            return instance;
        }
    }

    private const string _18KGold = "_18KGold";

    private bool _correctAnswer;

    /// <summary>
    /// *確保程式只會執行 1 次，可以用切換 bool 來達成
    /// </summary>
    private bool _executeOnce;

    /// <summary>
    /// 辨識正在執行
    /// </summary>
    private bool _identificationExecuting;

    public event Action OnLevelClear;

    public event Action OnResetProgress;

    public event Action<Collider> OnTouchedGameObjectStop;

    private void Awake()
    {
        _correctAnswer = false;

        _executeOnce = true;

        _identificationExecuting = false;
    }

    private void Start()
    {
        OnResetProgress += Handle_ResetThePrograss;

        OnTouchedGameObjectStop += Handle_TouchedGameObjectStop;

        OnTouchedGameObjectStop += Handle_Executing;

        ButtonForIdentificationGoldBoxXRSimpleInteractable.Instance.OnClickButton += Handle_ClickButton;
    }

    // *我不寫 OnTriggerEnter 是因為 NPC 對話和放入黃金比較難接起來
    private void OnTriggerStay(Collider collider)
    {
        if(collider.transform.CompareTag(_18KGold))
        {
            _correctAnswer = true;
        }

        if(_executeOnce)
        {
            // *若是去除外面的判斷式，這裡面的程式會執行多次
            // the object is correctly fell down and stop
            if(collider.GetComponent<Rigidbody>().velocity == Vector3.zero)
            {
                OnTouchedGameObjectStop?.Invoke(collider);

                NPCUI_LabScene.Instance.NpcTalking(EPlayingProgress.Identifying , .2f , ReportAnswerResult);
            }
        }
    }

    private void Handle_Executing(Collider collider)
    {
        _executeOnce = false;

        _identificationExecuting = true;
    }

    private void Handle_TouchedGameObjectStop(Collider collider)
    {
        collider.transform.SetParent(transform);

        collider.GetComponent<Rigidbody>().isKinematic = true;

        float identificationAnimeTimer = 1f;

        Destroy(collider.gameObject , identificationAnimeTimer);
    }

    // *現在這樣的函式是可以正常顯示出對話
    private void ReportAnswerResult()
    {
        float typingSpeed = .2f;

        float pauseTimer = .5f;

        if(_correctAnswer)
        {
            StartCoroutine(NPCUI_LabScene.Instance.ContinueTalking(EPlayingProgress.QuestionForLevelOneCorrect , typingSpeed , pauseTimer , OnLevelClear));
        }

        else
        {
            StartCoroutine(NPCUI_LabScene.Instance.ContinueTalking(EPlayingProgress.QuestionForLevelOneIncorrect , typingSpeed , pauseTimer , OnResetProgress));
        }
    }

    private void Handle_ClickButton()
    {
        if(_correctAnswer)
        {
            FirstLevelDoorAnime.Instance.Handle_DestroyDoor();
        }

        else
        {
            if(!_identificationExecuting)
            {
                IdentificationGoldBoxAnime.Instance.AppearAnime();

                NPCUI_LabScene.Instance.NpcTalking(EPlayingProgress.QuestionForLevelOne , .1f);
            }
        }
    }

    private void Handle_ResetThePrograss()
    {
        _identificationExecuting = false;

        _executeOnce = true;
    }

    private void OnDestroy()
    {
        OnLevelClear = null;

        OnResetProgress = null;

        OnTouchedGameObjectStop = null;
    }
}

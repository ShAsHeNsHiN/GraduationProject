using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System;

public class WallQuiz : MonoBehaviour
{
    private static WallQuiz _instance;
    public static WallQuiz Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<WallQuiz>();
            }

            return _instance;
        }
    }

    [SerializeField] private TextMeshProUGUI _question;

    private Animation _animation;

    private readonly Dictionary<EDetectionSolutionQuestion , List<string>> _questionAndAnswerDictionary = new();

    private int _currentQuestionIndex = default;

    private readonly List<List<string>> _answerList = new()
    {
        new()
        {
            LACTIC_ACID_DRIK_POKE_BUTTON , WHITE_VINEGAR_POKE_BUTTON
        } ,

        new()
        {
            SOAP_WATER_POKE_BUTTON ,
            SODA_WATER_POKE_BUTTON
        } ,

        new()
        {
            SALT_WATER_POKE_BUTTON
        } ,

        new()
        {
            SUGAR_WATER_POKE_BUTTON
        } ,

        new()
        {
            SOAP_WATER_POKE_BUTTON
        } ,

        new()
        {
            LACTIC_ACID_DRIK_POKE_BUTTON
        } ,

        new()
        {
            WHITE_VINEGAR_POKE_BUTTON
        } ,

        new()
        {
            SODA_WATER_POKE_BUTTON
        } ,

        new()
        {
            SUGAR_WATER_POKE_BUTTON ,
            SALT_WATER_POKE_BUTTON
        } ,
    };

    private const string LACTIC_ACID_DRIK_POKE_BUTTON = "LacticAcidDrinkPokeButton";
    private const string WHITE_VINEGAR_POKE_BUTTON = "WhiteVinegarPokeButton";
    private const string SOAP_WATER_POKE_BUTTON = "SoapWaterPokeButton";
    private const string SODA_WATER_POKE_BUTTON = "SodaWaterPokeButton";
    private const string SUGAR_WATER_POKE_BUTTON = "SugarWaterPokeButton";
    private const string SALT_WATER_POKE_BUTTON = "SaltWaterPokeButton";

    private readonly HashSet<string> _currentAnswerList = new();

    public event Action OnUpdateQuestion;

    public event Action OnAnswerCorrect;

    public event Action OnAllQuestionClear;

    private void Awake()
    {
        _animation = GetComponent<Animation>();

        OnUpdateQuestion += () => 
        {
            if(_currentQuestionIndex < _answerList.Count)
            {
                _question.text = EnumHelper.GetEnumDescription((EDetectionSolutionQuestion)_currentQuestionIndex);
            }
        };

        OnAnswerCorrect += () => 
        {
            _currentAnswerList.Clear();
        };

        OnAnswerCorrect += NextQuestion;
    }

    private void Start()
    {
        ButtonForIdetificationPurpleCabbage.Instance.OnClickButton += Handle_PlayAnimation;

        OnUpdateQuestion?.Invoke();

        TransferQuestionAndAnswerToDictionary();
    }

    private void Handle_PlayAnimation()
    {
        _animation.Play();
    }

    private void TransferQuestionAndAnswerToDictionary()
    {
        for (int i = 0; i < _answerList.Count; i++)
        {
            _questionAndAnswerDictionary.Add((EDetectionSolutionQuestion)i , _answerList[i]);
        }
    }

    /// <summary>
    /// 玩家用六色骰答題
    /// </summary>
    /// <param name="buttonName">玩家按的按鈕</param>
    public void Answering(string buttonName)
    {
        // 拿取當前題目的答案按鈕
        var answers = _questionAndAnswerDictionary[(EDetectionSolutionQuestion)_currentQuestionIndex];

        // 比對 buttonName 是否為答案按鈕
        if(answers.Contains(buttonName) && !_currentAnswerList.Contains(buttonName))
        {
            _currentAnswerList.Add(buttonName);

            bool isCorrect = _currentAnswerList.SetEquals(answers);

            // 跑答題程式
            if(isCorrect)
            {
                NPCUI_LabScene.Instance.NpcTalking(EPlayingProgress.QuestionForLevelTwoCorrect , .1f);

                OnAnswerCorrect?.Invoke();
            }

            if(_currentQuestionIndex == _answerList.Count)
            {
                // 答題結束
                OnAllQuestionClear?.Invoke();
            }
        }
    }

    private void NextQuestion()
    {
        _currentQuestionIndex++;

        OnUpdateQuestion?.Invoke();
    }

    private void OnDestroy()
    {
        OnUpdateQuestion = null;

        OnAnswerCorrect = null;

        OnAllQuestionClear = null;
    }
}

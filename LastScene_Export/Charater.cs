using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Charater : MonoBehaviour
{
    [Header("頭像")]
    [SerializeField] protected Image _avaterImage;

    [SerializeField] protected TextMeshProUGUI _avaterName;

    [SerializeField] protected Sprite [] _charaterSprites;

    protected Vector2 _studentSuImageSize = new(17 , 15);

    protected Vector2 _usualImageSize = new(15 , 15);

    protected readonly Dictionary<ECharater , Sprite> _avaterDictionary = new();

    [Header("角色對話")]
    [SerializeField] protected TextMeshProUGUI _charaterText;

    [SerializeField] protected TextAsset _charaterTextFile;

    protected Queue<string> _charaterDialogueList = new();

    public Queue<string> CharaterDialogueList => _charaterDialogueList;

    [Header("其他")]
    [SerializeField] protected float _typingSpeed = .1f;

    public event Action OnTypingStart;
    
    public event Action OnTypingEnd;

    protected virtual void Awake()
    {
        TextFileTransferToCharaterDialogues(_charaterTextFile , _charaterDialogueList);

        OnTypingStart += Handle_TypingStart;

        OnTypingEnd += Handle_TypingEnd;
    }

    protected virtual void Start()
    {
        Hide();

        _avaterDictionary.Clear();

        foreach (var item in _charaterSprites)
        {
            _avaterDictionary.Add(Enum.Parse<ECharater>(item.name) , item);
        }
    }

    private void Handle_TypingStart()
    {
        KeyController.Instance.KeysHide();

        _charaterText.text = string.Empty;
    }

    private void Handle_TypingEnd()
    {
        KeyController.Instance.KeysShow();
    }

    /// <summary>
    /// 將對話以打字效果呈現
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator TypingDialogue()
    {
        TypingStart();

        string charater = _charaterDialogueList.Dequeue();

        CharaterChecked(Enum.Parse<ECharater>(charater));

        string dialogue = _charaterDialogueList.Dequeue();

        for (int i = 0; i < dialogue.Length; i++)
        {
            _charaterText.text += dialogue[i];
            yield return new WaitForSeconds(_typingSpeed);
        }

        TypingEnd();
    }

    protected void TypingStart()
    {
        OnTypingStart?.Invoke();
    }

    protected void TypingEnd()
    {
        OnTypingEnd?.Invoke();
    }

    /// <summary>
    /// Charater 隱藏
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Charater 顯示
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    protected void TextFileTransferToCharaterDialogues(TextAsset textAsset , Queue<string> charaterDialogueQueue)
    {
        var charaterDialogues = textAsset.text.Split(new [] 
        { Environment.NewLine } , StringSplitOptions.None);

        foreach(var item in charaterDialogues)
        {
            charaterDialogueQueue.Enqueue(item);
        }
    }

    protected void UpdateAvater(Sprite charaterSprite , Vector2 imageSize , ECharater eCharater)
    {
        _avaterImage.sprite = charaterSprite;

        _avaterImage.GetComponent<RectTransform>().sizeDelta = imageSize;

        _avaterName.text = eCharater.ToString();
    }

    protected void CharaterChecked(ECharater eCharater)
    {
        if(eCharater == ECharater.酥同學)
        {
            UpdateAvater(_avaterDictionary[eCharater] , _studentSuImageSize , eCharater);
        }

        else
        {
            UpdateAvater(_avaterDictionary[eCharater] , _usualImageSize , eCharater);
        }
    }

    private void OnDestroy()
    {
        OnTypingStart = null;
        OnTypingEnd = null;
    }
}

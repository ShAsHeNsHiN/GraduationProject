using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextWriterForTMPCanvas : MonoBehaviour
{
    public static TextWriterForTMPCanvas Instance {get ; private set;}

    private List<TextWriterSingle> _textWriterSingleList;

    private void Awake()
    {
        Instance = this;

        _textWriterSingleList = new();
    }

    public static void RemoveWriter_Static(TextMeshProUGUI messageText)
    {
        Instance.RemoveWriter(messageText);
    }

    private void RemoveWriter(TextMeshProUGUI messageText)
    {
        for(int i = 0 ; i < _textWriterSingleList.Count ; i++)
        {
            if(_textWriterSingleList[i].GetTheMessage() == messageText)
            {
                _textWriterSingleList.RemoveAt(i);
                
                i--;
            }
        }
    }

    /// <summary>
    /// 為指定的 TextMeshProUGUI 添加打字效果，
    /// </summary>
    /// <param name="messageText">要顯示文字的 TextMeshProUGUI 物件</param>
    /// <param name="textToWrite">要打字效果的文字內容</param>
    /// <param name="timePerCharater">打字速度（秒）</param>
    /// <param name="backgroundRectTransform">對應的背景 RectTransform（可選）</param>
    /// <param name="removeWriterBeforeAdd">是否在添加前先移除當前的 Writer</param>
    /// <param name="onComplete">當文字顯示完畢時執行的回呼函式</param>
    /// <remarks>可選擇是否在添加前先移除已存在的 Writer。</remarks>
    /// <returns>回傳控制文字顯示的 `TextWriterSingle` 物件。</returns>
    public static TextWriterSingle AddWriter_Static(TextMeshProUGUI messageText , string textToWrite , float timePerCharater , RectTransform backgroundRectTransform , bool removeWriterBeforeAdd , Action onComplete)
    {
        if(removeWriterBeforeAdd)
        {
            Instance.RemoveWriter(messageText);
        }

        return Instance.AddWriter(messageText , textToWrite , timePerCharater , backgroundRectTransform , onComplete);
    }

    private TextWriterSingle AddWriter(TextMeshProUGUI messageText , string textToWrite , float timePerCharater , RectTransform backgroundRectTransform , Action onComplete)
    {
        TextWriterSingle textWriterSingle = new(messageText , textToWrite , timePerCharater ,backgroundRectTransform , onComplete);

        _textWriterSingleList.Add(textWriterSingle);

        return textWriterSingle;
    }

    private void Update()
    {
        for(int i = 0 ; i < _textWriterSingleList.Count ; i++)
        {
            bool destroyInstance = _textWriterSingleList[i].CompleteOrNot();
            
            if(destroyInstance)
            {
                _textWriterSingleList.RemoveAt(i);

                //the below code will remove all the Instance meanwhile
                i--;
            }
        }
    }

    // Represent a single TextWriter Instance
    public class TextWriterSingle
    {
        private readonly TextMeshProUGUI _messageText;
        private readonly string _textToWrite;
        private int _charaterIndex;
        private readonly float _timePerCharater;
        private float _timer;
        private readonly RectTransform _backgroundRectTransform;
        public Action OnComplete;

        public TextWriterSingle(TextMeshProUGUI messageText , string textToWrite , float timePerCharater , RectTransform backgroundRectTransform , Action onComplete)
        {
            _messageText = messageText;
            _textToWrite = textToWrite;
            _timePerCharater = timePerCharater;
            _backgroundRectTransform = backgroundRectTransform;
            OnComplete = onComplete;
            _charaterIndex = 0;
        }

        // return true on complete
        public bool CompleteOrNot()
        {
            _timer -= Time.deltaTime;
            // while condition executes in the same frame , but if condition will be different
            while(_timer <= 0f)
            {
                // Display next charater
                _timer += _timePerCharater;
                
                _charaterIndex++;
                
                _messageText.text = _textToWrite[.._charaterIndex];

                Resizing();

                if(_charaterIndex >= _textToWrite.Length)
                {
                    // Entire string displayed
                    OnComplete?.Invoke();
                    return true;
                }
            }

            return false;
        }

        private void Resizing()
        {
            _messageText.ForceMeshUpdate();

            Vector2 textSize = _messageText.GetRenderedValues(false);

            Vector2 paddingSize = new(10,10);

            _backgroundRectTransform.sizeDelta = textSize + paddingSize ;
        }

        public TextMeshProUGUI GetTheMessage()
        {
            return _messageText;
        }

        public bool IsActive()
        {
            return _charaterIndex < _textToWrite.Length;
        }

        public void WriteAllAndDestroy()
        {
            _messageText.text = _textToWrite;
            _charaterIndex = _textToWrite.Length;
            OnComplete?.Invoke();
            RemoveWriter_Static(_messageText);
        }

        public bool Finish()
        {
            return _charaterIndex == _textToWrite.Length;
        }
    }
}

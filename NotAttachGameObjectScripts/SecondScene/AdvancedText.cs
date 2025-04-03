using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class AdvancedTextPreprocessor : ITextPreprocessor
{
    // this class help me make my TextMeshPro
    
    public Dictionary<int , float> intervalDictionary;

    public AdvancedTextPreprocessor()
    {
        intervalDictionary = new Dictionary<int, float>();
    }

    public string PreprocessText(string text)
    {
        intervalDictionary.Clear();

        string processingText = text;
        string pattern = "<.*?>";
        Match match = Regex.Match(processingText , pattern);

        while(match.Success)
        {
            string label = match.Value.Substring(1 , match.Length - 2);

            if(float.TryParse(label , out float result))
            {
                intervalDictionary[match.Index - 1] = result;
            }

            processingText = processingText.Remove(match.Index , match.Length);

            match = Regex.Match(processingText , pattern);
        }

        processingText = text;
        // .    代表任意字符！！
        // *    代表前一個字符出現零次或多次
        // +    代表前一個字符出現一次或多次
        // ?    代表前一個字符出現零次或一次

        pattern = @"<(\d+)(\.\d+)?>";

        processingText = Regex.Replace(processingText , pattern , "");
        
        return processingText;
    }
}

public class MyOwnTextPreprocessor : ITextPreprocessor 
{
    public string PreprocessText(string text)
    {
        string processingText = text;
        string pattern = ".*?";

        Regex.Match(processingText , pattern);

        return processingText;
    }
}

public class AdvancedText : TextMeshProUGUI
{
    public AdvancedText()
    {
        // the class will be accessed by above class beforehand
        textPreprocessor = new AdvancedTextPreprocessor();
    }

    public void GetText()
    {
        Debug.Log(textInfo.characterCount);
    }

    private AdvancedTextPreprocessor SelfProprocessor => (AdvancedTextPreprocessor)textPreprocessor;

    public void ShowTextByTyping()
    {
        StartCoroutine(Typing());
    }

    // public void ShowWholeText(string content){
    //     StartCoroutine(WriteWholeText(content));
    // }

    public void ShowWholeText()
    {
        StartCoroutine(WriteWholeText());
    }

    public void ShowWholeTextV1(string content)
    {
        StartCoroutine(WriteWholeTextV1(content));
    }

    private int _typingIndex;
    private const float DefaultInterval = .2f;
    // private bool endTyping;

    private IEnumerator Typing()
    {
        // ForceMeshUpdate();
        for(int i = 0 ; i < m_characterCount ; i++)
        {
            SetSingleCharaterAlpha(i , 0);
        }

        _typingIndex = 0;
        while(_typingIndex < m_characterCount)
        {
            // endTyping = false;
            if(textInfo.characterInfo[_typingIndex].isVisible)
            {
                // Debug.Log("OPTION1");
                StartCoroutine(FadeInCharater(_typingIndex));
            }

            if(SelfProprocessor.intervalDictionary.TryGetValue(_typingIndex , out float result))
            {
                // Debug.Log("OPTION2");
                yield return new WaitForSecondsRealtime(result);
            }

            else
            {
                yield return new WaitForSecondsRealtime(DefaultInterval);
                // Debug.Log("OPTION3");
            }

            _typingIndex++;
        }

        if(_typingIndex >= m_characterCount)
        {
            TheStoryWriter.Instance.EndTyping();
        }

        // endTyping = true;

        yield return null;
    }

    private IEnumerator FadeInCharater(int index , float duration = .2f)
    {
        if(duration < 0)
        {
            SetSingleCharaterAlpha(index , 255);
        }

        else
        {
            float timer = 0;
            
            while(timer < duration)
            {
                timer = Mathf.Min(duration , timer + Time.unscaledTime);
                SetSingleCharaterAlpha(index , (byte)(255 * timer / duration));

                yield return null;
            }
        }
    }

    // private IEnumerator WriteWholeText(string content){
    //     for(int i = 0 ; i < m_characterCount ; i++){
    //         SetSingleCharaterAlpha(i , 255);
    //     }

    //     typingIndex = content.Length;

    //     yield return null;
    // }

    private IEnumerator WriteWholeText(){
        for(int i = 0 ; i < m_characterCount ; i++)
        {
            SetSingleCharaterAlpha(i , 255);
        }

        // typingIndex = content.Length;

        yield return null;
    }

    private IEnumerator WriteWholeTextV1(string content)
    {
        _typingIndex = content.Length;

        yield return null;
    }

    // new Alpha's range is 0~255
    private void SetSingleCharaterAlpha(int index , byte newAlpha)
    {
        TMP_CharacterInfo charInfo = textInfo.characterInfo[index];
        int matIndex = charInfo.materialReferenceIndex;
        int vertIndex = charInfo.vertexIndex;

        for(int i = 0 ; i < 4 ; i++)
        {
            textInfo.meshInfo[matIndex].colors32[vertIndex + i ].a = newAlpha;
        }

        UpdateVertexData();
    }
}
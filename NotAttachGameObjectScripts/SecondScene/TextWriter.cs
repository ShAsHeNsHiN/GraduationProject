using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextWriter : MonoBehaviour
{
    public static TextWriter Instance;

    public List<TextWriterSingle> textWriterSingleList;

    private void Awake(){
        Instance = this;
        textWriterSingleList = new List<TextWriterSingle>();
    }

    public static void RemoveWriter_Static(TextMeshProUGUI messageText){
        Instance.RemoveWriter(messageText);
    }

    private void RemoveWriter(TextMeshProUGUI messageText){
        for(int i = 0 ; i < textWriterSingleList.Count ; i++){
            if(textWriterSingleList[i].GetTheMessage() == messageText){
                textWriterSingleList.RemoveAt(i);
                 i--;
            }
        }
    }

    public static TextWriterSingle AddWriter_Static(TextMeshProUGUI messageText , string textToWrite , float timePerCharater , RectTransform backgroundRectTransform , bool removeWriterBeforeAdd , Action onComplete){
        if(removeWriterBeforeAdd){
            Instance.RemoveWriter(messageText);
        }
        return Instance.AddWriter(messageText , textToWrite , timePerCharater , backgroundRectTransform , onComplete);
    }

    private TextWriterSingle AddWriter(TextMeshProUGUI messageText , string textToWrite , float timePerCharater , RectTransform backgroundRectTransform , Action onComplete){
        TextWriterSingle textWriterSingle = new(messageText , textToWrite , timePerCharater ,backgroundRectTransform , onComplete);
        textWriterSingleList.Add(textWriterSingle);
        return textWriterSingle;
    }

    private void Update(){
        // Debug.Log(textWriterSingleList.Count);
        for(int i = 0 ; i < textWriterSingleList.Count ; i++){
            bool destroyInstance = textWriterSingleList[i].CompleteOrNot();
            // Debug.Log(destroyInstance);
            if(destroyInstance){
                textWriterSingleList.RemoveAt(i);
                //the below code will remove all the Instance meanwhile
                 i--;
            }
        }
    }

    // Represent a single TextWriter Instance
    public class TextWriterSingle{

        private TextMeshProUGUI messageText;
        private string theResultText;
        private string textToWrite;
        private int charaterIndex;
        private float timePerCharater;
        private float timer;
        private RectTransform backgroundRectTransform;
        private Action onComplete;

        public TextWriterSingle(TextMeshProUGUI messageText , string textToWrite , float timePerCharater , RectTransform backgroundRectTransform , Action onComplete){
            this.messageText = messageText;
            // this.textToWrite = textToWrite;
            this.timePerCharater = timePerCharater;
            this.backgroundRectTransform = backgroundRectTransform;
            this.onComplete = onComplete;
            theResultText = textToWrite;
            charaterIndex = 0;
            }

        // return true on complete
        public bool CompleteOrNot(){
                timer -= Time.deltaTime;
                // while condition executes in the same frame,but if condition will be different
                while(timer <= 0f){
                    // Display next charater
                    timer += timePerCharater;
                    charaterIndex++;
                    
                    messageText.text = theResultText[..charaterIndex];
                    Resizing();

                    if(charaterIndex >= theResultText.Length){
                    // Entire string displayed
                    onComplete?.Invoke();
                    return true;
                    }
                }

                return false;
            }

        private void Resizing(){
            messageText.ForceMeshUpdate();

            Vector2 textSize = messageText.GetRenderedValues(false);

            Vector2 paddingSize = new(10,10);

            backgroundRectTransform.sizeDelta = textSize + paddingSize ;
        }

        public TextMeshProUGUI GetTheMessage(){
            return messageText;
        }

        public bool IsActive(){
            return charaterIndex < theResultText.Length;
        }

        public void WriteAllAndDestroy(){
            messageText.text = theResultText;
            charaterIndex = theResultText.Length;
            Resizing();
            onComplete?.Invoke();
            RemoveWriter_Static(messageText);
        }
    }
}

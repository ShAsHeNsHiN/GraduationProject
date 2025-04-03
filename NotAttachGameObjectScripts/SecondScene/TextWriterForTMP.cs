using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextWriterForTMP : MonoBehaviour
{
    public static TextWriterForTMP Instance;

    public List<TextWriterSingle> textWriterSingleList;

    private void Awake(){
        Instance = this;
        textWriterSingleList = new List<TextWriterSingle>();
    }

    public static void RemoveWriter_Static(TextMeshPro messageText){
        Instance.RemoveWriter(messageText);
    }

    private void RemoveWriter(TextMeshPro messageText){
        for(int i = 0 ; i < textWriterSingleList.Count ; i++){
            if(textWriterSingleList[i].GetTheMessage() == messageText){
                textWriterSingleList.RemoveAt(i);
                 i--;
            }
        }
    }

    public static TextWriterSingle AddWriter_Static(TextMeshPro messageText , string textToWrite , float timePerCharater  , bool removeWriterBeforeAdd , Action onCompleteStopSound){
        if(removeWriterBeforeAdd){
            Instance.RemoveWriter(messageText);
        }
        return Instance.AddWriter(messageText , textToWrite , timePerCharater  , onCompleteStopSound);
    }

    private TextWriterSingle AddWriter(TextMeshPro messageText , string textToWrite , float timePerCharater  , Action onCompleteStopSound){
        TextWriterSingle textWriterSingle = new(messageText , textToWrite , timePerCharater  , onCompleteStopSound);
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

        private TextMeshPro messageText;
        private string textToWrite;
        private int charaterIndex;
        private float timePerCharater;
        private float timer;
        private Action onCompleteStopSound;

        public TextWriterSingle(TextMeshPro messageText , string textToWrite , float timePerCharater  , Action onCompleteStopSound){
            this.messageText = messageText;
            this.textToWrite = textToWrite;
            this.timePerCharater = timePerCharater;
            this.onCompleteStopSound = onCompleteStopSound;
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
                    
                    messageText.text = textToWrite[..charaterIndex];

                    if(charaterIndex >= textToWrite.Length){
                    // Entire string displayed
                        onCompleteStopSound?.Invoke();
                        return true;
                    }
                }

                return false;
            }

        public TextMeshPro GetTheMessage(){
            return messageText;
        }

        public bool IsActive(){
            return charaterIndex < textToWrite.Length;
        }

        public void WriteAllAndDestroy(){
            messageText.text = textToWrite;
            charaterIndex = textToWrite.Length;
            onCompleteStopSound?.Invoke();
            RemoveWriter_Static(messageText);
        }
    }
}

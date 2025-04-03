using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LittleTest : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject dialogue;

    public Camera mainCamera;

    private bool start;
    private bool changedColor;
    private float desiredDuration = 5f;
    private float elapsedTime;

    private void Awake(){
        startButton = GetComponent<Button>();
    }

    private void Start(){
        startButton.onClick.AddListener(() => {
            start = true;
            StartGame();
            print("you're click!");
        });
        
    }

    private void Update(){
        if(changedColor){
            // elapsedTime += Time.deltaTime;
            // float percentageComplete = elapsedTime / desiredDuration;
            // if(percentageComplete <= 1){
            //     mainCamera.backgroundColor = Color.Lerp(Color.black , Color.white , percentageComplete);
            // }
            // else{
            //     changedColor = false;
            // }
        }
    }

    private void StartGame(){
        if(start){
            Time.timeScale = 0f;
        }
        else{
            Time.timeScale = 1f;
        }
    }
}

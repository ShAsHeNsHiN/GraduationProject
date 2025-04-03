using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GoToNextScene : MonoBehaviour
{
    public static GoToNextScene Instance{get ; private set ;}

    [SerializeField] private Button goButton;

    private void Awake(){
        Instance = this;
        goButton.onClick.AddListener(() => {
            SceneManager.LoadScene(1);
        });
    }

    private void Start(){
        Hide();
    }

    public void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}

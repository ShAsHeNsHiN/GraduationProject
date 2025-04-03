using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MissionUI : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject menuButtonActiveBg;
    [SerializeField] private TextMeshProUGUI missionText;

    [SerializeField] private InputActionReference menuButtonRef;

    private string [] mission = new string [] {
        "黃金的價值:<br>窩不知道"
    };

    private void Start(){
        missionText.SetText(mission[0]);

        Hide();

        menuButtonRef.action.performed += MenuOpenAndClose;
    }

    private void MenuOpenAndClose(InputAction.CallbackContext obj){
        if(container.activeSelf){
            Hide();
        }
        else{
            Show();
        }
    }

    private void Hide(){
        container.SetActive(false);
        menuButtonActiveBg.SetActive(false);
    }

    private void Show(){
        container.SetActive(true);
        menuButtonActiveBg.SetActive(true);
    }
}

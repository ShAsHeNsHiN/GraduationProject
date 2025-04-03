using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractScaleUI : MonoBehaviour
{
    [SerializeField] private PlayerInteractScale playerInteractScale;

    [Header("TheLeftKey")]
    [SerializeField] private GameObject container;
    // [Space]
    [SerializeField] private TextMeshProUGUI keyIncreaseText;

    [Header("TheRightKey")]
    [SerializeField] private GameObject container1;
    // [Space]
    [SerializeField] private TextMeshProUGUI keyDecreaseText;

    [Header("TheWeight")]
    [SerializeField] private GameObject container2;
    [SerializeField] private TextMeshProUGUI theWeightText;

    private void Update(){
        if(playerInteractScale.InteractWhichScale() != null){
            Show(playerInteractScale.InteractWhichScale());
        }
        else{
            Hide();
            // NPCUI.Instance.Hide();
        }
    }

    private void Show(ITheChosenScale tilt){
        keyIncreaseText.SetText(playerInteractScale.GetIncreaseCode().ToString());
        keyDecreaseText.SetText(playerInteractScale.GetDecreaseCode().ToString());
        theWeightText.SetText("Weight:" + tilt.GetWeightText());

        container.SetActive(true);
        container1.SetActive(true);
        container2.SetActive(true);
    }

    private void Hide(){
        container.SetActive(false);
        container1.SetActive(false);
        container2.SetActive(false);
    }
}

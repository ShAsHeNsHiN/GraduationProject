using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerIntroUI : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button showButton;

    private void Start(){
        Hide();

        closeButton.onClick.AddListener(() => {
            Hide();
        });

        showButton.onClick.AddListener(() => {
            Show();
        });
    }

    private void Hide(){
        container.SetActive(false);
        showButton.gameObject.SetActive(!container.activeSelf);
    }

    private void Show(){
        container.SetActive(true);
        showButton.gameObject.SetActive(!container.activeSelf);
    }
}

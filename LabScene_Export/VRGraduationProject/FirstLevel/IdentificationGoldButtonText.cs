using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IdentificationGoldButtonText : MonoBehaviour
{
    public static IdentificationGoldButtonText Instance{get ; private set;}

    private TextMeshProUGUI _buttonText;

    private void Awake()
    {
        Instance = this;

        _buttonText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        VRTriggerIdentificationGold.Instance.OnLevelClear += Handle_SetText;
    }

    private void Handle_SetText()
    {
        _buttonText.SetText("檢測成功!");
    }
}

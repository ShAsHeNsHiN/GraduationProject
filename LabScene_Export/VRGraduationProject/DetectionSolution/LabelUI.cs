using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LabelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _labelText;

    private readonly Dictionary<string , string> _detectionSolutionDictionary = new()
    {
        { "SugarWater", "糖水" },
        { "SodaWater" , "小蘇打水"},
        { "SaltWater", "食鹽水"},
        { "SoapWater" , "肥皂水"},
        { "WhiteVinegar" , "白醋"},
        { "LacticAcidDrink" , "乳酸飲料"},
    };

    private void Start()
    {
        string detectionSolutionName = transform.parent.name;

        _labelText.text = _detectionSolutionDictionary[detectionSolutionName];
    }
}

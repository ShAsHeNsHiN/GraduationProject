using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SixthDicePokeButtonText : MonoBehaviour
{
    private static SixthDicePokeButtonText _instance;
    public static SixthDicePokeButtonText Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<SixthDicePokeButtonText>();
            }

            return _instance;
        }
    }

    private TextMeshProUGUI _pokeButtonText;

    private readonly Dictionary<string , string> _pokeButtonNameDictionary = new()
    {
        {"LacticAcidDrinkPokeButton" , "乳酸飲料"} ,
        {"WhiteVinegarPokeButton" , "白醋"} ,
        {"SoapWaterPokeButton" , "肥皂水"} ,
        {"SodaWaterPokeButton" , "小蘇打水"} ,
        {"SugarWaterPokeButton" , "糖水"} ,
        {"SaltWaterPokeButton" , "食鹽水"} ,
    };

    private void Awake()
    {
        _pokeButtonText = GetComponent<TextMeshProUGUI>();
    }

    public void DisplayPokeButtonText(string buttonName)
    {
        _pokeButtonText.text = _pokeButtonNameDictionary[buttonName];
    }

    public void Detecting()
    {
        _pokeButtonText.text = "感測中…";
    }
}

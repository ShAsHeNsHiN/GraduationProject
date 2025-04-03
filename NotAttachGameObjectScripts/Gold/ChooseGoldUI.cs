using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseGoldUI : MonoBehaviour
{
    [SerializeField] private PlayerChooseGold playerChooseGold;

    [Header("HandleAll")]
    [SerializeField] private UnityEngine.GameObject container;

    [Header("Gold1(leftButton)")]
    [SerializeField] private TheScaleItemSO _14KGoldSO;
    [SerializeField] private Button gold1Button;
    [SerializeField] private TextMeshProUGUI gold1Text;

    [Header("Gold2(rightButton)")]
    [SerializeField] private TheScaleItemSO _18KGoldSO;
    [SerializeField] private Button gold2Button;
    [SerializeField] private TextMeshProUGUI gold2Text;
    
    private void Start(){
        gold1Button.onClick.AddListener(() => {
            // Instantiate
            playerChooseGold.WhichItemAddGold().InstaGold1();

            Hide();
        });

        gold2Button.onClick.AddListener(() => {
            // Instantiate
            playerChooseGold.WhichItemAddGold().InstaGold2();

            Hide();
        });

        UpdateVisual();
        Hide();
    }

    private void UpdateVisual(){
        gold1Text.SetText(_14KGoldSO.ItemName);
        gold2Text.SetText(_18KGoldSO.ItemName);
    }

    public void Show(){
        gold1Button.Select();
        container.SetActive(true);
    }

    private void Hide(){
        container.SetActive(false);
    }
}

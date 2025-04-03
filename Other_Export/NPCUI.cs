using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCUI : MonoBehaviour
{
    [SerializeField] protected GameObject _avatarGameObject;
    [SerializeField] protected Sprite _avatarSprite;

    [Space]
    [SerializeField] protected GameObject _messagePanelGameObject;

    // 訊息框(RectTransform)
    [SerializeField] protected RectTransform _messageBoxRectTransform;

    // 訊息(Text)
    [SerializeField] protected TextMeshProUGUI _message;

    protected virtual void Awake()
    {
        _avatarGameObject.transform.GetComponent<Image>().sprite = _avatarSprite;
    }

    protected void ControlNpc(EActiveState eActiveState)
    {
        _avatarGameObject.SetActive(Convert.ToBoolean((int)eActiveState));

        _messagePanelGameObject.SetActive(Convert.ToBoolean((int)eActiveState));
    }
}

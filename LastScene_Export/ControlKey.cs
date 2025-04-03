using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlKey : MonoBehaviour
{
    [SerializeField] protected InputActionReference _keyReference;

    public event Action OnKeyPressed;

    protected const string LEAVE_KEY_TEXT = "LeaveKeyText";

    /// <summary>
    /// 進行對話
    /// </summary>
    /// <param name="obj"></param>
    protected virtual void Handle_Dialogue(InputAction.CallbackContext obj)
    {
        OnKeyPressed?.Invoke();
    }

    /// <summary>
    /// 以想要的物件狀態控制對話事件訂閱
    /// </summary>
    /// <param name="eActiveState">想要的物件狀態(顯示 or 隱藏)</param>
    private void DialogueEventControlledBy(EActiveState eActiveState)
    {
        switch (eActiveState)
        {
            case EActiveState.顯示:

                _keyReference.action.performed += Handle_Dialogue;

                break;

            case EActiveState.隱藏:

                _keyReference.action.performed -= Handle_Dialogue;

                break;
        }
    }

    protected virtual void Handle_UpdateKeyText()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 以想要的物件狀態控制遊戲物件與對話事件訂閱
    /// </summary>
    /// <param name="eKeyState">想要的物件狀態(顯示 or 隱藏)</param>
    public void SetActiveAndControlDialogueEvent(EActiveState eKeyState)
    {
        gameObject.SetActive(Convert.ToBoolean((int)eKeyState));

        DialogueEventControlledBy(eKeyState);
    }

    private void OnDestroy()
    {
        OnKeyPressed = null;
    }
}

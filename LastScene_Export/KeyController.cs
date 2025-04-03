using UnityEngine;

public class KeyController : MonoBehaviour
{
    private static KeyController instance;

    public static KeyController Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindAnyObjectByType<KeyController>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        SubscribeFadeEvents();
    }

    public void SubscribeFadeEvents()
    {
        FaderScreen.Instance.OnFadeEnd += KeysShow;
        FaderScreen.Instance.OnFadeStart += KeysHide;
    }

    /// <summary>
    /// 將每個控制鍵隱藏
    /// </summary>
    public void KeysHide()
    {
        ControlKeyState(EActiveState.隱藏 , LeftKey.Instance , RightKey.Instance);
    }

    /// <summary>
    /// 將每個控制鍵調成一致的狀態
    /// </summary>
    /// <param name="eKeyState">想要的控制鍵狀態(顯示 or 隱藏)</param>
    /// <param name="controlKeys">想要變更狀態的控制鍵</param>
    private void ControlKeyState(EActiveState eKeyState , params ControlKey[] controlKeys)
    {
        foreach (var item in controlKeys)
        {
            item.SetActiveAndControlDialogueEvent(eKeyState);
        }
    }

    /// <summary>
    /// 將每個控制鍵顯示
    /// </summary>
    public void KeysShow()
    {
        ControlKeyState(EActiveState.顯示 , LeftKey.Instance , RightKey.Instance);
    }
}

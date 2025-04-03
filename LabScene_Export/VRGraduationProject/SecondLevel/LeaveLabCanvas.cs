using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaveLabCanvas : MonoBehaviour
{
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;

    [Header("Player Ray Controller")]
    [SerializeField] private GameObject _leftRayGameObject;
    [SerializeField] private GameObject _rightRayGameObject;

    private const float DESIREDURATION = 11f;

    private void Awake()
    {
        LeaveExperience.Instance.OnPlayerTriggerEnter += Handle_Show;
    }

    private void Start()
    {
        _yesButton.onClick.AddListener(Handle_LeaveGame);

        _noButton.onClick.AddListener(Handle_ContinueGame);

        Hide();
    }

    private void Handle_LeaveGame()
    {
        Time.timeScale = 1f;

        MusicManager.Instance.ChangeAudio();

        StartCoroutine(WakeUp());
    }

    private void Handle_ContinueGame()
    {
        Time.timeScale = 1f;

        // 玩家離開遊戲時，我預設把地板給隱藏了，因此繼續遊戲就得顯示回來
        GroundManager.Instance.Show();

        Hide();
    }

    private IEnumerator WakeUp()
    {
        HideRays();
        
        FaderScreen.Instance.FadeIn();

        yield return new WaitForSeconds(DESIREDURATION);

        Loader.Load(Loader.Scene.LastScene);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>防止玩家誤觸到 LeavLabCanvas</remarks>
    private void HideRays()
    {
        _leftRayGameObject.SetActive(false);
        
        _rightRayGameObject.SetActive(false);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Handle_Show()
    {
        gameObject.SetActive(true);
    }
}

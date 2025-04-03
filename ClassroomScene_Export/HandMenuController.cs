using UnityEngine;
using UnityEngine.UI;

public class HandMenuController : MonoBehaviour
{
    [SerializeField] private Button _previousButton;

    [Header("StartButton")]
    [SerializeField] private Button _startButton;
    [SerializeField] private GameObject _playIcon;
    [SerializeField] private GameObject _stopIcon;

    [Space]
    [SerializeField] private Button _nextButton;

    private int _currentStoryIndex = default;

    private void Start()
    {
        _startButton.onClick.AddListener(Handle_StartButton);

        _previousButton.onClick.AddListener(Handle_PreviousButton);

        _nextButton.onClick.AddListener(Handle_NextButton);
    }

    private void Handle_StartButton()
    {
        NPCUI_SecondScene.Instance.NpcTalking(_currentStoryIndex);

        ChangePlayIconToStop();

        PlayCDPlayer.Instance.StartEffect();

        StruggleBGM.Instance.Play();

        // *防止玩家誤按
        _startButton.onClick.RemoveAllListeners();
    }

    private void ChangePlayIconToStop()
    {
        // 隱藏播放圖示
        _playIcon.SetActive(false);
        
        // 顯示暫停圖示
        _stopIcon.SetActive(true);
    }

    private void Handle_PreviousButton()
    {
        // 要有上一段對話，這個倒退鍵才可以按
        if(_currentStoryIndex > 0)
        {
            _currentStoryIndex--;

            NPCUI_SecondScene.Instance.NpcTalking(_currentStoryIndex);
        }

        // *防止玩家在轉場時誤按
        if(_currentStoryIndex == NPCUI_SecondScene.Instance.StoryContentLength)
        {
            _previousButton.onClick.RemoveAllListeners();
        }
    }

    private void Handle_NextButton()
    {
        _currentStoryIndex++;

        NPCUI_SecondScene.Instance.NpcTalking(_currentStoryIndex);

        // *防止玩家在轉場時誤按
        if(_currentStoryIndex == NPCUI_SecondScene.Instance.StoryContentLength)
        {
            _nextButton.onClick.RemoveAllListeners();
        }
    }
}

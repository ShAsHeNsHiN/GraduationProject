using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeyHintAnime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _keyHintLeftText;
    [SerializeField] private Image _keyHintLeftBg;
    [SerializeField] private TextMeshProUGUI _keyHintRightText;

    private const string VISIBLE = "visible";
    private const string FLASH = "flash";

    private Animator _ownAnimator;

    private Animator _keyHintLeftAnimator;
    private Animator _keyHintLeftBgAnimator;
    private Animator _keyHintRightAnimator;

    private float _effectTimer = 1f;

    private bool _firstTimeAnime;

    private void Awake()
    {
        _ownAnimator = GetComponent<Animator>();

        _keyHintLeftAnimator = _keyHintLeftText.GetComponent<Animator>();

        _keyHintRightAnimator = _keyHintRightText.GetComponent<Animator>();

        _keyHintLeftBgAnimator = _keyHintLeftBg.GetComponent<Animator>();   
    }

    private void OnEnable()
    {
        _keyHintLeftAnimator.SetTrigger(VISIBLE);

        _keyHintRightAnimator.SetTrigger(VISIBLE);

        _keyHintLeftBgAnimator.SetTrigger(VISIBLE);
    }

    private void Update()
    {
        _effectTimer -= Time.deltaTime;

        if(_firstTimeAnime)
        {
            _ownAnimator.SetTrigger(FLASH);
        }

        else
        {
            if(_effectTimer < 0)
            {
                _firstTimeAnime = true;
            }
        }
    }
}

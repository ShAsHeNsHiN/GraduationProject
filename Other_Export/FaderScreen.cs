using System;
using System.Collections;
using UnityEngine;

public class FaderScreen : MonoBehaviour
{
    private static FaderScreen instance;
    public static FaderScreen Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindAnyObjectByType<FaderScreen>();
            }

            return instance;
        }
    }

    [SerializeField] private Color _fadeColor;

    public float EffectDuration {get ; private set;} = 3f;

    private MeshRenderer _meshRenderer;

    public event Action OnFadeEnd;

    public event Action OnFadeStart;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        OnFadeEnd += Handle_RecoverToOriginalColor;

        FadeOut();
    }

    public void FadeIn()
    {
        Fade(0 , 1);
    }

    public void FadeOut()
    {
        Fade(1 , 0);
    }

    private void Fade(float alphaIn , float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn , alphaOut));
    }

    private IEnumerator FadeRoutine(float alphaIn , float alphaOut)
    {
        OnFadeStart?.Invoke();

        float timer = 0;

        while(timer <= EffectDuration)
        {
            timer += Time.deltaTime;

            _fadeColor.a = Mathf.Lerp(alphaIn , alphaOut , timer / EffectDuration);

            _meshRenderer.sharedMaterial.color = _fadeColor;

            yield return null;
        }

        OnFadeEnd?.Invoke();
    }

    private void Handle_RecoverToOriginalColor()
    {
        _meshRenderer.sharedMaterial.color = _fadeColor;
    }

    private void OnDestroy()
    {
        OnFadeStart = null;

        OnFadeEnd = null;
    }
}

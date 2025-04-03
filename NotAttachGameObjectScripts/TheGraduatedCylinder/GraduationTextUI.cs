using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class GraduationTextUI : MonoBehaviour
{
    [SerializeField] private GameObject container;

    [Space]
    [SerializeField] private TextMeshProUGUI graduationText;

    [Header("LiquidValue")]
    [SerializeField] private LiquidSO smallerMLSO;
    [SerializeField] private LiquidSO biggerMLSO;

    [Header("GraCyTextPos")]
    [SerializeField] private Transform GraduationTextSmallerMLTransform;
    [SerializeField] private Transform GraduationTextBiggerMLTransform;

    private Animator animator;

    private bool mLSmallerToBigger;
    private bool mLBiggerToSmaller;
    private bool addGold;

    private void Awake(){
        animator = GetComponent<Animator>();

        mLSmallerToBigger = false;
        mLBiggerToSmaller = false;
        addGold = false;
    }

    private void Start(){
        Hide();
    }

    public void Show(){
        container.SetActive(true);
        animator.SetTrigger("Display");
    }

    private void Hide(){
        container.SetActive(false);
    }

    public void GraduationTextForSmallerMl(){
        if(addGold){
            graduationText.SetText("Graduation : " + smallerMLSO.mL.ToString());

            StartCoroutine(FallingAnime());

            mLSmallerToBigger = false;
        }
        else{
            // start siutuation
            graduationText.SetText("Graduation : " + smallerMLSO.mL.ToString());
            transform.position = GraduationTextSmallerMLTransform.position;
        }       
    }

    public void GraduationTextForBiggerMl(){
        graduationText.SetText("Graduation : " + biggerMLSO.mL.ToString());

        // mLSmallerToBigger = true;
        addGold = true;
        mLBiggerToSmaller = false;

        StartCoroutine(RisingAnime());
    }

    public bool GetSmallerToBiggerML(){
        return mLSmallerToBigger;
    }

    public bool GetBiggerToSmallerML(){
        return mLBiggerToSmaller;
    }

    public bool GetAddGold(){
        return addGold;
    }

    private IEnumerator RisingAnime(){
        float t = 0;
        float speed = .5f;
        while(t < 1){
            transform.position = Vector3.Lerp(GraduationTextSmallerMLTransform.position , GraduationTextBiggerMLTransform.position , t);
            t += Time.deltaTime / speed;
            yield return null;
        }

        // below code are running after above code
        // rising anime
        mLSmallerToBigger = true;
    }

    private IEnumerator FallingAnime(){
        float t = 0;
        float speed = .5f;
        while(t < 1){
            transform.position = Vector3.Lerp(GraduationTextBiggerMLTransform.position , GraduationTextSmallerMLTransform.position , t);
            t += Time.deltaTime / speed;
            yield return null;
        }

        // falling anime
        mLBiggerToSmaller = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AboutTeller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    // [SerializeField] private TextWriter textWriter;
    [SerializeField] private Button changeTheMessageButton;
    // public GameObject messageUI;
    // [SerializeField] private ResizeTheText resizeTheText;

    private TextWriter.TextWriterSingle textWriterSingle;
    private AudioSource audioSource;

    private RectTransform backgroundRectTransform;
    public RectTransform canvasRectTransorm;
    private RectTransform UIRectTransform;
    string [] theMessageArray = new string [] {
                "How are you?",
                "I'm find,thank you!",
                "HAHA,there you are!",
                "Got it!",
                "Surprise,MotherFucker.",
                "EMOTIONAL DAMAGE!!",
                "How Stupid You Are!!"
                };

    private void Awake(){
        // Application.targetFrameRate = 3;
        backgroundRectTransform = transform.Find("MessageUI").Find("MessageBackground").GetComponent<RectTransform>();
        UIRectTransform = transform.Find("MessageUI").GetComponent<RectTransform>();
        audioSource = transform.Find("MessageUI").Find("MessageSound").GetComponent<AudioSource>();
        

        changeTheMessageButton.onClick.AddListener(() => {
            
            string theChosenMessage = theMessageArray[Random.Range(0 , theMessageArray.Length)];
            
            if(textWriterSingle != null && textWriterSingle.IsActive()){
                // Currently active TextWriter
                textWriterSingle.WriteAllAndDestroy();
            }

            else{
                StartPlayingSound();
                textWriterSingle = TextWriter.AddWriter_Static(messageText , theChosenMessage , .1f ,backgroundRectTransform , true , StopPlayingSound);
            }
        });
   
    }

    private void StartPlayingSound(){
        audioSource.Play();
    }

    private void StopPlayingSound(){
        audioSource.Stop();
    }

    private void Start(){
        // Debug.Log(transform.Find("MessageUI").Find("MessageBackground"));
        // System.Func<string> theMessageText = () => {
        //     return "Hi,I'm a mermaid.Are you like me?~";
        // };
        // StartPlayingSound();
        // textWriterSingle = TextWriter.AddWriter_Static(messageText , theMessageText() , .1f ,backgroundRectTransform , true , StopPlayingSound);
    }

    // private void Update(){
    //     Vector2 anchoredPosition = Input.mousePosition / canvasRectTransorm.localScale.x;

    //     if(anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransorm.rect.width){
    //         // Whole Message left screen on right side
    //         anchoredPosition.x = canvasRectTransorm.rect.width - backgroundRectTransform.rect.width;
    //     }

    //     if(anchoredPosition.x < 0){
    //         // Whole Message left screen on left side
    //         anchoredPosition.x = 0;
    //     }

    //     if(anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransorm.rect.height){
    //         // Whole Message left screen on top side
    //         anchoredPosition.y = canvasRectTransorm.rect.height - backgroundRectTransform.rect.height;
    //     }

    //     if(anchoredPosition.y < 0){
    //         // Whole Message left screen on bottom side
    //         anchoredPosition.y = 0 ;
    //     }

    //     UIRectTransform.anchoredPosition = anchoredPosition;
    // }
}

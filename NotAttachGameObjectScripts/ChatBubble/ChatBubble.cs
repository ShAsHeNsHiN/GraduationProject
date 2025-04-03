using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using TMPro;

public class ChatBubble : MonoBehaviour
{
    public static ChatBubble Instance {get ; private set ;}

    [SerializeField] private TextMeshPro theMessage;
    [SerializeField] private Thought_Icon_Face_SO thought_Icon_Face_SO;

    private float timer;
    private SpriteRenderer iconSprite;
    private SpriteRenderer backgroundSpriteRenderer;
    private TextWriterForTMP.TextWriterSingle textWriterSingle;
    private AudioSource audioSource;
    private Emotion emotion1;
    private UnityEngine.GameObject gameObject2;

    string [] theMessageArray = new string [] {
                "How are you?",
                "I'm find,thank you!",
                "HAHA,there you are!",
                "Got it!",
                "Surprise,MotherFucker.",
                "EMOTIONAL DAMAGE!!",
                "How Stupid You Are!!"
                };

    public enum Emotion{
        Angry,
        Contemptuous,
        Shocked,
        Happy
    }

    private void Awake(){
        Instance = this;
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        iconSprite = transform.Find("Icon").GetComponent<SpriteRenderer>();
        // audioSource = transform.Find("MessageSound").GetComponent<AudioSource>();

        // Hide_Static();
    }

    private void Start(){
        // Debug.Log(Player.Instance.GetPfChatBubbleTransform().gameObject);
        // Debug.Log(Instance.transform);
    }

    public static void Create(Transform parent , Vector3 localPosition , Emotion emotion , string text , float timerDisplay){
        // Debug.Log(Player.Instance.GetPfChatBubbleTransform());
        Transform chatBubbleTransform = Instantiate( GameAsset.Instance.GetPfChatBubble() , parent);
        chatBubbleTransform.transform.localPosition = localPosition;

        chatBubbleTransform.GetComponent<ChatBubble>().SetUp(emotion , text , timerDisplay);

        float destroyTime = 2f;
        Destroy(chatBubbleTransform.gameObject , (text.Length-1)*timerDisplay + destroyTime );
    }

    private Sprite GetTheEmotion(Emotion emotion){
        switch(emotion){
            default:
            case Emotion.Angry: 
                return thought_Icon_Face_SO.angry_Face;
            case Emotion.Contemptuous:
                return thought_Icon_Face_SO.contemptuous_Face[Random.Range(0 , thought_Icon_Face_SO.contemptuous_Face.Length)];
            case Emotion.Shocked:
                return thought_Icon_Face_SO.shocked_Face;
            case Emotion.Happy:
                return thought_Icon_Face_SO.happy_Face[Random.Range(0 , thought_Icon_Face_SO.happy_Face.Length)];
        }
    }

    private void Update(){
        // timer -= Time.deltaTime;
        // if(timer <= 0f){
        //     // Choose_Icon_Face(thought_Icon_Face_SO.thought_Icon_Face);
        //     SetUp( Emotion.Angry , theMessageArray[Random.Range(0 , theMessageArray.Length)]);
        //     timer = .2f;
        // }
    }

    public void SetUp(Emotion emotion , string text , float timerDisplay){
        theMessage.SetText(text);

        //the below code can let theMessage render instantly.
        theMessage.ForceMeshUpdate();

        //if want to use the size value,please change the draw mode to sliced in SpriteRender first!!
        Vector2 textSize = theMessage.GetRenderedValues(false);
        Vector2 padding = new(9f, 4f);
        backgroundSpriteRenderer.size = textSize + padding;

        Vector3 offset = new(-14.5f , 0);
        backgroundSpriteRenderer.transform.localPosition = 
        new Vector3(backgroundSpriteRenderer.size.x / 2f , 0f) + offset ;

        iconSprite.sprite = GetTheEmotion(emotion);

        textWriterSingle = TextWriterForTMP.AddWriter_Static(theMessage , text , timerDisplay  , true , () => {});

    }

    public TextWriterForTMP.TextWriterSingle GetTextWriterSingle(){
        return textWriterSingle;
    }

    private void StartPlayingSound(){
        audioSource.Play();
    }

    private void StopPlayingSound(){
        audioSource.Stop();
    }

    public static void Hide_Static(){
        Instance.Hide();
    }

    private void Hide(){
        gameObject.SetActive(false);
    }

    public static void Show_Static(){
        Instance.Show();
    }

    private void Show(){
        gameObject.SetActive(true);
    }
}

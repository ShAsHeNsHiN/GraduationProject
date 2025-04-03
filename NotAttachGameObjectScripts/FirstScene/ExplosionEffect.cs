using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private Transform circleTransform;
    [SerializeField] private Transform otherCircleTransform;
    
    private Vector3 originalSize = new Vector3(1 , 1 , 1);
    private Vector3 maximizeScale = new Vector3(11 , 11  , 11);

    private Vector3 originalPos = new Vector3(0 , .9f , 0);
    private Vector3 targetPos = new Vector3(-.3f , .9f , 0);

    private float timer;
    private float animeTimer = 1f;
    
    private float elapsedTime;
    private float desiredDuration = 3f;

    private new Animation animation;

    private void Awake(){
        animation = circleTransform.GetComponent<Animation>();
    }
    
    private void Start(){

    }

    private void Update(){
        


        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / desiredDuration;
        if(percentageComplete <= 1.1f){
            circleTransform.localScale = Vector3.Lerp(originalSize , maximizeScale , percentageComplete);

            circleTransform.localPosition = Vector3.Lerp(originalPos , targetPos , percentageComplete);

            // print(elapsedTime);
        }

        if(percentageComplete == .5f){
            print("Exactly Execute!");
        }

        if(circleTransform.localScale.x >= maximizeScale.x){
            elapsedTime = 0;
            print("execute!");
        }

        // print(animation.isPlaying);
        // if(animation.isPlaying){

        // }
        // else{
        //     Transform ccTransform = Instantiate(circleTransform , transform);
        //     animation = ccTransform.GetComponent<Animation>();
        //     ccTransform.localScale = new Vector3(1 , 1 , 1);
        // }

        if(Input.GetKeyDown(KeyCode.J)){
            Transform ccTransform = Instantiate(circleTransform , transform);
            print(ccTransform == circleTransform);
        }
    }
}

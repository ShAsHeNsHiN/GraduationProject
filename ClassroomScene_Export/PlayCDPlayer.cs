using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCDPlayer : MonoBehaviour
{
    public static PlayCDPlayer Instance{get ; private set;}

    private const float ROTATESPEED = 5f;

    private void Awake()
    {
        Instance = this;

        enabled = false;
    }

    private void Update() => Effect();

    public void StartEffect()
    {
        // Update() 開始執行
        enabled = true;
    }

    private void Effect()
    {
        transform.Rotate(ROTATESPEED * Time.deltaTime * new Vector3(0 , 0 , -50));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganWall : MonoBehaviour
{
    [Header("我預設牆只會移動 X 軸")]
    [SerializeField] private Vector3 _pushDistance = Vector3.right;

    private void Start()
    {
        WallQuiz.Instance.OnAllQuestionClear += () => 
        {
            Destroy(gameObject);
        };

        WallQuiz.Instance.OnAnswerCorrect += Handle_PushTheWall;
    }

    private void Handle_PushTheWall()
    {
        transform.position += _pushDistance;
    }
}

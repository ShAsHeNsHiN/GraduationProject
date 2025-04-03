using UnityEngine;

public class LockPokeButtonText : MonoBehaviour
{
    [SerializeField] private Transform _sixthDiceTransform;

    private const float OFFSET = .15f;

    private void Start()
    {
        if(_sixthDiceTransform == null)
        {
            _sixthDiceTransform = ControlSixthDice.Instance.transform;
        }

        WallQuiz.Instance.OnAllQuestionClear += () => 
        {
            Destroy(gameObject);
        };
    }

    private void Update()
    {
        transform.position = _sixthDiceTransform.position + Vector3.up * OFFSET;
    }
}

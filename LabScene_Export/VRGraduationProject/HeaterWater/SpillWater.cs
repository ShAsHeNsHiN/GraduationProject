using UnityEngine;

public class SpillWater : MonoBehaviour
{
    public static SpillWater Instance{get ; private set;}

    [SerializeField] private Transform _liquidPivot;

    private ParticleSystem _particleSystem;

    private float _hitWaterTimes = 100f;

    public bool HasAddedWater {get ; private set;}

    private void Awake()
    {
        Instance = this;

        _particleSystem = GetComponent<ParticleSystem>();

        HasAddedWater = default;
    }

    private void Update()
    {
        if(HasAddedWater)
        {
            _particleSystem.Stop();

            _liquidPivot.gameObject.SetActive(false);
        }

        else
        {
            // *這裡的判斷是我自己抓出特定的角度，準確率大概只有 5 成
            if(Vector3.Angle(Vector3.up , transform.forward) <= 90f && Vector3.Angle(Vector3.up , transform.forward) >= 30f)
            {
                if(!_particleSystem.isPlaying)
                {
                    _particleSystem.Play();
                }
            }

            else
            {
                _particleSystem.Stop();
            }
        }
        
        // Debug.Log(transform.eulerAngles);
        // Debug.Log(transform.eulerAngles.y);
        // // 上方的y值是個亂數，只有在Vector 3 中才為0
        // Debug.Log(transform.eulerAngles.x);
    }

    private void OnParticleCollision(GameObject other)
    {
        _hitWaterTimes--;

        if(_hitWaterTimes < 0)
        {
            HasAddedWater = true;
        }
    }

    private void OnParticleTrigger()
    {
        if(TriggerPurpleCabbageSliced.Instance.HasAddedPurpleCabbageSliced)
        {
            _hitWaterTimes--;

            if(_hitWaterTimes < 0)
            {
                HasAddedWater = true;
            }
        }
    }
}

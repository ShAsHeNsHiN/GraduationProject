using UnityEngine;

public class TriggerToSqueezePurpleCabbageLiquid : MonoBehaviour
{
    private static TriggerToSqueezePurpleCabbageLiquid _instance;
    public static TriggerToSqueezePurpleCabbageLiquid Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<TriggerToSqueezePurpleCabbageLiquid>();
            }

            return _instance;
        }
    }

    [SerializeField] private MeshRenderer _liquidMeshRenderer;

    private const string DROPPER = "Dropper";
    
    public bool Contact {get ; private set;}

    private void Awake()
    {
        Contact = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.CompareTag(DROPPER))
        {
            Contact = true;

            // // let the dropper squeeze different color liquid
            collider.transform.GetComponent<Dropper>().ChangeColor(_liquidMeshRenderer.sharedMaterial.color);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.transform.CompareTag(DROPPER))
        {
            Contact = false;
        }
    }
}

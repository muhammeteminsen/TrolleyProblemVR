using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrainTrigger : MonoBehaviour
{
    public static bool IsSwitchable;
    [SerializeField] private GameObject dieObject;
    [SerializeField] private GameObject destructionTrain;
    [SerializeField] private ParticleSystem explosionVFX;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        IsSwitchable = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Switchable"))
        {
            Debug.LogWarning("Switch");
            IsSwitchable = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Instantiate(dieObject, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            _camera.transform.DOShakePosition(.2f, .1f, fadeOut: true).OnComplete(() =>
            {
                _camera.transform.DOKill();
            });
        }

        else if (other.gameObject.CompareTag("FatCharacter"))
        {
            if (destructionTrain != null)
            {
                Debug.LogWarning("TriggerFatCharacter");
                // GameObject destructionTrainInstantiate =
                //     Instantiate(destructionTrain, transform.position, transform.rotation);
                // Destroy(gameObject);
                //ApplyExplosion(other, destructionTrainInstantiate);
            }
        }
    }

    private Rigidbody[] _destructionTrainRb;
    private void ApplyExplosion(Collider other, GameObject destructionTrainInstantiate)
    {
        if (destructionTrainInstantiate == null) return;
        _destructionTrainRb = destructionTrainInstantiate.GetComponentsInChildren<Rigidbody>();
        foreach (var t in _destructionTrainRb)
        {
            t.AddExplosionForce(Random.Range(-300, 300), other.transform.position, 10f);
        }
        _camera.transform.DOShakePosition(.1f, .1f, fadeOut: true).OnComplete(() =>
        {
            _camera.transform.DOKill();
        });
        ParticleSystem explosionInstantiate = Instantiate(explosionVFX, other.transform.position,Quaternion.identity);
        Destroy(explosionInstantiate.gameObject,1.5f);
    }
}
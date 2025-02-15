using DG.Tweening;
using UnityEngine;

public class TrainTrigger : MonoBehaviour
{
    public static bool IsSwitchable;
    [SerializeField] private GameObject dieObject;
    [SerializeField] private DOTweenController doTweenController;
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
            doTweenController.GetComponent<TrainMovement>().isPlay = false;
            doTweenController.GetShakeRotation(transform);
            Destroy(other.transform.parent.gameObject,.1f);
        }
    }
}
using DG.Tweening;
using UnityEngine;

public class TrainTrigger : MonoBehaviour
{
    private Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        Destroy(other.gameObject);
        _camera.DOKill();
        _camera.transform.DOShakePosition(.2f, .1f, fadeOut: true);
    }
}

using System;
using DG.Tweening;
using UnityEngine;

public class TrainTrigger : MonoBehaviour
{
    public static bool IsSwitchable;
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
            Destroy(other.gameObject);
            _camera.transform.DOShakePosition(.2f,.1f,fadeOut:true).OnComplete(() =>
            {
                _camera.transform.DOKill();
                
            });
        }
    }
}

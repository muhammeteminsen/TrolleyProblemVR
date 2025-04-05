using DG.Tweening;
using UnityEngine;

public class TrainTrigger : MonoBehaviour
{
    private Camera _camera;
    private GameStateManager _gameStateManager;
    private void Awake()
    {
        _camera = Camera.main;
        _gameStateManager = FindAnyObjectByType<GameStateManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            _camera.DOKill();
            _camera.transform.DOShakePosition(.2f, .1f, fadeOut: true);
        }
        else if (other.gameObject.CompareTag("FatCharacter"))
        {
            _gameStateManager.hasInteraction = true;
            _gameStateManager.ChangeState(new PauseState());
            Destroy(other.transform.parent.gameObject);
        }
    }
}

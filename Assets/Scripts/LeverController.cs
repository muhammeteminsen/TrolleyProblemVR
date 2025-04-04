using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeverController : MonoBehaviour
{
    private GameStateManager _gameStateManager;
    private PathController _pathController;
    [SerializeField] private Transform lever; 

    private void Awake()
    {
        _gameStateManager = GetComponent<GameStateManager>();
        _pathController = GetComponent<PathController>();
    }

    private void Update()
    {
        PullLever();
    }

    private void PullLever()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two) || Keyboard.current[Key.B].wasPressedThisFrame &&
            _pathController.pathSwitch != null)
        {
            if (_gameStateManager.currentState is not UnSwitchState) return;
            _gameStateManager.ChangeState(new SwitchState());
            StartCoroutine(SmartRotation());
            _gameStateManager.hasSwitched = true;
        }
    }
    private IEnumerator SmartRotation()
    {
        float duration = 1f;
        float elapsedTime = 0f;
        while (elapsedTime<duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            lever.rotation = Quaternion.Lerp(lever.rotation, Quaternion.Euler(0, 0, -45), t);
            yield return null;
        }
    }
}
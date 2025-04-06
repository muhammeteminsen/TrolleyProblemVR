using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    private GameStateManager _gameStateManager;
    private PathController _pathController;
    private IPullable _pullable;
    private IPushable _pushable;
    private IBridgeable _bridgeable;

    private void Awake()
    {
        _gameStateManager = GetComponent<GameStateManager>();
        _pathController = GetComponent<PathController>();
        _pullable = GetComponent<IPullable>();
        _pushable = GetComponent<IPushable>();
        _bridgeable = GetComponent<IBridgeable>();
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two) || Keyboard.current[Key.B].wasPressedThisFrame)
        {
            _pullable?.Pull(_gameStateManager, _pathController);
            _pushable?.Push();
            _bridgeable?.Open();
        }
    }
}
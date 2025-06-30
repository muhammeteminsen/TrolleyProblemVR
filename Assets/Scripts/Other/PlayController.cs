using UnityEngine;

public class PlayController : MonoBehaviour
{
    private GameStateManager _gameStateManager;
    private InputHandler _inputHandler;

    private void Awake()
    {
        _gameStateManager = GetComponent<GameStateManager>();
        _inputHandler = GetComponent<InputHandler>();
    }

    private void Update()
    {
        Play();
        Pause();
    }

    private void Play()
    {
        if (_inputHandler.InteractSecondaryButtonLeft())
        {
            _gameStateManager.GetPulledInteraction();
        }
    }

    private void Pause()
    {
        if (_inputHandler.InteractSecondaryButtonRight())
        {
            _gameStateManager.ChangeState(new PauseState());
        }
    }
}
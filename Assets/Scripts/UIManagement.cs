using UnityEngine;

public class UIManagement : MonoBehaviour
{
    private GameStateManager _gameStateManager;
    private void Awake()
    {
        _gameStateManager = GetComponent<GameStateManager>();
    }

    public void Play()
    {
        if (_gameStateManager.hasInteraction)return;
        switch (_gameStateManager.currentState)
        {
            case SwitchState:
                _gameStateManager.ChangeState(new SwitchState());
                break;
            default:
                _gameStateManager.ChangeState(new UnSwitchState());
                break;
        }
    }

    public void Pause()
    {
        _gameStateManager.ChangeState(new PauseState());
    }
}
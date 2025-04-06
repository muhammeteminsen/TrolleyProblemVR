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
        if (_gameStateManager.hasInteraction) return;
        _gameStateManager.GetPulledInteraction();
    }

    public void Pause()
    {
        _gameStateManager.ChangeState(new PauseState());
    }
}
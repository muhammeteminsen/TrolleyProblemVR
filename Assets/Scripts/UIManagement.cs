using UnityEngine;

public class UIManagement : MonoBehaviour
{
    private GameStateManager _gameStateManager;
    private PathController _pathController;
    private void Awake()
    {
        _gameStateManager = GetComponent<GameStateManager>();
        _pathController = GetComponent<PathController>();
    }

    public void Play()
    {
        if (_pathController.GetDistanceToTrain(_gameStateManager.playHandler.train) <= 0.1f) return;
        _gameStateManager.ChangeState(new UnSwitchState());
    }

    public void Pause()
    {
        _gameStateManager.ChangeState(new PauseState());
    }
}
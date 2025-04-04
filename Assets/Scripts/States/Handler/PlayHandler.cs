using DG.Tweening;
using UnityEngine;

public class PlayHandler : MonoBehaviour
{
    public GameObject train;
    [SerializeField] private float movementSpeed = 4f;
    private PathController _pathController;
    private int _currentIndex;
    private bool _hasPaused;

    private void Awake()
    {
        _pathController = GetComponent<PathController>();
    }

    public void UpdateUnSwitch(GameStateManager state)
    {
        TowardsUnSwitch(state);
    }

    public void ExitUnSwitch(GameStateManager state)
    {
        train.transform.DOShakeRotation(.2f, 1f, 1, 5f).OnComplete(() =>
        {
            _hasPaused = true;
            state.ChangeState(new PauseState());
        });
    }
    
    public void UpdateSwitch()
    {
        TowardsSwitch();
    }

    private void TowardsUnSwitch(GameStateManager state)
    {
        if (_hasPaused) return;
        _hasPaused = false;
        train.transform.position = Vector3.MoveTowards(train.transform.position,
            _pathController.GetPathPoints(), Time.deltaTime * movementSpeed);
        if (Vector3.Distance(train.transform.position, _pathController.GetPathPoints()) >= 0.1f) return;
        ExitUnSwitch(state);
    }

    private void TowardsSwitch()
    {
        train.transform.position = Vector3.MoveTowards(train.transform.position,
            _pathController.GetPathPoints(), Time.deltaTime * movementSpeed);
        train.transform.rotation = Quaternion.RotateTowards(train.transform.rotation,
            Quaternion.Euler(0, 0, 0), Time.deltaTime * movementSpeed);
        if (Vector3.Distance(train.transform.position, _pathController.GetPathSwitchPoints(ref _currentIndex )) >= 0.1f) return;
        _currentIndex++;
    }
    
}
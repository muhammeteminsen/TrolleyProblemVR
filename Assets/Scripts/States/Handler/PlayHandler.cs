using DG.Tweening;
using UnityEngine;

public class PlayHandler : MonoBehaviour
{
    public Transform train;
    [SerializeField] private float movementSpeed = 4f;
    private PathController _pathController;
    private int _currentIndex;
    private bool _hasReachedEnd;

    private void Start()
    {
        _hasReachedEnd = false;
        _pathController = GetComponent<PathController>();
    }

    public void UpdateUnSwitch(GameStateManager state)
    {
        TowardsUnSwitch(state);
    }

    public void ExitPlay(GameStateManager state)
    {
        train.transform.DOShakeRotation(.2f, 1f, 1, 5f).OnComplete(() => { state.ChangeState(new PauseState()); });
    }

    public void UpdateSwitch()
    {
        TowardsSwitch();
    }

    private void TowardsUnSwitch(GameStateManager state)
    {
        if (_hasReachedEnd || TrainTrigger.isTriggerFatCharacter) return;
        train.transform.position = Vector3.MoveTowards(train.transform.position,
            _pathController.GetPathPoints(), Time.deltaTime * movementSpeed);
        if (Vector3.Distance(train.transform.position, _pathController.GetPathPoints()) >= 0.1f) return;
        ExitPlay(state);
        _hasReachedEnd = true;
    }

    private void TowardsSwitch()
    {
        if (_currentIndex >= _pathController.pathSwitchPoints.Count || TrainTrigger.isTriggerFatCharacter) return;
        train.transform.position = Vector3.MoveTowards(train.transform.position,
            _pathController.GetPathSwitchPoints(ref _currentIndex), Time.deltaTime * movementSpeed);
        Vector3 dir = _pathController.GetPathSwitchPoints(ref _currentIndex) - train.transform.position;
        train.transform.rotation = Quaternion.Lerp(train.transform.rotation,
            Quaternion.LookRotation(dir) * Quaternion.Euler(0, 90, 0), Time.deltaTime * 5f);
        if (Vector3.Distance(train.transform.position, _pathController.GetPathSwitchPoints(ref _currentIndex)) >=
            0.1f) return;
        _currentIndex++;
    }
}
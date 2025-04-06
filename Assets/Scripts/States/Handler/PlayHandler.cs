using DG.Tweening;
using UnityEngine;

public class PlayHandler : DistanceController
{
    [SerializeField] private float movementSpeed = 4f;
    private PathController _pathController;
    private int _currentIndex;
    
    protected override void Start()
    {
        base.Start();
        _pathController = GetComponent<PathController>();
    }

    public void UpdateUnSwitch(GameStateManager state)
    {
        TowardsUnSwitch(state);
    }

    public void ExitPlay(GameStateManager state)
    {
        Train.transform.DOShakeRotation(.2f, 1f, 1, 5f).OnComplete(() =>
        {
            state.ChangeState(new PauseState());
        });
    }
    
    public void UpdateSwitch()
    {
        TowardsSwitch();
    }

    private void TowardsUnSwitch(GameStateManager state)
    {
        Train.transform.position = Vector3.MoveTowards(Train.transform.position,
            _pathController.GetPathPoints(), Time.deltaTime * movementSpeed);
        if (Vector3.Distance(Train.transform.position, _pathController.GetPathPoints()) >= 0.1f) return;
        ExitPlay(state);
        state.hasInteraction = true;
    }

    private void TowardsSwitch()
    {
        if (Distance(_pathController.GetPathPoints().z) <= distanceThreshold) return;
        Train.transform.position = Vector3.MoveTowards(Train.transform.position,
            _pathController.GetPathSwitchPoints(ref _currentIndex), Time.deltaTime * movementSpeed);
        Vector3 dir = _pathController.GetPathSwitchPoints(ref _currentIndex) - Train.transform.position;
        Train.transform.rotation = Quaternion.Lerp(Train.transform.rotation,
            Quaternion.LookRotation(dir) * Quaternion.Euler(0, 90, 0), Time.deltaTime * 5f);
        if (Vector3.Distance(Train.transform.position, _pathController.GetPathSwitchPoints(ref _currentIndex )) >= 0.1f) return;
        _currentIndex++;
    }
    
}
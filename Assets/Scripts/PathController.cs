using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField] private Transform path;
    public Transform pathSwitch;
    private List<Transform> _pathPoints = new List<Transform>();
    private List<Transform> _pathSwitchPoints = new List<Transform>();
    private GameStateManager _gameStateManager;
    private void Awake()
    {
        _gameStateManager = GetComponent<GameStateManager>();
        if (path == null) return;
        foreach (Transform child in path)
        {
            _pathPoints.Add(child);
        }

        if (pathSwitch == null) return;
        foreach (Transform child in pathSwitch)
        {
            _pathSwitchPoints.Add(child);
        }
    }
    
    public Vector3 GetPathPoints()
    {
        return _pathPoints[^1].position;
    }
    public Vector3 GetPathSwitchPoints(ref int currentIndex)
    {
        if (currentIndex >= _pathSwitchPoints.Count || currentIndex < 0 )
        {
            Vector3 lastPoint =  _pathSwitchPoints[^1].position;
           _gameStateManager.ChangeState(new PauseState());
            return lastPoint;
        }
        return _pathSwitchPoints[currentIndex].position;
    }
}   
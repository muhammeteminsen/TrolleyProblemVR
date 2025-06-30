using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField] private Transform path;
    public Transform pathSwitch;
    public List<Transform> pathPoints = new List<Transform>();
    public List<Transform> pathSwitchPoints = new List<Transform>();
    private GameStateManager _gameStateManager;
    private void Awake()
    {
        _gameStateManager = GetComponent<GameStateManager>();
        if (path == null) return;
        foreach (Transform child in path)
        {
            pathPoints.Add(child);
        }

        if (pathSwitch == null) return;
        foreach (Transform child in pathSwitch)
        {
            pathSwitchPoints.Add(child);
        }
    }
    
    public Vector3 GetPathPoints()
    {
        return pathPoints[^1].position;
    }
    public Vector3 GetPathSwitchPoints(ref int currentIndex)
    {
        if (currentIndex >= pathSwitchPoints.Count || currentIndex < 0 )
        {
            Vector3 lastPoint =  pathSwitchPoints[^1].position;
           _gameStateManager.ChangeState(new PauseState());
            return lastPoint;
        }
        return pathSwitchPoints[currentIndex].position;
    }
}   
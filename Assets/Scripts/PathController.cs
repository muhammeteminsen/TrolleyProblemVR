using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField] private Transform path;
    [SerializeField] private Transform pathSwitch;
    private List<Transform> _pathPoints = new List<Transform>();
    private List<Transform> _pathSwitchPoints = new List<Transform>();
    private void Awake()
    {
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
        if (currentIndex >= _pathSwitchPoints.Count) return Vector3.zero;
        var pathSwitchPoint = _pathSwitchPoints[currentIndex].position;
        return pathSwitchPoint;
    }
   
    public float GetDistanceToTrain(GameObject train)
    {
        return Vector3.Distance(train.transform.position, GetPathPoints());
    }
    
}   
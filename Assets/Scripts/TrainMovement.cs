using System;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    [SerializeField, Header("----Train Amount----")]
    private GameObject train;

    [SerializeField] private float trainSpeed = 10f;
    [SerializeField] private Transform path;
    [SerializeField] private Transform pathSwitch;
    private List<Transform> _paths = new List<Transform>();
    private List<Transform> _pathsSwitch = new List<Transform>();
    [SerializeField] private float distanceAmount = 0.1f;
    [SerializeField] private float trainRotSmoothness;
    private int _currentPath;
    private bool _isPlay;
    public bool _isPathSwitch;

    private void Awake()
    {
        if (path == null)
            return;
        _paths.Clear();
        Transform[] childTransform = path.GetComponentsInChildren<Transform>();
        foreach (var child in childTransform)
        {
            if (child != path.transform && !_paths.Contains(child))
            {
                _paths.Add(child);
            }
        }

        if (pathSwitch == null)
            return;
        _pathsSwitch.Clear();
        Transform[] childTransformSwitch = pathSwitch.GetComponentsInChildren<Transform>();
        foreach (var child in childTransformSwitch)
        {
            if (child != pathSwitch.transform && !_pathsSwitch.Contains(child))
            {
                _pathsSwitch.Add(child);
            }
        }
    }

    private void Update()
    {
        Movement();
        PathSwitch();
    }

    private void Movement()
    {
        if (!_isPlay || _isPathSwitch) return;
        if (_currentPath > _paths.Count - 1) return;
        float distance = Vector3.Distance(train.transform.position, _paths[_currentPath].position);
        if (distance <= distanceAmount)
        {
            _currentPath++;
        }
        else
        {
            train.transform.position =
                Vector3.MoveTowards(train.transform.position, _paths[_currentPath].position,
                    Time.deltaTime * trainSpeed);
        }
    }

    private void PathSwitch()
    {
        if (!_isPlay || !_isPathSwitch) return;
        if (_currentPath > _pathsSwitch.Count - 1) return;
        float distance = Vector3.Distance(train.transform.position, _pathsSwitch[_currentPath].position);
        if (distance <= distanceAmount)
        {
            _currentPath++;
        }
        else
        {
            Vector3 dir = (_pathsSwitch[_currentPath].position - train.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Quaternion offsetRotation = Quaternion.Euler(0, 90, 0);
            Quaternion correctedRotation = lookRotation * offsetRotation;
            train.transform.rotation = Quaternion.Slerp(train.transform.rotation, correctedRotation,
                Time.deltaTime * trainRotSmoothness);
            train.transform.position =
                Vector3.MoveTowards(train.transform.position, _pathsSwitch[_currentPath].position,
                    Time.deltaTime * trainSpeed);
        }
      
    }

    public void MovementInteraction()
    {
        _isPlay = true;
    }
}
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TrainMovement : MonoBehaviour
{
    public static TrainMovement Instance;

    [SerializeField, Header("----Train Amount----")]
    private GameObject train;

    [SerializeField] private float trainRotSmoothness;
    [SerializeField] private float trainSpeed = 10f;

    [SerializeField, Header("----Paths Variables----")]
    private Transform path;

    [SerializeField] private Transform pathSwitch;
    private List<Transform> _paths = new List<Transform>();
    private List<Transform> _pathsSwitch = new List<Transform>();
    [SerializeField] private float distanceAmount = 0.1f;
    public int currentPath;
    [Header("----Boolean----")] private bool _isPlay;
    public bool isPathSwitch;
    private bool _isTweenActive = false;
    [SerializeField] private float duration, strength, vibrato, randomness;
    [SerializeField] private Ease ease;

    private void Awake()
    {
        Instance = this;
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
        if (!_isTweenActive)
        {
            Movement();
            PathSwitch();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void Movement()
    {
        if (!_isPlay || isPathSwitch) return;
        if (currentPath > _paths.Count - 1) return;
        float distance = Vector3.Distance(train.transform.position, _paths[currentPath].position);
        if (distance <= distanceAmount)
        {
            currentPath++;
            TrainAnimation();
        }
        else
        {
            train.transform.position =
                Vector3.MoveTowards(train.transform.position, _paths[currentPath].position,
                    Time.deltaTime * trainSpeed);
        }
    }

    private void PathSwitch()
    {
        if (!_isPlay || !isPathSwitch) return;
        if (currentPath > _pathsSwitch.Count - 1) return;
        float distance = Vector3.Distance(train.transform.position, _pathsSwitch[currentPath].position);
        if (distance <= distanceAmount)
        {
            currentPath++;
            TrainAnimation();
        }
        else
        {
            Vector3 dir = (_pathsSwitch[currentPath].position - train.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Quaternion offsetRotation = Quaternion.Euler(0, 90, 0);
            Quaternion correctedRotation = lookRotation * offsetRotation;
            train.transform.rotation = Quaternion.Slerp(train.transform.rotation, correctedRotation,
                Time.deltaTime * trainRotSmoothness);
            train.transform.position =
                Vector3.MoveTowards(train.transform.position, _pathsSwitch[currentPath].position,
                    Time.deltaTime * trainSpeed);
        }
    }

    private void TrainAnimation()
    {
        
        if ((!isPathSwitch || currentPath != _pathsSwitch.Count) &&
            (isPathSwitch || currentPath != _paths.Count)) return;
        _isTweenActive = true;
        train.transform.DOShakeRotation(duration, strength, (int)vibrato, randomness)
            .SetEase(ease)
            .OnComplete(() =>
            {
                train.transform.DOKill();
                _isTweenActive = false;
            });
    }

    public void MovementInteraction()
    {
        _isPlay = true;
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainMovement : MonoBehaviour
{
    public static TrainMovement Instance;

    [SerializeField, Header("----Train Amount----")]
    private GameObject train;
    private Transform _player;
    [SerializeField] private float trainRotSmoothness;
    [SerializeField] private float trainSpeed = 10f;

    [SerializeField, Header("----Paths Variables----")]
    private Transform path;
    [SerializeField] private Transform pathSwitch;
    private List<Transform> _paths = new List<Transform>();
    private List<Transform> _pathsSwitch = new List<Transform>();
    [SerializeField] private float distanceAmount = 0.1f;
    public int currentPath;
    
    [Header("----Boolean----")] 
    public bool isPlay { get; set; }
    public bool isPathSwitch { get; set; }
    private bool _isTweenActive;
    private DOTweenController _doTweenController; 

    private void Awake()
    {
        Instance = this;
        _doTweenController = GetComponent<DOTweenController>();
        _player = GameObject.FindWithTag("Player").transform;
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Movement()
    {
        if (!isPlay || isPathSwitch || train == null) return;
        if (currentPath > _paths.Count - 1) return;
        float distance = Vector3.Distance(train.transform.position, _paths[currentPath].position);
        if (distance <= distanceAmount)
        {
            currentPath++;
            StopTrainAnimation();
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
        if (!isPlay || !isPathSwitch || train == null) return;
        if (currentPath > _pathsSwitch.Count - 1) return;
        float distance = Vector3.Distance(train.transform.position, _pathsSwitch[currentPath].position);
        if (distance <= distanceAmount)
        {
            currentPath++;
            StopTrainAnimation();
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

    private void StopTrainAnimation()
    {
        if ((!isPathSwitch || currentPath != _pathsSwitch.Count) &&
            (isPathSwitch || currentPath != _paths.Count)) return;
        ApplyShakeRotation();
    }

    private void PauseTrainAnimation()
    {
        ApplyShakeRotation();
    }

    private void ApplyShakeRotation()
    {
        _isTweenActive = true;
        _doTweenController.GetShakeRotation(train.transform,
            () => { _isTweenActive = false; });
    }

    public float GetPlayerToTrain()
    {
        return  train.transform.position.z - _player.position.z;
    }
    
    public void MovementInteraction()
    {
        isPlay = true;
    }

    public void PauseTrain()
    {
        if (!isPlay) return;
        PauseTrainAnimation();
        isPlay = false;
    }
}
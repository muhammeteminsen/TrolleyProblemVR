using UnityEngine;

public class ControllerBase : MonoBehaviour, IDistanceable
{
    private Transform _train;
    protected RagdollController Ragdoll;
    protected bool IsPushed;
    [SerializeField] protected float distanceThreshold = 2f;

    protected virtual void Start()
    {
        _train = GameObject.FindGameObjectWithTag("Train").transform;
        Ragdoll = GetComponent<RagdollController>();
        IsPushed = false;
    }

    public float Distance(float from)
    {
        return Mathf.Abs(Vector3.Distance(new Vector3(0, 0, from), new Vector3(0, 0, _train.position.z)));
    }
}
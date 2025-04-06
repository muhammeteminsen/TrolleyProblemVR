using UnityEngine;

public class DistanceController : MonoBehaviour, IDistanceable
{
    protected Transform Train;
    [SerializeField] protected float distanceThreshold = 2f;
    protected virtual void Start()
    {
        Train = GameObject.FindGameObjectWithTag("Train").transform;
    }
    
    public float Distance(float from)
    {
        return Mathf.Abs(Vector3.Distance(new Vector3(0,0,from), new Vector3(0,0,Train.position.z)));
    }
}

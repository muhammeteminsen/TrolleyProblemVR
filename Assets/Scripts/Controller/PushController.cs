using System;
using UnityEngine;

public class PushController : DistanceController, IPushable
{
    [SerializeField] private GameObject pushableObject;
    private Animator _pushableAnimator;
    private bool _isPushed;

    protected override void Start()
    {
        base.Start();
        _pushableAnimator = pushableObject.transform.parent.GetComponent<Animator>();
        RagdollController.Instance.GetRagdoll(pushableObject);
        RagdollController.Instance.RagdollOff(_pushableAnimator);
    }

    private void Update()
    {
        Debug.LogWarning(Distance(pushableObject.transform.position.z));
    }

    public void Push()
    {
        if(pushableObject == null) return;
        if (Distance(pushableObject.transform.position.z) <= distanceThreshold) return;
        if (_isPushed) return;
        Vector3 newPushPosition = new Vector3(0, Vector3.up.y, Vector3.back.z) * 10f;
        RagdollController.Instance.RagdollOn(_pushableAnimator, newPushPosition);
        _isPushed = true;
    }
}
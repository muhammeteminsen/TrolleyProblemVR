using System.Collections;
using UnityEngine;

public class Chapter2Controller : ControllerBase, IPushable
{
    [SerializeField] private GameObject pushableObject;
    private Animator _pushableAnimator;

    protected override void Start()
    {
        base.Start();
        _pushableAnimator = pushableObject.transform.parent.GetComponent<Animator>();
        Ragdoll?.GetRagdoll(pushableObject);
        Ragdoll?.RagdollOff(_pushableAnimator);
    }
    public void Push()
    {
        if (IsPushed) return;
        if(pushableObject == null) return;
        if (Distance(pushableObject.transform.position.z) <= distanceThreshold) return;
        StartCoroutine(SmartPush());
    }

    private IEnumerator SmartPush()
    {
        IsPushed = true;
        PlayerAnimationEvents.OnPush?.Invoke();
        yield return new WaitForSecondsRealtime(1.5f);
        Vector3 newPushPosition = new Vector3(0, Vector3.up.y, Vector3.back.z) * 10f;
        Ragdoll?.RagdollOn(_pushableAnimator, newPushPosition);
        yield return null;
    }
}
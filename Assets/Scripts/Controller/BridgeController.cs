using System.Collections;
using UnityEngine;

public class BridgeController : DistanceController, IBridgeable
{
    [SerializeField] private Transform bridgeDoor;
    [SerializeField] private GameObject bridgeFallObject;
    private Animator _bridgeFallObjectAnimator;

    protected override void Start()
    {
        base.Start();
        _bridgeFallObjectAnimator = bridgeFallObject.transform.parent.GetComponent<Animator>();
        RagdollController.Instance.GetRagdoll(bridgeFallObject);
        RagdollController.Instance.RagdollOff(_bridgeFallObjectAnimator);
    }

    public void Open()
    {
        if (Distance(bridgeDoor.position.z) <= distanceThreshold) return;
        if (bridgeDoor == null) return;
        StartCoroutine(SmartPush());
    }

    private IEnumerator SmartPush()
    {
        float duration = 0.2f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            bridgeDoor.rotation = Quaternion.Lerp(bridgeDoor.rotation, Quaternion.Euler(0, -90, 0), t);
            yield return null;
        }
        Vector3 newPushPosition = new Vector3(0, 0, Vector3.back.z) * 10f;
        RagdollController.Instance.RagdollOn(_bridgeFallObjectAnimator, newPushPosition);
    }
}
using System.Collections;
using UnityEngine;

public class BridgeController : DistanceController, IBridgeable
{
    [SerializeField] private Transform bridgeDoor;

    public void OpenBridge()
    {
        if (Distance(bridgeDoor.position.z) <= distanceThreshold) return;
        if (bridgeDoor == null) return;
        StartCoroutine(SmartRotation());
    }

    private IEnumerator SmartRotation()
    {
        float duration = 1f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            bridgeDoor.rotation = Quaternion.Lerp(bridgeDoor.rotation, Quaternion.Euler(0, 90, 0), t);
            yield return null;
        }
    }
}
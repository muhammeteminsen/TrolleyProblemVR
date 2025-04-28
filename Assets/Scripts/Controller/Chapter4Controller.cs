using UnityEngine;
using System.Collections;

public class Chapter4Controller : ControllerBase, IBridgeable, IPullable
{
    [SerializeField] private Transform bridgeDoor;
    [SerializeField] private Transform lever;
    [SerializeField] private GameObject pushableObject;
    private Animator _pushableAnimator;
   

    protected override void Start()
    {
        base.Start();
        _pushableAnimator = pushableObject.transform.parent.GetComponent<Animator>();
        Ragdoll?.GetRagdoll(pushableObject);
        Ragdoll?.RagdollOff(_pushableAnimator);
    }
    

    public void Open()
    {
        if (IsPushed)return;
        if (bridgeDoor == null) return;
        StartCoroutine(SmartPush());
        IsPushed = true;
    }
    
    public void Pull(GameStateManager stateManager, PathController pathController)
    {
        if (pathController.pathSwitch == null) return;
        if (Distance(pathController.GetPathPoints().z) <= distanceThreshold) return;
        stateManager.hasPulled = !stateManager.hasPulled;
        StartCoroutine(SmartRotation(stateManager));
        if (stateManager.currentState is PauseState) return;
        stateManager.GetPulledInteraction();

    }

    private IEnumerator SmartRotation(GameStateManager stateManager)
    {
        float duration = 1f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            lever.rotation = Quaternion.Lerp(lever.rotation,
                stateManager.hasPulled ? Quaternion.Euler(0, 0, -45) : Quaternion.Euler(0, 0, 45), t);
            yield return null;
        }
    }

    private IEnumerator SmartPush()
    {
        float duration = 0.3f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            bridgeDoor.rotation = Quaternion.Lerp(bridgeDoor.rotation, Quaternion.Euler(0, -90, 0), t);
            yield return null;
        }
        Vector3 newPushPosition = new Vector3(0, Vector3.up.y, Vector3.back.z) * 5f;
        Ragdoll?.RagdollOn(_pushableAnimator, newPushPosition);
    }
}
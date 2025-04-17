using System.Collections;
using UnityEngine;

public class Chapter1Controller : ControllerBase, IPullable
{
    [SerializeField] private Transform lever;

    protected override void Start()
    {
        base.Start();
    }

    public void Pull(GameStateManager stateManager, PathController pathController)
    {
        if (Distance(pathController.GetPathPoints().z) <= distanceThreshold) return;
        if (pathController.pathSwitch == null) return;
        stateManager.hasPulled = !stateManager.hasPulled;
        StartCoroutine(SmartRotation(stateManager));
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
}
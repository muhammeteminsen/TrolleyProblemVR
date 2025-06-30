using System.Collections;
using UnityEngine;

public class Chapter1Controller : ControllerBase, IPullable
{
    [SerializeField] private Transform lever;
    
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
}
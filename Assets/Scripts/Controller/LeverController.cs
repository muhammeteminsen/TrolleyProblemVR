using System.Collections;
using UnityEngine;

public class LeverController : MonoBehaviour, IPullable
{
    [SerializeField] private Transform lever;
   
    public void Pull(GameStateManager stateManager, PathController pathController)
    {
        if (pathController.pathSwitch == null) return;
        if (stateManager.currentState is not UnSwitchState) return;
        stateManager.ChangeState(new SwitchState());
        StartCoroutine(SmartRotation());
        stateManager.hasSwitched = true;
    }
    private IEnumerator SmartRotation()
    {
        float duration = 1f;
        float elapsedTime = 0f;
        while (elapsedTime<duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            lever.rotation = Quaternion.Lerp(lever.rotation, Quaternion.Euler(0, 0, -45), t);
            yield return null;
        }
    }
}

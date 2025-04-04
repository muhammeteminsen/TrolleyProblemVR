using UnityEngine;

public class PushController : MonoBehaviour, IPushable
{
    public void Push()
    {
        Debug.Log("Pushing the object");
    }
}

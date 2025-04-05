using UnityEngine;

public class PushController : MonoBehaviour, IPushable
{
    [SerializeField] private GameObject pushableObject;
    private Animator _pushableAnimator;
    private bool _isPushed;

    private void Awake()
    {
        _pushableAnimator = pushableObject.transform.parent.GetComponent<Animator>();
        RagdollController.Instance.GetRagdoll(pushableObject);
        RagdollController.Instance.RagdollOff(_pushableAnimator);
    }

    public void Push()
    {
        if (_isPushed) return;
        Vector3 newPushPosition = new Vector3(0, Vector3.up.y, Vector3.back.z) * 10f;
        RagdollController.Instance.RagdollOn(_pushableAnimator, newPushPosition);
        _isPushed = true;
    }
}

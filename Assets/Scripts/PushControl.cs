using UnityEngine;

public class PushControl : MonoBehaviour
{
    private static readonly int IsPunch = Animator.StringToHash("isPunch");
    [SerializeField] private GameObject enemy;
    [SerializeField] private float distanceAmount;
    private Animator _enemyAnimator;
    private RagdollControl _instance;
    private bool _isPush;
    void Awake()
    {
        _instance = RagdollControl.Instance;
        _enemyAnimator = enemy.GetComponent<Animator>();
        _instance.GetRagdoll(enemy);
        _instance.RagdollOff(_enemyAnimator);
        _isPush = false;
    }

    

    private void OnMouseDown()
    {
        if (TrainMovement.Instance.GetPlayerToTrain()<distanceAmount || _isPush) return;
        GetComponent<Animator>().SetTrigger(IsPunch);
        _isPush = true;
    }

    public void PunchAnimationControl()
    {
        if (enemy==null)
            return;
        Vector3 forcePosition =
            new Vector3(enemy.transform.forward.x, enemy.transform.up.y, enemy.transform.position.z);
        _instance.RagdollOn(_enemyAnimator,forcePosition*3f);
    }
}

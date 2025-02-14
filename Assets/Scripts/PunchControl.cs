using UnityEngine;

public class PunchControl : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private Animator _enemyAnimator;
    private RagdollControl _instance;
    void Awake()
    {
        _instance = RagdollControl.Instance;
        _enemyAnimator = enemy.GetComponent<Animator>();
        _instance.GetRagdoll(enemy);
        _instance.RagdollOff(_enemyAnimator);
    }

    private void OnMouseDown()
    {
        GetComponent<Animator>().SetTrigger("isPunch");
    }

    public void PunchAnimationControl()
    {
        Vector3 forcePosition =
            new Vector3(enemy.transform.forward.x, enemy.transform.up.y, enemy.transform.position.z);
        _instance.RagdollOn(_enemyAnimator,forcePosition*3f);
    }
}

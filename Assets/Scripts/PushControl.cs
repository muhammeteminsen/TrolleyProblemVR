using System.Collections;
using UnityEngine;

public class PushControl : MonoBehaviour
{
    private static readonly int IsPunch = Animator.StringToHash("isPunch");
    private static readonly int IsPush = Animator.StringToHash("isPush");
    [SerializeField] private GameObject enemy;
    [SerializeField] private float distanceAmount;
    [SerializeField] private GameObject bridgeDoor;
    private Animator _enemyAnimator;
    private RagdollControl _instance;
    private LeverInteraction _leverInteraction;
    private bool _isPush;
    private bool _coroutineRun;
    void Awake()
    {
        _leverInteraction = FindAnyObjectByType<LeverInteraction>();
        _instance = RagdollControl.Instance;
        _enemyAnimator = enemy.GetComponent<Animator>();
        _instance.GetRagdoll(enemy);
        _instance.RagdollOff(_enemyAnimator);
        _isPush = false;
    }

    

    private void OnMouseDown()
    {
        if (_isPush) return;
        if (TrainMovement.Instance.GetPlayerToTrain()>distanceAmount && bridgeDoor==null)
        {
            GetComponent<Animator>().SetTrigger(IsPunch);
        }
        else if (bridgeDoor!=null)
        {
            GetComponent<Animator>().SetTrigger(IsPush);
        }
        
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

    public void PushAnimationControl()
    {
        _coroutineRun = true;
        if (!_coroutineRun) return;
        StartCoroutine(PushStart()); 
    }

    private IEnumerator PushStart()
    {
        Vector3 t = new Vector3(bridgeDoor.transform.rotation.x, bridgeDoor.transform.rotation.y - 90f,
            bridgeDoor.transform.rotation.z);
        float duration=.2f;
        float elapsedTime=0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float d = elapsedTime / duration;
            bridgeDoor.transform.rotation = Quaternion.Lerp(bridgeDoor.transform.rotation,Quaternion.Euler(t),d);
            _leverInteraction.GetLeverRotate(ref d);
            yield return null;
        }
        if (enemy==null) yield return null;
        _instance.RagdollOn(_enemyAnimator,enemy.transform.forward*7f);
        _coroutineRun = false;
    } 
}

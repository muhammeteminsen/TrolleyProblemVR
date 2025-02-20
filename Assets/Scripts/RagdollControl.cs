using UnityEngine;

public class RagdollControl : MonoBehaviour
{
    Collider[] _colliders;
    Rigidbody[] _rigidbodies;
    public static RagdollControl Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
    }

    public void GetRagdoll(GameObject enemyRagdollRig)
    {
        _colliders = enemyRagdollRig.GetComponentsInChildren<Collider>();
        _rigidbodies = enemyRagdollRig.GetComponentsInChildren<Rigidbody>();
    }

    public void RagdollOff(Animator enemyAnimator)
    {
        enemyAnimator.enabled = true;
        foreach (Collider ragdollCollider in _colliders)
        {
            ragdollCollider.enabled = false;
        }

        foreach (Rigidbody ragdollRigidbody in _rigidbodies)
        {
            ragdollRigidbody.isKinematic = true;
        }
    }

    public void RagdollOn(Animator enemyAnimator, Vector3 force)
    {
        enemyAnimator.enabled = false;
        foreach (Collider ragdollCollider in _colliders)
        {
            ragdollCollider.enabled = true;
        }

        foreach (Rigidbody ragdollRigidbody in _rigidbodies)
        {
            ragdollRigidbody.isKinematic = false;
            ragdollRigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}
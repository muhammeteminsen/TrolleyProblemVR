using UnityEngine;

public class RagdollControl : MonoBehaviour
{
    public void GetRagdollOn(GameObject target)
    {
        Animator animator = target.GetComponent<Animator>();
        animator.enabled = false;
    }
        
    
}

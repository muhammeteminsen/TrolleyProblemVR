using System;
using UnityEngine;

public class TrainTrigger : MonoBehaviour
{
    public static bool IsSwitchable;

    private void Start()
    {
        IsSwitchable = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Switchable"))
        {
            Debug.LogWarning("Switch");
            IsSwitchable = false;
            TrainMovement.Instance.isPathSwitch = false;
        }
    }
    
    
}

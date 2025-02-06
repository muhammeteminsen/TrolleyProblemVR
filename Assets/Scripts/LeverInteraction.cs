using UnityEngine;

public class LeverInteraction : MonoBehaviour
{
    private Quaternion _defaultRot;
    private bool _isLeverSwitch;

    private void Awake()
    {
        _defaultRot = transform.localRotation;
    }

    private void Update()
    {
        LeverRotation();
    }

    private void LeverRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation,
            !_isLeverSwitch
                ? Quaternion.Euler(_defaultRot.x, _defaultRot.y, 45)
                : Quaternion.Euler(_defaultRot.x, _defaultRot.y, -90), Time.deltaTime * 5f);
    }

    private void OnMouseDown()
    {
        _isLeverSwitch = !_isLeverSwitch;
        if (!TrainTrigger.IsSwitchable) return;
        TrainMovement.Instance.isPathSwitch = !TrainMovement.Instance.isPathSwitch;
        TrainMovement.Instance.currentPath = 0;
        

    }
}
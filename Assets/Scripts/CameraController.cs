using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _camera;
    private GameObject _train;
    private Vector3 _defaultCameraPosition;
    [SerializeField] private float cameraFollowSmoothness;
    private void Awake()
    {
        _camera = Camera.main;
        _train = GameObject.FindWithTag("Train");
        
        if (_camera != null)
            _defaultCameraPosition = _camera.transform.position;
    }

    private void FixedUpdate()
    {
        CameraPosition();
    }
    private void CameraPosition()
    {
        Vector3 cameraPosition =
            new Vector3(_defaultCameraPosition.x, _defaultCameraPosition.y, _camera.transform.position.z);
        _camera.transform.position =
            Vector3.Lerp(cameraPosition, _train.transform.position, Time.deltaTime*cameraFollowSmoothness);
    }
    
    
}
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;  // You can adjust this in the Inspector

    void Update()
    {
        // Move forward every frame
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}

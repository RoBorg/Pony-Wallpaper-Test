using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField]
    private float ySpeed = 1;

    [SerializeField, Range(1.01f, 2f)]
    private float yDecay = 2;

    private float currentYVelocity = 0;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            currentYVelocity = Input.GetAxis("Mouse X") * ySpeed;
        }

        transform.Rotate(0, currentYVelocity, 0);
        currentYVelocity /= yDecay;
    }
}

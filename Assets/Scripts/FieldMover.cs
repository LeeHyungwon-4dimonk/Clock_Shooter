using UnityEngine;
using UnityEngine.InputSystem;

public class FieldMover : MonoBehaviour
{
    [SerializeField] private float rotateAngle = 45f;
    [SerializeField] private float rotateSpeed = 10f;

    private float targetYRotation;

    void Start()
    {
        targetYRotation = transform.eulerAngles.y;
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        if (input.x > 0.1f)
        {
            targetYRotation += rotateAngle;
        }
        else if (input.x < -0.1f)
        {
            targetYRotation -= rotateAngle;
        }
    }

    void Update()
    {
        Quaternion targetRotation = Quaternion.Euler(0f, targetYRotation, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
    }
}
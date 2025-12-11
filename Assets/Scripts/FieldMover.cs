using DG.Tweening;
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

        transform.DORotate(new Vector3(0, targetYRotation, 0), 0.2f, RotateMode.Fast);
    }
}
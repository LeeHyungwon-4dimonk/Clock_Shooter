using UnityEngine;
using UnityEngine.InputSystem;

public class FieldMover : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 100f;
    private Vector2 input;

    public void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
    }

    void Update()
    {
        gameObject.transform.Rotate(Vector3.up, input.x * rotateSpeed * Time.deltaTime);
    }
}
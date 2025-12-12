using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class FieldMover : MonoBehaviour
{
    [SerializeField] private float _rotateAngle = 45f;
    [SerializeField] private float _rotateSpeed = 10f;

    private float _targetYRotation;

    void Start()
    {
        _targetYRotation = transform.eulerAngles.y;
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        if (input.x > 0.1f)
        {
            _targetYRotation += _rotateAngle;
            Manager.Game.AddTurn();
        }
        else if (input.x < -0.1f)
        {
            _targetYRotation -= _rotateAngle;
            Manager.Game.SubTurn();
        }

        transform.DORotate(new Vector3(0, _targetYRotation, 0), 0.2f, RotateMode.Fast);
    }
}
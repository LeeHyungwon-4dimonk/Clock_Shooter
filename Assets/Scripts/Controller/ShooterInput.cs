using UnityEngine;
using UnityEngine.InputSystem;

public class ShooterInput : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _bulletSpeed = 10f;

    private PlayerInput _playerInput;
    private InputAction _fireUpAction;
    private InputAction _fireDownAction;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _fireUpAction = _playerInput.actions["FireUp"];
        _fireDownAction = _playerInput.actions["FireDown"];
    }

    private void OnEnable()
    {
        _fireUpAction.performed += OnFireUp;
        _fireDownAction.performed += OnFireDown;
    }

    private void OnDisable()
    {
        _fireUpAction.performed -= OnFireUp;
        _fireDownAction.performed -= OnFireDown;
    }

    private void OnFireUp(InputAction.CallbackContext ctx)
    {
        Fire(Vector3.forward);
    }

    private void OnFireDown(InputAction.CallbackContext ctx)
    {
        Fire(Vector3.back);
    }

    private void Fire(Vector3 dir)
    {
        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().linearVelocity = dir * _bulletSpeed;
    }
}

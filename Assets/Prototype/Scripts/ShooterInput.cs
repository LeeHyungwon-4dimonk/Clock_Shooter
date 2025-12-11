using UnityEngine;
using UnityEngine.InputSystem;

public class ShooterInput : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 10f;

    private PlayerInput playerInput;
    private InputAction fireUpAction;
    private InputAction fireDownAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        fireUpAction = playerInput.actions["FireUp"];
        fireDownAction = playerInput.actions["FireDown"];
    }

    private void OnEnable()
    {
        fireUpAction.performed += OnFireUp;
        fireDownAction.performed += OnFireDown;
    }

    private void OnDisable()
    {
        fireUpAction.performed -= OnFireUp;
        fireDownAction.performed -= OnFireDown;
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().linearVelocity = dir * bulletSpeed;
    }
}

using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShooterInput : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _bulletSpeed = 10f;

    private PlayerInput _playerInput;
    private InputAction _fireUpAction;
    private InputAction _fireDownAction;

    public bool IsInitialized { get; private set; } = false;
    
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _fireUpAction = _playerInput.actions["FireUp"];
        _fireDownAction = _playerInput.actions["FireDown"];
    }

    private async void Start()
    {
        await InitAsync();
    }

    public async Task InitAsync()
    {
        if (IsInitialized) return;

        var _bulletPrefab = await AssetLoaderProvider.Loader.LoadAsync<GameObject>("Bullet");

        Manager.Pool.CreatePool("Bullet", _bulletPrefab, 4, "Bullets", this.transform);

        IsInitialized = true;
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
        GameObject bullet = Manager.Pool.Get("Bullet");
        bullet.transform.position = _firePoint.transform.position;
        bullet.GetComponent<Rigidbody>().linearVelocity = dir * _bulletSpeed;
        Manager.Game.turnStack.OtherAction();
    }
}
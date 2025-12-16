using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShooterInput : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 20f;
    [SerializeField] private float _firePointOffset = 1f;

    private Vector3[] _firePoints;
    private PlayerInput _playerInput;
    private InputAction _fireUpAction;
    private InputAction _fireDownAction;

    public bool IsInitialized { get; private set; } = false;
    
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _fireUpAction = _playerInput.actions["FireUp"];
        _fireDownAction = _playerInput.actions["FireDown"];

        _firePoints = new Vector3[2];
        _firePoints[0] = transform.position + new Vector3(0, 0, _firePointOffset);
        _firePoints[1] = transform.position + new Vector3(0, 0, -_firePointOffset);
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

        if (dir == Vector3.forward) bullet.transform.position = _firePoints[0];
        else bullet.transform.position = _firePoints[1];
        
        bullet.GetComponent<Rigidbody>().linearVelocity = dir * _bulletSpeed;
        Manager.Game.turnStack.OtherAction();
    }
}
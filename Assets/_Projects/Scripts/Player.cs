using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float _normalSpeed;
    [SerializeField] float _slowSpeed;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _bulletSpawnPoint;
    [SerializeField] float _bulletFireRate;
    
    int _remainingLives;
    bool _isSlowMode;
    bool _isFiring;
    float _nextFireTime;
    
    void Awake()
    {
        
    }
    
    void Start()
    {
        _remainingLives = 3;
    }
    
    void Update()
    {
        HandleInput();
        Move();
    }
    
    void HandleInput()
    {
        _isSlowMode = Keyboard.current.leftShiftKey.isPressed || Keyboard.current.rightShiftKey.isPressed;
    }
    
    void Move()
    {
        Vector3 movement = Vector3.zero;
        
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            movement.x = -1f;
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            movement.x = 1f;
        }
        if (Keyboard.current.upArrowKey.isPressed)
        {
            movement.y = 1f;
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            movement.y = -1f;
        }
        
        float currentSpeed = _isSlowMode ? _slowSpeed : _normalSpeed;
        transform.position += movement.normalized * currentSpeed * Time.deltaTime;
        
        // 画面端での移動制限（プレイエリア: 1152*1080、ピクセル=1m）
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -576f, 576f); // プレイエリア幅1152の半分
        pos.y = Mathf.Clamp(pos.y, -540f, 540f); // プレイエリア高1080の半分
        transform.position = pos;
    }
    
    void Fire()
    {
        
    }
    
    public void TakeDamage()
    {
        
    }
    
    public void AddLife()
    {
        
    }
    
    public int GetRemainingLives()
    {
        return 0;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
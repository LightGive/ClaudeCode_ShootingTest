using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float _normalSpeed;
    [SerializeField] float _slowSpeed;
    
    [SerializeField] GameSettings _gameSettings;
    [SerializeField] Transform _bulletSpawnPoint;
    [SerializeField] float _bulletFireRate;
    
    int _remainingLives;
    bool _isSlowMode;
    bool _isFiring;
    float _nextFireTime;
    
    void Awake()
    {
        // GameSettingsが未設定の場合はエラーを出して明確に通知
        if (_gameSettings == null)
        {
            Debug.LogError($"GameSettings is not assigned to Player. Please assign GameSettings in the inspector.", this);
        }
    }
    
    void Start()
    {
        _remainingLives = 3;
        
        // デフォルト値を設定（Inspectorで設定しない場合）
        if (_bulletFireRate <= 0f)
        {
            _bulletFireRate = 10f; // 秒間6発
        }
    }
    
    void Update()
    {
        HandleInput();
        Move();
        
        // Zキーを押している間、連射する
        if (_isFiring)
        {
            if (Time.time >= _nextFireTime)
            {
                Fire();
                _nextFireTime = Time.time + (1f / _bulletFireRate);
            }
        }
    }
    
    void HandleInput()
    {
        _isSlowMode = Keyboard.current.leftShiftKey.isPressed || Keyboard.current.rightShiftKey.isPressed;
        _isFiring = Keyboard.current.zKey.isPressed;
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
        
        // 画面端での移動制限（GameSettingsを使用）
        if (_gameSettings != null)
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, -_gameSettings.PlayAreaWidth / 2f, _gameSettings.PlayAreaWidth / 2f);
            pos.y = Mathf.Clamp(pos.y, -_gameSettings.PlayAreaHeight / 2f, _gameSettings.PlayAreaHeight / 2f);
            transform.position = pos;
        }
    }
    
    void Fire()
    {
        if (_bulletSpawnPoint != null)
        {
            BulletMovementData movementData = new BulletMovementData
            {
                Direction = Vector2.up,
                Speed = 500f,
                IsPlayerBullet = true
            };
            
            BulletPool.Instance.GetBullet(_bulletSpawnPoint.position, movementData);
        }
    }
    
    public void TakeDamage(int damage)
    {
        if (_remainingLives <= 0)
        {
            return; // すでにゲームオーバー状態
        }
        
        _remainingLives -= damage;
        
#if UNITY_EDITOR
        Debug.Log($"Player took {damage} damage. Remaining lives: {_remainingLives}");
#endif
        
        if (_remainingLives <= 0)
        {
            Die();
        }
    }

    void Die()
    {
#if UNITY_EDITOR
        Debug.Log("Player died!");
#endif
        // TODO: ゲームオーバー処理を実装
        // 一時的にオブジェクトを非アクティブ化
        gameObject.SetActive(false);
    }

    
    public void AddLife()
    {
        
    }
    
    public int GetRemainingLives()
    {
        return _remainingLives;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
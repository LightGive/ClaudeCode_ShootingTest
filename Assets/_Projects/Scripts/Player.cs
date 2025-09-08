using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] GameSettings _gameSettings;
    [SerializeField] Transform _bulletSpawnPoint;
    [SerializeField] float _bulletFireRate;
    
    int _remainingLives;
    bool _isSlowMode;
    bool _isFiring;
    float _nextFireTime;
    Rigidbody2D _rigidbody2D;
    
    // キャッシュされた速度値
    float _normalSpeed;
    float _slowSpeed;
    float _bulletSpeed;
    
    void Awake()
    {
        // GameSettingsが未設定の場合はエラーを出して明確に通知
        if (_gameSettings == null)
        {
            Debug.LogError($"GameSettings is not assigned to Player. Please assign GameSettings in the inspector.", this);
        }
        
        // Rigidbody2Dをキャッシュ
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (_rigidbody2D == null)
        {
            Debug.LogError($"Rigidbody2D component is required for Player movement. Please add Rigidbody2D component.", this);
        }
    }
    
    void Start()
    {
        _remainingLives = GameConstants.Defaults.DEFAULT_LIFE_COUNT;
        
        // 速度値をキャッシュしてパフォーマンス最適化
        if (_gameSettings != null)
        {
            _normalSpeed = _gameSettings.PlayerNormalSpeed;
            _slowSpeed = _gameSettings.PlayerSlowSpeed;
            _bulletSpeed = _gameSettings.PlayerBulletSpeed;
        }
        else
        {
            _normalSpeed = GameConstants.Defaults.PLAYER_NORMAL_SPEED;
            _slowSpeed = GameConstants.Defaults.PLAYER_SLOW_SPEED;
            _bulletSpeed = GameConstants.Defaults.PLAYER_BULLET_SPEED;
        }
        
        // デフォルト値を設定（Inspectorで設定しない場合）
        if (_bulletFireRate <= 0f)
        {
            _bulletFireRate = GameConstants.Defaults.DEFAULT_FIRE_RATE;
        }
    }
    
    void Update()
    {
        HandleInput();
        
        // Zキーを押している間、連射する
        if (_isFiring)
        {
            if (Time.time >= _nextFireTime)
            {
                Fire();
                _nextFireTime = Time.time + (GameConstants.Defaults.FIRE_INTERVAL_MULTIPLIER / _bulletFireRate);
            }
        }
    }
    
    void FixedUpdate()
    {
        Move();
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
            movement.x = GameConstants.Input.NEGATIVE_MOVE_INPUT_VALUE;
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            movement.x = GameConstants.Input.MOVE_INPUT_VALUE;
        }
        if (Keyboard.current.upArrowKey.isPressed)
        {
            movement.y = GameConstants.Input.MOVE_INPUT_VALUE;
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            movement.y = GameConstants.Input.NEGATIVE_MOVE_INPUT_VALUE;
        }
        
        if (movement != Vector3.zero)
        {
            float currentSpeed = _isSlowMode ? _slowSpeed : _normalSpeed;
            Vector3 newPosition = _rigidbody2D.position + (Vector2)(movement.normalized * currentSpeed * Time.fixedDeltaTime);
            
            // 画面端での移動制限（GameSettingsを使用）
            if (_gameSettings != null)
            {
                newPosition.x = Mathf.Clamp(newPosition.x, -_gameSettings.PlayAreaWidth / GameConstants.Boundaries.BOUNDARY_CALCULATION_DIVISOR, _gameSettings.PlayAreaWidth / GameConstants.Boundaries.BOUNDARY_CALCULATION_DIVISOR);
                newPosition.y = Mathf.Clamp(newPosition.y, -_gameSettings.PlayAreaHeight / GameConstants.Boundaries.BOUNDARY_CALCULATION_DIVISOR, _gameSettings.PlayAreaHeight / GameConstants.Boundaries.BOUNDARY_CALCULATION_DIVISOR);
            }
            
            _rigidbody2D.MovePosition(newPosition);
        }
    }
    
    void Fire()
    {
        if (_bulletSpawnPoint != null)
        {
            BulletMovementData movementData = new BulletMovementData(
                Vector2.up, 
                _bulletSpeed, 
                true
            );
            
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
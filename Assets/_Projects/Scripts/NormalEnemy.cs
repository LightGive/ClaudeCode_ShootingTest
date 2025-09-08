using UnityEngine;

public class NormalEnemy : Enemy
{
    [SerializeField] Vector2 _moveDirection;
    [SerializeField] GameSettings _gameSettings;
    [SerializeField] float _fireRate;
    
    EnemySpawnData _spawnData;
    float _nextFireTime;
    Rigidbody2D _rigidbody2D;
    
    void Awake()
    {
        // GameSettingsが未設定の場合の警告
        if (_gameSettings == null)
        {
            Debug.LogWarning($"GameSettings is not assigned to NormalEnemy. Please assign GameSettings in the inspector.", this);
        }
        
        // Rigidbody2Dをキャッシュ
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (_rigidbody2D == null)
        {
            Debug.LogError($"Rigidbody2D component is required for NormalEnemy movement. Please add Rigidbody2D component.", this);
        }
    }
    
    void Start()
    {
        // Initialize()で設定するようにしたため、ここでは何もしない
    }

    public override void Initialize(EnemySpawnData spawnData)
    {
        // 基底クラスの初期化を呼び出す（_isDead = false等）
        base.Initialize(spawnData);
        
        _spawnData = spawnData;
        _moveDirection = spawnData.MoveDirection.normalized;
        _moveSpeed = spawnData.MoveSpeed;
        _fireRate = spawnData.FireRate;
        _nextFireTime = Time.time + spawnData.FireDelay;
        
        // HealthをspawnDataから設定
        _health = spawnData.Health;
        
#if UNITY_EDITOR
        Debug.Log($"NormalEnemy {gameObject.name} initialized - MoveDirection: {_moveDirection}, MoveSpeed: {_moveSpeed}, Health: {_health}");
#endif
    }

    protected override void Move()
    {
        Vector2 newPosition = _rigidbody2D.position + (_moveDirection * _moveSpeed * Time.fixedDeltaTime);
        _rigidbody2D.MovePosition(newPosition);
        
        // 画面外判定
        Vector3 pos = transform.position;
        if (_gameSettings != null && 
            (pos.x < _gameSettings.LeftBoundary || pos.x > _gameSettings.RightBoundary || 
             pos.y < _gameSettings.BottomBoundary || pos.y > _gameSettings.TopBoundary))
        {
            // 画面外ではアイテムドロップなしで破棄
            Die(false); // shouldDropItem = false
        }
    }
    
    protected override void Attack()
    {
        if (_spawnData == null || _spawnData.BulletPatterns == null || _spawnData.BulletPatterns.Length == 0)
        {
            return;
        }
        
        // _fireRateが0の場合は攻撃しない（ゼロ除算を防止）
        if (_fireRate <= 0f)
        {
            return;
        }
        
        // BulletPoolの存在チェック
        if (BulletPool.Instance == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"BulletPool.Instance is null for enemy {gameObject.name}");
#endif
            return;
        }
            
        if (Time.time >= _nextFireTime)
        {
            foreach (var bulletPattern in _spawnData.BulletPatterns)
            {
                BulletPool.Instance.GetBullet(transform.position, bulletPattern);
            }
            _nextFireTime = Time.time + (GameConstants.Defaults.FIRE_INTERVAL_MULTIPLIER / _fireRate);
        }
    }
    
    protected override void Die(bool shouldDropItem = true)
    {
        base.Die(shouldDropItem);
    }
}
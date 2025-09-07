using UnityEngine;

public class NormalEnemy : Enemy
{
    [SerializeField] Vector2 _moveDirection;
    [SerializeField] GameSettings _gameSettings;
    [SerializeField] float _fireRate;
    
    EnemySpawnData _spawnData;
    float _nextFireTime;
    
    void Awake()
    {
        
    }
    
    void Start()
    {
        // Initialize()で設定するようにしたため、ここでは何もしない
    }

    public override void Initialize(EnemySpawnData spawnData)
    {
        _spawnData = spawnData;
        _moveDirection = spawnData.MoveDirection;
        _moveSpeed = spawnData.MoveSpeed;
        _fireRate = spawnData.FireRate;
        _nextFireTime = Time.time + spawnData.FireDelay;
        
        // HealthをspawnDataから設定
        _health = spawnData.Health;
    }

    protected override void Move()
    {
        transform.position += (Vector3)(_moveDirection * _moveSpeed * Time.deltaTime);
        
        // 画面外判定
        Vector3 pos = transform.position;
        if (_gameSettings != null && 
            (pos.x < _gameSettings.LeftBoundary || pos.x > _gameSettings.RightBoundary || 
             pos.y < _gameSettings.BottomBoundary || pos.y > _gameSettings.TopBoundary))
        {
            TriggerOnDestroyed();
            Destroy(gameObject);
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
            
        if (Time.time >= _nextFireTime)
        {
            foreach (var bulletPattern in _spawnData.BulletPatterns)
            {
                BulletPool.Instance.GetBullet(transform.position, bulletPattern);
            }
            _nextFireTime = Time.time + (1f / _fireRate);
        }
    }
    
    protected override void Die()
    {
#if UNITY_EDITOR
        Debug.Log("NormalEnemy Die() called, calling base.Die()");
#endif
        base.Die();
    }
}
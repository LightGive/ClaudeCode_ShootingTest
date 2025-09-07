using UnityEngine;

public class NormalEnemy : Enemy
{
    [SerializeField] Vector2 _moveDirection;
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

    public void Initialize(EnemySpawnData spawnData)
    {
        _spawnData = spawnData;
        _moveDirection = spawnData.MoveDirection;
        _moveSpeed = spawnData.MoveSpeed;
        _fireRate = spawnData.FireRate;
        _nextFireTime = Time.time + spawnData.FireDelay;
        
        // Healthをここで初期化
        _health = 1;
    }

    
    protected override void Move()
    {
        transform.position += (Vector3)(_moveDirection * _moveSpeed * Time.deltaTime);
        
        // 画面外判定
        Vector3 pos = transform.position;
        if (pos.x < -700f || pos.x > 700f || pos.y < -700f || pos.y > 700f)
        {
            OnDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
    
    protected override void Attack()
    {
        if (_spawnData == null || _spawnData.BulletPatterns == null || _spawnData.BulletPatterns.Length == 0)
            return;
            
        if (Time.time >= _nextFireTime)
        {
            foreach (var bulletPattern in _spawnData.BulletPatterns)
            {
                BulletPool.Instance.GetBullet(transform.position, bulletPattern);
            }
            _nextFireTime = Time.time + (1f / _fireRate);
        }
    }
    
    public override void TakeDamage(int damage)
    {
        Debug.Log($"NormalEnemy TakeDamage called. Before: Health={_health}, Damage={damage}, IsDead={_isDead}");
        
        if (_isDead)
        {
            Debug.Log("Enemy is already dead, skipping damage");
            return;
        }
        
        _health -= damage;
        Debug.Log($"After damage: Health={_health}");
        
        if (_health <= 0)
        {
            Debug.Log("Health <= 0, calling Die()");
            Die();
        }
    }
    
    protected override void Die()
    {
        Debug.Log("NormalEnemy Die() called, calling base.Die()");
        base.Die();
    }
}
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Vector2 _direction;
    [SerializeField] int _damage;
    [SerializeField] bool _hasHit = false;
    [SerializeField] bool _isPlayerBullet;
    [SerializeField] GameSettings _gameSettings;
    
    void Awake()
    {
        // GameSettingsが未設定の場合の警告
        if (_gameSettings == null)
        {
            Debug.LogWarning($"GameSettings is not assigned to Bullet prefab. Using fallback values. Please assign GameSettings in the inspector.");
        }
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        Move();
        CheckBounds();
    }
    
    void Move()
    {
        transform.position += (Vector3)(_direction * _speed * Time.deltaTime);
    }
    
    void CheckBounds()
    {
        Vector3 pos = transform.position;
        
        // 画面外判定（プレイエリア + マージン）
        bool isOutOfBounds = false;
        
        if (_gameSettings != null)
        {
            // GameSettingsを使用した正確な境界判定
            isOutOfBounds = (pos.x < _gameSettings.BulletLeftBoundary || pos.x > _gameSettings.BulletRightBoundary || 
                           pos.y < _gameSettings.BulletBottomBoundary || pos.y > _gameSettings.BulletTopBoundary);
        }
        else
        {
            // フォールバック: デフォルト値を使用
            isOutOfBounds = (pos.x < GameConstants.Boundaries.DEFAULT_LEFT_BOUNDARY || pos.x > GameConstants.Boundaries.DEFAULT_RIGHT_BOUNDARY || pos.y < GameConstants.Boundaries.DEFAULT_BOTTOM_BOUNDARY || pos.y > GameConstants.Boundaries.DEFAULT_TOP_BOUNDARY);
        }
        
        if (isOutOfBounds)
        {
            BulletPool.Instance.ReturnBullet(this);
        }
    }
    
    public void Initialize(Vector2 direction, float speed, int damage, bool isPlayerBullet)
    {
        _direction = direction.normalized;
        _speed = speed;
        _damage = damage;
        _isPlayerBullet = isPlayerBullet;
        _hasHit = false; // ヒットフラグをリセット
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (_hasHit) return; // 既にヒットしている場合は処理しない
        
        bool shouldDestroy = _isPlayerBullet ? HandleEnemyHit(other) : HandlePlayerHit(other);
        
        // 当たった場合の共通処理
        if (shouldDestroy)
        {
            _hasHit = true;
            DestroyBullet();
        }
    }

    bool HandleEnemyHit(Collider2D other)
    {
        // タグで事前フィルタリング（パフォーマンス最適化）
        if (!other.CompareTag(GameConstants.Tags.ENEMY)) return false;
        
        // 敵との当たり判定
        var enemy = other.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(_damage);
            return true;
        }
        
        return false;
    }

    bool HandlePlayerHit(Collider2D other)
    {
        // タグで事前フィルタリング（パフォーマンス最適化）
        if (!other.CompareTag(GameConstants.Tags.PLAYER)) return false;
        
        // プレイヤーとの当たり判定
        var player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            player.TakeDamage(_damage);
            return true;
        }
        
        return false;
    }
    
    void DestroyBullet()
    {
        BulletPool.Instance.ReturnBullet(this);
    }
}
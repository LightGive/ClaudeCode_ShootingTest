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
            isOutOfBounds = (pos.x < -650f || pos.x > 650f || pos.y < -650f || pos.y > 650f);
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
        
        bool shouldDestroy = false;
        
        // プレイヤーの弾の場合
        if (_isPlayerBullet)
        {
            // タグで事前フィルタリング（パフォーマンス最適化）
            if (other.CompareTag("Enemy"))
            {
                // 敵との当たり判定
                var enemy = other.GetComponentInParent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(_damage);
                    shouldDestroy = true;
                }
            }
        }
        // 敵の弾の場合
        else
        {
            // タグで事前フィルタリング（パフォーマンス最適化）
            if (other.CompareTag("Player"))
            {
                // プレイヤーとの当たり判定
                Player player = other.GetComponentInParent<Player>();
                if (player != null)
                {
                    player.TakeDamage(_damage);
                    shouldDestroy = true;
                }
            }
        }
        
        // 当たった場合の共通処理
        if (shouldDestroy)
        {
            _hasHit = true;
            DestroyBullet();
        }
    }
    
    void DestroyBullet()
    {
        BulletPool.Instance.ReturnBullet(this);
    }
}
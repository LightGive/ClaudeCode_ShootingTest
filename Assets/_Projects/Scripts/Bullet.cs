using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Vector2 _direction;
    [SerializeField] int _damage;
        [SerializeField] bool _hasHit = false;
        [SerializeField] GameSettings _gameSettings;
[SerializeField] bool _isPlayerBullet;
    
    void Awake()
    {
        
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
        if (_gameSettings != null && 
            (pos.x < _gameSettings.BulletLeftBoundary || pos.x > _gameSettings.BulletRightBoundary || 
             pos.y < _gameSettings.BulletBottomBoundary || pos.y > _gameSettings.BulletTopBoundary))
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
        
#if UNITY_EDITOR
        Debug.Log($"Bullet collision with: {other.gameObject.name}, IsPlayerBullet: {_isPlayerBullet}");
#endif
        
        bool shouldDestroy = false;
        
        // プレイヤーの弾の場合
        if (_isPlayerBullet)
        {
            // 敵との当たり判定
            var enemy = other.GetComponent<Enemy>();
#if UNITY_EDITOR
            Debug.Log($"Enemy component found: {enemy != null}");
#endif
            if (enemy != null)
            {
#if UNITY_EDITOR
                Debug.Log($"Calling TakeDamage with damage: {_damage}");
#endif
                enemy.TakeDamage(_damage);
                shouldDestroy = true;
            }
        }
        // 敵の弾の場合
        else
        {
            // プレイヤーとの当たり判定
            Player player = other.GetComponentInParent<Player>();
            if (player != null)
            {
                player.TakeDamage();
                shouldDestroy = true;
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
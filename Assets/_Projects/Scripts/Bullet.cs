using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Vector2 _direction;
    [SerializeField] int _damage;
        [SerializeField] bool _hasHit = false;
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
        if (pos.x < -600f || pos.x > 600f || pos.y < -600f || pos.y > 600f)
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
        
        Debug.Log($"Bullet collision with: {other.gameObject.name}, IsPlayerBullet: {_isPlayerBullet}");
        
        // プレイヤーの弾の場合
        if (_isPlayerBullet)
        {
            // 敵との当たり判定
            var enemy = other.GetComponent<Enemy>();
            Debug.Log($"Enemy component found: {enemy != null}");
            if (enemy != null)
            {
                _hasHit = true; // ヒットフラグをセット
                Debug.Log($"Calling TakeDamage with damage: {_damage}");
                enemy.TakeDamage(_damage);
                DestroyBullet();
                return;
            }
        }
        // 敵の弾の場合
        else
        {
            // プレイヤーとの当たり判定
            Player player = other.GetComponentInParent<Player>();
            if (player != null)
            {
                _hasHit = true; // ヒットフラグをセット
                player.TakeDamage();
                DestroyBullet();
                return;
            }
        }
    }
    
    void DestroyBullet()
    {
        BulletPool.Instance.ReturnBullet(this);
    }
}
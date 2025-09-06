using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Vector2 _direction;
    [SerializeField] int _damage;
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
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
    
    void DestroyBullet()
    {
        BulletPool.Instance.ReturnBullet(this);
    }
}
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] int _defaultCapacity = 100;
    [SerializeField] int _maxPoolSize = 500;
    
    ObjectPool<Bullet> _pool;
    
    public static BulletPool Instance { get; private set; }
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializePool();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void InitializePool()
    {
        _pool = new ObjectPool<Bullet>(
            createFunc: () => CreateBullet(),
            actionOnGet: bullet => OnGetBullet(bullet),
            actionOnRelease: bullet => OnReleaseBullet(bullet),
            actionOnDestroy: bullet => OnDestroyBullet(bullet),
            collectionCheck: false,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxPoolSize
        );
    }
    
    Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(_bulletPrefab);
        return bullet;
    }
    
    void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }
    
    void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
    
    void OnDestroyBullet(Bullet bullet)
    {
        if (bullet != null)
        {
            Destroy(bullet.gameObject);
        }
    }
    
    public Bullet GetBullet(Vector3 position, BulletMovementData movementData)
    {
        Bullet bullet = _pool.Get();
        bullet.transform.position = position;
        bullet.Initialize(movementData.Direction, movementData.Speed, movementData.Damage, movementData.IsPlayerBullet);
        return bullet;
    }
    
    public void ReturnBullet(Bullet bullet)
    {
        if (bullet != null && bullet.gameObject.activeInHierarchy)
        {
            _pool.Release(bullet);
        }
    }
}
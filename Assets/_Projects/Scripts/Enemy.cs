using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected GameObject _bulletPrefab;
    [SerializeField] protected float _lifeItemDropRate;
    
    public event System.Action<Enemy> OnDestroyed;
    protected bool _isDead;
    
    protected void TriggerOnDestroyed()
    {
        OnDestroyed?.Invoke(this);
    }
    
    void Awake()
    {
        
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (!_isDead)
        {
            Move();
            Attack();
        }
    }
    
    protected abstract void Move();
    
    protected abstract void Attack();
    
    public virtual void Initialize(EnemySpawnData spawnData)
    {
        // 基本的な初期化処理
        // 派生クラスでオーバーライドして具体的な初期化を行う
    }
    
    public virtual void TakeDamage(int damage)
    {
#if UNITY_EDITOR
        Debug.Log("TakeDamage" + _isDead + damage);
#endif
        if (_isDead)
        {
            return;
        }
        
        _health -= damage;
        
        if (_health <= 0)
        {
            Die();
        }
    }
    
    protected virtual void Die()
    {
#if UNITY_EDITOR
        Debug.Log($"Enemy Die() called for {gameObject.name}");
#endif
        
        if (_isDead)
        {
#if UNITY_EDITOR
            Debug.Log("Enemy is already dead, skipping Die()");
#endif
            return;
        }
        
        _isDead = true;
#if UNITY_EDITOR
        Debug.Log("Set _isDead = true");
#endif
        
        DropLifeItem();
#if UNITY_EDITOR
        Debug.Log("DropLifeItem() called");
#endif
        
#if UNITY_EDITOR
        Debug.Log("Invoking OnDestroyed event");
#endif
        OnDestroyed?.Invoke(this);
        
#if UNITY_EDITOR
        Debug.Log($"Calling Destroy() on {gameObject.name}");
#endif
        Destroy(gameObject);
#if UNITY_EDITOR
        Debug.Log("Destroy() called");
#endif
    }
    
    protected void DropLifeItem()
    {
#if UNITY_EDITOR
        Debug.Log($"DropLifeItem() called, drop rate: {_lifeItemDropRate}");
#endif
        
        if (Random.Range(0f, 1f) < _lifeItemDropRate)
        {
#if UNITY_EDITOR
            Debug.Log("Should drop life item, but not implemented yet");
#endif
            // TODO: ライフアイテムの生成処理
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("No life item drop");
#endif
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
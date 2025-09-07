using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected GameObject _bulletPrefab;
    [SerializeField] protected float _lifeItemDropRate;
    
        
    public System.Action OnDestroyed;
protected bool _isDead;
    
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
    
    public virtual void TakeDamage(int damage)
    {
        Debug.Log("TakeDamage" + _isDead + damage);
        if (_isDead) return;
        
        _health -= damage;
        
        if (_health <= 0)
        {
            Die();
        }
    }
    
    protected virtual void Die()
    {
        Debug.Log($"Enemy Die() called for {gameObject.name}");
        
        if (_isDead)
        {
            Debug.Log("Enemy is already dead, skipping Die()");
            return;
        }
        
        _isDead = true;
        Debug.Log("Set _isDead = true");
        
        DropLifeItem();
        Debug.Log("DropLifeItem() called");
        
        Debug.Log("Invoking OnDestroyed event");
        OnDestroyed?.Invoke();
        
        Debug.Log($"Calling Destroy() on {gameObject.name}");
        Destroy(gameObject);
        Debug.Log("Destroy() called");
    }
    
    protected void DropLifeItem()
    {
        Debug.Log($"DropLifeItem() called, drop rate: {_lifeItemDropRate}");
        
        if (Random.Range(0f, 1f) < _lifeItemDropRate)
        {
            Debug.Log("Should drop life item, but not implemented yet");
            // TODO: ライフアイテムの生成処理
        }
        else
        {
            Debug.Log("No life item drop");
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
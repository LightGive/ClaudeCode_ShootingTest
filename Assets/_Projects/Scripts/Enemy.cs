using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected GameObject _bulletPrefab;
    [SerializeField] protected float _lifeItemDropRate;
    
    protected bool _isDead;
    
    void Awake()
    {
        
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    
    protected abstract void Move();
    
    protected abstract void Attack();
    
    public virtual void TakeDamage(int damage)
    {
        
    }
    
    protected virtual void Die()
    {
        
    }
    
    protected void DropLifeItem()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected GameObject _bulletPrefab;
    [SerializeField] protected float _lifeItemDropRate;
    
    public event System.Action<Enemy> OnDestroyed;
    protected bool _isDead;
    
    void Update()
    {
        if (!_isDead)
        {
            Attack();
        }
    }
    
    void FixedUpdate()
    {
        if (!_isDead)
        {
            Move();
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
    
    protected virtual void Die(bool shouldDropItem = true)
    {
        if (_isDead)
        {
            return;
        }
        
        _isDead = true;
        
        // アイテムドロップ処理（必要な場合のみ）
        if (shouldDropItem)
        {
            DropLifeItem();
        }
        
        // イベントを発行（オブジェクトがまだ有効な状態）
        OnDestroyed?.Invoke(this);
        
        // オブジェクトの破棄を次フレームに遅らせて安全性を向上
        StartCoroutine(DestroyNextFrame());
    }
    
    protected void DropLifeItem()
    {
        if (Random.Range(0f, 1f) < _lifeItemDropRate)
        {
            // TODO: ライフアイテムの生成処理
        }
    }
    
    System.Collections.IEnumerator DestroyNextFrame()
    {
        // 次フレームまで待機してから破棄
        yield return null;
        
        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
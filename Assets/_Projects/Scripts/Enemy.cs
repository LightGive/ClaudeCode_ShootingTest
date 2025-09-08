using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected GameObject _bulletPrefab;
    [SerializeField] protected float _lifeItemDropRate;
    
    public event System.Action<Enemy> OnDestroyed;
    protected bool _isDead;
    
    // プール用の元プレファブ参照
    protected GameObject _originalPrefab;
    
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
        // プールから取得時の初期化
        _isDead = false;
        
        // 基本的な初期化処理
        // 派生クラスでオーバーライドして具体的な初期化を行う
    }
    
    // プール用のプレファブ設定
    public void SetOriginalPrefab(GameObject prefab)
    {
        _originalPrefab = prefab;
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
        
#if UNITY_EDITOR
        Debug.Log($"Enemy {gameObject.name} is dying. shouldDropItem: {shouldDropItem}");
#endif
        
        _isDead = true;
        
        // アイテムドロップ処理（必要な場合のみ）
        if (shouldDropItem)
        {
            DropLifeItem();
        }
        
#if UNITY_EDITOR
        Debug.Log($"Enemy {gameObject.name} firing OnDestroyed event");
#endif
        
        // イベントを発行（オブジェクトがまだ有効な状態）
        OnDestroyed?.Invoke(this);
        
#if UNITY_EDITOR
        Debug.Log($"Enemy {gameObject.name} returning to pool");
#endif
        
        // プールに返却
        if (EnemyPool.Instance != null && _originalPrefab != null)
        {
            EnemyPool.Instance.ReturnEnemy(this, _originalPrefab);
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning($"Enemy {gameObject.name} - Pool or prefab is null, using fallback destroy");
#endif
            // フォールバック: プールがない場合は従来通り破棄
            StartCoroutine(DestroyNextFrame());
        }
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
using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class EnemyPool : MonoBehaviour
{
    [System.Serializable]
    public class EnemyPoolData
    {
        [Tooltip("敵のプレファブ")]
        public GameObject enemyPrefab;
        [Tooltip("プールの初期容量")]
        public int defaultCapacity = 5;
        [Tooltip("プールの最大サイズ")]
        public int maxPoolSize = 20;
    }
    
    [SerializeField] EnemyPoolData[] _enemyPoolData;
    
    Dictionary<GameObject, ObjectPool<Enemy>> _enemyPools = new Dictionary<GameObject, ObjectPool<Enemy>>();
    
    public static EnemyPool Instance { get; private set; }
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializePools();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void InitializePools()
    {
        foreach (var poolData in _enemyPoolData)
        {
            if (poolData.enemyPrefab == null)
            {
                Debug.LogWarning($"Enemy prefab is null in EnemyPool", this);
                continue;
            }
            
            var pool = new ObjectPool<Enemy>(
                createFunc: () => CreateEnemy(poolData.enemyPrefab),
                actionOnGet: enemy => OnGetEnemy(enemy),
                actionOnRelease: enemy => OnReleaseEnemy(enemy),
                actionOnDestroy: enemy => OnDestroyEnemy(enemy),
                collectionCheck: false,
                defaultCapacity: poolData.defaultCapacity,
                maxSize: poolData.maxPoolSize
            );
            
            _enemyPools[poolData.enemyPrefab] = pool;
        }
    }
    
    Enemy CreateEnemy(GameObject prefab)
    {
        GameObject enemyObj = Instantiate(prefab, transform);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        if (enemy == null)
        {
            Debug.LogError($"Enemy component not found on prefab: {prefab.name}", this);
        }
        return enemy;
    }
    
    void OnGetEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }
    
    void OnReleaseEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        // 親をEnemyPoolに戻す
        enemy.transform.SetParent(transform);
    }
    
    void OnDestroyEnemy(Enemy enemy)
    {
        if (enemy != null)
        {
            Destroy(enemy.gameObject);
        }
    }
    
    public Enemy GetEnemy(GameObject prefab, Vector3 position, EnemySpawnData spawnData, Transform parent = null)
    {
        if (!_enemyPools.ContainsKey(prefab))
        {
            Debug.LogError($"No pool found for enemy prefab: {prefab.name}", this);
            return null;
        }
        
        Enemy enemy = _enemyPools[prefab].Get();
        enemy.transform.position = position;
        enemy.transform.rotation = Quaternion.identity;
        
        // 親を設定（指定されていない場合はEnemySpawnerに設定される想定）
        if (parent != null)
        {
            enemy.transform.SetParent(parent);
        }
        
        enemy.Initialize(spawnData);
        return enemy;
    }
    
    public void ReturnEnemy(Enemy enemy, GameObject prefab)
    {
        if (!_enemyPools.ContainsKey(prefab))
        {
            Debug.LogError($"No pool found for enemy prefab: {prefab.name}", this);
            return;
        }
        
        if (enemy != null && enemy.gameObject.activeInHierarchy)
        {
            _enemyPools[prefab].Release(enemy);
        }
    }
}
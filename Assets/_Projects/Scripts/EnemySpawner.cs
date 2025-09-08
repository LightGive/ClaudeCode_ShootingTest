using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    [Header("ウェーブ設定")]
    [SerializeField] EnemyWave[] _enemyWaves;
    [SerializeField] float _waveClearDelay = GameConstants.Defaults.WAVE_CLEAR_DELAY;
    
    [Header("デバッグ")]
    [SerializeField] bool _autoStartWaves = true;
    
    int _currentWaveIndex;
    bool _isWaveActive;
    HashSet<Enemy> _activeEnemies = new HashSet<Enemy>();
    
    // ソート結果キャッシュ（パフォーマンス最適化）
    Dictionary<EnemyWave, EnemySpawnData[]> _sortedSpawnCache = new Dictionary<EnemyWave, EnemySpawnData[]>();
    
    public System.Action OnWaveCompleted;
    public System.Action OnAllWavesCompleted;
    
    void Start()
    {
        if (_autoStartWaves && _enemyWaves.Length > 0)
        {
            StartWave(0);
        }
    }
    
    public void StartWave(int waveIndex)
    {
        if (waveIndex < 0 || waveIndex >= _enemyWaves.Length)
        {
            Debug.LogWarning($"Wave index {waveIndex} is out of range");
            return;
        }
        
        if (_isWaveActive)
        {
            Debug.LogWarning("Wave is already active");
            return;
        }
        
        _currentWaveIndex = waveIndex;
        _isWaveActive = true;
        _activeEnemies.Clear();
        
        StartCoroutine(SpawnWaveCoroutine(_enemyWaves[waveIndex]));
    }
    
    public void StartNextWave()
    {
        if (_currentWaveIndex + 1 < _enemyWaves.Length)
        {
            StartWave(_currentWaveIndex + 1);
        }
        else
        {
            OnAllWavesCompleted?.Invoke();
        }
    }
    
    IEnumerator SpawnWaveCoroutine(EnemyWave wave)
    {
#if UNITY_EDITOR
        Debug.Log($"Starting wave: {wave.WaveName}");
#endif
        
        // キャッシュされたソート結果を取得または初回ソート実行
        EnemySpawnData[] sortedSpawns;
        if (_sortedSpawnCache.ContainsKey(wave))
        {
            // キャッシュから取得（O(1)）
            sortedSpawns = _sortedSpawnCache[wave];
#if UNITY_EDITOR
            Debug.Log($"Using cached sorted spawn data for wave: {wave.WaveName}");
#endif
        }
        else
        {
            // 初回のみソートしてキャッシュに保存
            sortedSpawns = wave.EnemySpawns.OrderBy(s => s.SpawnDelay).ToArray();
            _sortedSpawnCache[wave] = sortedSpawns;
#if UNITY_EDITOR
            Debug.Log($"Sorted and cached spawn data for wave: {wave.WaveName}");
#endif
        }
        
        float waveStartTime = Time.time;
        int spawnIndex = 0;
        bool allEnemiesSpawned = false;
        
        // ウェーブ完了まで継続的にチェック
        while (true)
        {
            float elapsedTime = Time.time - waveStartTime;
            
            // まだ出現していない敵をチェック
            if (!allEnemiesSpawned)
            {
                // 出現時間が来た敵を全て生成
                while (spawnIndex < sortedSpawns.Length && sortedSpawns[spawnIndex].SpawnDelay <= elapsedTime)
                {
                    SpawnEnemy(sortedSpawns[spawnIndex]);
                    spawnIndex++;
                }
                
                // 全ての敵が出現完了したかチェック
                if (spawnIndex >= sortedSpawns.Length)
                {
                    allEnemiesSpawned = true;
#if UNITY_EDITOR
                    Debug.Log("All enemies spawned");
#endif
                }
            }
            
            // ウェーブ完了条件をチェック
            bool waveCompleted = false;
            
            if (wave.WaitForAllEnemiesDestroyed)
            {
                // 全敵撃破待ち
                waveCompleted = allEnemiesSpawned && _activeEnemies.Count == 0;
            }
            else
            {
                // 時間経過待ち（ウェーブ開始からの総時間）
                waveCompleted = elapsedTime >= wave.WaveDuration;
            }
            
            if (waveCompleted)
            {
#if UNITY_EDITOR
                Debug.Log($"Wave completed after {elapsedTime:F1} seconds");
#endif
                break;
            }
            
            yield return null; // 次フレームまで待機
        }
        
        // ウェーブクリア処理
        yield return new WaitForSeconds(_waveClearDelay);
        CompleteWave();
    }
    
    void SpawnEnemy(EnemySpawnData spawnData)
    {
        if (spawnData.EnemyPrefab == null)
        {
            Debug.LogWarning("Enemy prefab is null");
            return;
        }
        
        GameObject enemyObj = Instantiate(spawnData.EnemyPrefab, spawnData.SpawnPosition, Quaternion.identity, transform);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        
        if (enemy != null)
        {
            // 敵の初期化
            enemy.Initialize(spawnData);
            
            _activeEnemies.Add(enemy);
            enemy.OnDestroyed += OnEnemyDestroyed;
        }
    }
    
    void OnEnemyDestroyed(Enemy enemy)
    {
        _activeEnemies.Remove(enemy);
        
        // イベント登録解除してメモリリークを防止
        enemy.OnDestroyed -= OnEnemyDestroyed;
    }
    
    void CompleteWave()
    {
        _isWaveActive = false;
        OnWaveCompleted?.Invoke();
        
#if UNITY_EDITOR
        Debug.Log($"Wave {_currentWaveIndex + 1} completed!");
#endif
        
        // 次のウェーブを開始
        if (_autoStartWaves)
        {
            StartNextWave();
        }
    }
    
    public bool IsWaveActive()
    {
        return _isWaveActive;
    }
    
    public int GetCurrentWaveIndex()
    {
        return _currentWaveIndex;
    }
    
    public int GetActiveEnemyCount()
    {
        return _activeEnemies.Count;
    }
    
    void OnDestroy()
    {
        // Spawnerが破棄される際に、残存する敵のイベント登録を全て解除
        foreach (Enemy enemy in _activeEnemies)
        {
            if (enemy != null)
            {
                enemy.OnDestroyed -= OnEnemyDestroyed;
            }
        }
        _activeEnemies.Clear();
    }
}
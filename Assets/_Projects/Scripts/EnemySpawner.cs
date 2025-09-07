using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("ウェーブ設定")]
    [SerializeField] EnemyWave[] _enemyWaves;
    [SerializeField] float _waveClearDelay = 2f;
    
    [Header("デバッグ")]
    [SerializeField] bool _autoStartWaves = true;
    
    int _currentWaveIndex;
    bool _isWaveActive;
    List<Enemy> _activeEnemies = new List<Enemy>();
    
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
        Debug.Log($"Starting wave: {wave.WaveName}");
        
        // 敵を順次生成
        foreach (var spawnData in wave.EnemySpawns)
        {
            yield return new WaitForSeconds(spawnData.SpawnDelay);
            SpawnEnemy(spawnData);
        }
        
        // ウェーブ完了条件を待機
        if (wave.WaitForAllEnemiesDestroyed)
        {
            yield return new WaitUntil(() => _activeEnemies.Count == 0);
        }
        else
        {
            yield return new WaitForSeconds(wave.WaveDuration);
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
        
        GameObject enemyObj = Instantiate(spawnData.EnemyPrefab, spawnData.SpawnPosition, Quaternion.identity);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        
        if (enemy != null)
        {
            // 敵の初期化
            if (enemy is NormalEnemy normalEnemy)
            {
                normalEnemy.Initialize(spawnData);
            }
            
            _activeEnemies.Add(enemy);
            enemy.OnDestroyed += () => OnEnemyDestroyed(enemy);
        }
    }
    
    void OnEnemyDestroyed(Enemy enemy)
    {
        _activeEnemies.Remove(enemy);
    }
    
    void CompleteWave()
    {
        _isWaveActive = false;
        OnWaveCompleted?.Invoke();
        
        Debug.Log($"Wave {_currentWaveIndex + 1} completed!");
        
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
}
using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour
{
    [SerializeField] string _stageName;
    [SerializeField] GameObject[] _normalEnemyPrefabs;
    [SerializeField] GameObject _bossPrefab;
    [SerializeField] float _stageIntroductionDuration;
    [SerializeField] Transform[] _enemySpawnPoints;
    
    int _remainingNormalEnemies;
    bool _bossSpawned;
    bool _stageCleared;
    BossEnemy _currentBoss;
    
    void Awake()
    {
        
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    
    public void StartStage()
    {
        
    }
    
    IEnumerator ShowStageIntroduction()
    {
        yield return null;
    }
    
    void SpawnNormalEnemies()
    {
        
    }
    
    void CheckNormalEnemiesStatus()
    {
        
    }
    
    void StartBossBattle()
    {
        
    }
    
    public void OnEnemyDestroyed()
    {
        
    }
    
    public void OnBossDefeated()
    {
        
    }
    
    public bool IsStageCleared()
    {
        return _stageCleared;
    }
    
    public string GetStageName()
    {
        return _stageName;
    }
    
    public BossEnemy GetCurrentBoss()
    {
        return _currentBoss;
    }
}
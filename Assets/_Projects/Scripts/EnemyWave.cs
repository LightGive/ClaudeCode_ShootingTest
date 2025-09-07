using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Wave", menuName = "Shooting Game/Enemy Wave")]
public class EnemyWave : ScriptableObject
{
    [Header("ウェーブ情報")]
    public string WaveName;
    [TextArea(2, 4)]
    public string Description;
    
    [Header("敵出現データ")]
    public EnemySpawnData[] EnemySpawns;
    
    [Header("ウェーブ設定")]
    public float WaveDuration = 10f;
    public bool WaitForAllEnemiesDestroyed = true;
    
    public int GetTotalEnemyCount()
    {
        return EnemySpawns != null ? EnemySpawns.Length : 0;
    }
    
    public float GetMaxSpawnTime()
    {
        if (EnemySpawns == null || EnemySpawns.Length == 0)
        {
            return 0f;
        }
            
        float maxTime = 0f;
        foreach (var spawn in EnemySpawns)
        {
            if (spawn.SpawnDelay > maxTime)
            {
                maxTime = spawn.SpawnDelay;
            }
        }
        return maxTime;
    }
}
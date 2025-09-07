using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    [Header("敵情報")]
        public int Health;
public GameObject EnemyPrefab;
    public Vector3 SpawnPosition;
    public float SpawnDelay;
    
    [Header("移動設定")]
    public Vector2 MoveDirection;
    public float MoveSpeed;
    
    [Header("弾丸設定")]
    public BulletMovementData[] BulletPatterns;
    public float FireRate;
    public float FireDelay;
    
    public EnemySpawnData()
    {
        Health = 1;
        SpawnPosition = Vector3.zero;
        SpawnDelay = 0f;
        MoveDirection = Vector2.down;
        MoveSpeed = 100f;
        BulletPatterns = new BulletMovementData[0];
        FireRate = 1f;
        FireDelay = 1f;
    }
}
using UnityEngine;

[System.Serializable]
public class BulletMovementData
{
    public Vector2 Direction;
    public float Speed;
    public bool IsPlayerBullet;
    public int Damage = 1;
    public float Lifetime = 10f;
    
    public BulletMovementData()
    {
        Direction = Vector2.up;
        Speed = 300f;
        IsPlayerBullet = false;
        Damage = 1;
        Lifetime = 10f;
    }
    
    public BulletMovementData(Vector2 direction, float speed, bool isPlayerBullet, int damage = 1, float lifetime = 10f)
    {
        Direction = direction;
        Speed = speed;
        IsPlayerBullet = isPlayerBullet;
        Damage = damage;
        Lifetime = lifetime;
    }
}
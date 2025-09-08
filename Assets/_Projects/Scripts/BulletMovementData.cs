using UnityEngine;

[System.Serializable]
public class BulletMovementData
{
    public Vector2 Direction;
    public float Speed;
    public bool IsPlayerBullet;
    public int Damage = GameConstants.Defaults.DEFAULT_DAMAGE;
    public float Lifetime = GameConstants.Defaults.DEFAULT_BULLET_LIFETIME;
    
    public BulletMovementData()
    {
        Direction = Vector2.up;
        Speed = GameConstants.Defaults.DEFAULT_BULLET_SPEED;
        IsPlayerBullet = false;
        Damage = GameConstants.Defaults.DEFAULT_DAMAGE;
        Lifetime = GameConstants.Defaults.DEFAULT_BULLET_LIFETIME;
    }
    
    public BulletMovementData(Vector2 direction, float speed, bool isPlayerBullet, int damage = GameConstants.Defaults.DEFAULT_DAMAGE, float lifetime = GameConstants.Defaults.DEFAULT_BULLET_LIFETIME)
    {
        Direction = direction;
        Speed = speed;
        IsPlayerBullet = isPlayerBullet;
        Damage = damage;
        Lifetime = lifetime;
    }
}
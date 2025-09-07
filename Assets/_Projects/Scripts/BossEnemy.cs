using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField] int _maxHealth;
    [SerializeField] float[] _phaseFireRates;
    [SerializeField] GameObject[] _bulletPatterns;
    
    int _currentPhase;
    float _nextFireTime;
    
    void Awake()
    {
        
    }
    
    void Start()
    {
        
    }
    
    protected override void Move()
    {
        
    }
    
    protected override void Attack()
    {
        
    }
    
    void UpdatePhase()
    {
        
    }
    
    void FireBulletPattern(int patternIndex)
    {
        
    }
    
    public override void TakeDamage(int damage)
    {
        
    }
    
    protected override void Die(bool shouldDropItem = true)
    {
#if UNITY_EDITOR
        Debug.Log("BossEnemy Die() called, calling base.Die()");
#endif
        base.Die(shouldDropItem);
    }
    
    public float GetHealthPercentage()
    {
        return 0f;
    }
}
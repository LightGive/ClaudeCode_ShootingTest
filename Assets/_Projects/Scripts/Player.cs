using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _normalSpeed;
    [SerializeField] float _slowSpeed;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _bulletSpawnPoint;
    [SerializeField] float _bulletFireRate;
    
    int _remainingLives;
    bool _isSlowMode;
    bool _isFiring;
    float _nextFireTime;
    
    void Awake()
    {
        
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    
    void HandleInput()
    {
        
    }
    
    void Move()
    {
        
    }
    
    void Fire()
    {
        
    }
    
    public void TakeDamage()
    {
        
    }
    
    public void AddLife()
    {
        
    }
    
    public int GetRemainingLives()
    {
        return 0;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
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
        _remainingLives = 3;
        _normalSpeed = 5f;
        _slowSpeed = 2f;
    }
    
void Update()
    {
        HandleInput();
        Move();
    }
    
void HandleInput()
    {
        _isSlowMode = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }
    
void Move()
    {
        Vector3 movement = Vector3.zero;
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement.x = -1f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement.x = 1f;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement.y = 1f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movement.y = -1f;
        }
        
        float currentSpeed = _isSlowMode ? _slowSpeed : _normalSpeed;
        transform.position += movement.normalized * currentSpeed * Time.deltaTime;
        
        // 画面端での移動制限（プレイエリア: 1152*1080）
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -5.76f, 5.76f); // 1152/2 = 576, Unity units = 576/100 = 5.76
        pos.y = Mathf.Clamp(pos.y, -5.4f, 5.4f);   // 1080/2 = 540, Unity units = 540/100 = 5.4
        transform.position = pos;
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
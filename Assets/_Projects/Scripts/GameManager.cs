using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _totalStages;
    [SerializeField] Player _player;
    [SerializeField] UIManager _uiManager;
    [SerializeField] StageManager _stageManager;
    
    int _currentStage;
    GameState _gameState;
    
    public enum GameState
    {
        Menu,
        Playing,
        Paused,
        GameOver,
        GameClear
    }
    
    void Awake()
    {
        
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    
    public void StartGame()
    {
        
    }
    
    public void PauseGame()
    {
        
    }
    
    public void ResumeGame()
    {
        
    }
    
    public void GameOver()
    {
        
    }
    
    public void GameClear()
    {
        
    }
    
    public void NextStage()
    {
        
    }
    
    public void RestartGame()
    {
        
    }
    
    public GameState GetGameState()
    {
        return _gameState;
    }
    
    public int GetCurrentStage()
    {
        return _currentStage;
    }
}
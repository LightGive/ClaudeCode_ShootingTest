using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Game UI")]
    [SerializeField] TextMeshProUGUI _livesText;
    [SerializeField] Slider _bossHealthBar;
    [SerializeField] TextMeshProUGUI _stageNameText;
    [SerializeField] GameObject _bossHealthPanel;
    
    [Header("Menu UI")]
    [SerializeField] GameObject _mainMenuPanel;
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] GameObject _gameClearPanel;
    [SerializeField] GameObject _pausePanel;
    
    [Header("Buttons")]
    [SerializeField] Button _startButton;
    [SerializeField] Button _restartButton;
    [SerializeField] Button _pauseButton;
    [SerializeField] Button _resumeButton;
    [SerializeField] Button _quitButton;
    
    void Awake()
    {
        
    }
    
    void Start()
    {
        
    }
    
    public void UpdateLivesDisplay(int lives)
    {
        
    }
    
    public void UpdateBossHealthBar(float healthPercentage)
    {
        
    }
    
    public void ShowBossHealthBar()
    {
        
    }
    
    public void HideBossHealthBar()
    {
        
    }
    
    public void ShowStageIntroduction(string stageName)
    {
        
    }
    
    public void HideStageIntroduction()
    {
        
    }
    
    public void ShowMainMenu()
    {
        
    }
    
    public void ShowGameOverPanel()
    {
        
    }
    
    public void ShowGameClearPanel()
    {
        
    }
    
    public void ShowPausePanel()
    {
        
    }
    
    public void HideAllPanels()
    {
        
    }
    
    void SetupButtonEvents()
    {
        
    }
}
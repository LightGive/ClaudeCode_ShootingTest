using UnityEngine;

public static class GameConstants
{
    // タグ定数
    public static class Tags
    {
        public const string ENEMY = "Enemy";
        public const string PLAYER = "Player";
    }
    
    // 境界値定数
    public static class Boundaries
    {
        public const float DEFAULT_LEFT_BOUNDARY = -650f;
        public const float DEFAULT_RIGHT_BOUNDARY = 650f;
        public const float DEFAULT_TOP_BOUNDARY = 650f;
        public const float DEFAULT_BOTTOM_BOUNDARY = -650f;
        
        public const float BOUNDARY_MARGIN = 100f;
        public const float BULLET_BOUNDARY_MARGIN = 50f;
        public const float BOUNDARY_CALCULATION_DIVISOR = 2f;
    }
    
    // デフォルト値定数
    public static class Defaults
    {
        public const int DEFAULT_HEALTH = 1;
        public const int DEFAULT_DAMAGE = 1;
        public const int DEFAULT_LIFE_COUNT = 3;
        
        public const float DEFAULT_BULLET_SPEED = 300f;
        public const float DEFAULT_BULLET_LIFETIME = 10f;
        public const float DEFAULT_FIRE_RATE = 10f;
        public const float DEFAULT_FIRE_DELAY = 1f;
        public const float DEFAULT_MOVE_SPEED = 100f;
        public const float DEFAULT_WAVE_DURATION = 10f;
        
        public const float PLAYER_BULLET_SPEED = 500f;
        public const float PLAYER_NORMAL_SPEED = 300f;
        public const float PLAYER_SLOW_SPEED = 100f;
        
        public const float WAVE_CLEAR_DELAY = 2f;
        public const float FIRE_INTERVAL_MULTIPLIER = 1f;
        
        public const int DEFAULT_BULLET_POOL_CAPACITY = 100;
        public const int MAX_BULLET_POOL_SIZE = 500;
        
        public const float ITEM_DROP_CHANCE = 1f;
    }
    
    // プレイエリア定数
    public static class PlayArea
    {
        public const float WIDTH = 1152f;
        public const float HEIGHT = 1080f;
    }
    
    // UI定数
    public static class UI
    {
        public const int TEXT_AREA_MIN_LINES = 2;
        public const int TEXT_AREA_MAX_LINES = 4;
    }
    
    // 入力定数
    
    // メニュー関連定数
    public static class MenuPaths
    {
        public const string GAME_SETTINGS_MENU = "Shooting Game/Game Settings";
        public const string ENEMY_WAVE_MENU = "Shooting Game/Enemy Wave";
    }
    
    // ファイル名定数
    public static class FileNames
    {
        public const string GAME_SETTINGS_FILE = "Game Settings";
        public const string ENEMY_WAVE_FILE = "New Enemy Wave";
    }
    
    // 数値定数
    public static class Numbers
    {
        public const int ZERO = 0;
        public const int ONE = 1;
        public const float ZERO_F = 0f;
    }
    public static class Input
    {
        public const float MOVE_INPUT_VALUE = 1f;
        public const float NEGATIVE_MOVE_INPUT_VALUE = -1f;
    }
}
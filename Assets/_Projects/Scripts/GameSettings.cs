using UnityEngine;

[CreateAssetMenu(fileName = "Game Settings", menuName = "Shooting Game/Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("画面設定")]
    [Tooltip("プレイエリアの幅（ピクセル）")]
    public float PlayAreaWidth = GameConstants.PlayArea.WIDTH;
    
    [Tooltip("プレイエリアの高さ（ピクセル）")]
    public float PlayAreaHeight = GameConstants.PlayArea.HEIGHT;
    
    [Header("境界設定")]
    [Tooltip("オブジェクトが画面外と判定される境界のマージン")]
    public float BoundaryMargin = GameConstants.Boundaries.BOUNDARY_MARGIN;
    
    // 計算用プロパティ
    public float LeftBoundary => -(PlayAreaWidth / GameConstants.Boundaries.BOUNDARY_CALCULATION_DIVISOR) - BoundaryMargin;
    public float RightBoundary => (PlayAreaWidth / GameConstants.Boundaries.BOUNDARY_CALCULATION_DIVISOR) + BoundaryMargin;
    public float TopBoundary => (PlayAreaHeight / GameConstants.Boundaries.BOUNDARY_CALCULATION_DIVISOR) + BoundaryMargin;
    public float BottomBoundary => -(PlayAreaHeight / GameConstants.Boundaries.BOUNDARY_CALCULATION_DIVISOR) - BoundaryMargin;
    
    [Header("プレイヤー設定")]
    [Tooltip("プレイヤーの通常移動速度")]
    public float PlayerNormalSpeed = GameConstants.Defaults.PLAYER_NORMAL_SPEED;
    
    [Tooltip("プレイヤーの低速移動速度")]
    public float PlayerSlowSpeed = GameConstants.Defaults.PLAYER_SLOW_SPEED;
    
    [Tooltip("プレイヤーの弾丸速度")]
    public float PlayerBulletSpeed = GameConstants.Defaults.PLAYER_BULLET_SPEED;
    
    [Header("弾丸設定")]
    [Tooltip("弾丸の画面外判定マージン")]
    public float BulletBoundaryMargin = GameConstants.Boundaries.BULLET_BOUNDARY_MARGIN;
    
    // 弾丸用境界プロパティ
    public float BulletLeftBoundary => -(PlayAreaWidth / GameConstants.Boundaries.BOUNDARY_CALCULATION_DIVISOR) - BulletBoundaryMargin;
    public float BulletRightBoundary => (PlayAreaWidth / GameConstants.Boundaries.BOUNDARY_CALCULATION_DIVISOR) + BulletBoundaryMargin;
    public float BulletTopBoundary => (PlayAreaHeight / GameConstants.Boundaries.BOUNDARY_CALCULATION_DIVISOR) + BulletBoundaryMargin;
    public float BulletBottomBoundary => -(PlayAreaHeight / GameConstants.Boundaries.BOUNDARY_CALCULATION_DIVISOR) - BulletBoundaryMargin;
}
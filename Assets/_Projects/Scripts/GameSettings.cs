using UnityEngine;

[CreateAssetMenu(fileName = "Game Settings", menuName = "Shooting Game/Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("画面設定")]
    [Tooltip("プレイエリアの幅（ピクセル）")]
    public float PlayAreaWidth = 1152f;
    
    [Tooltip("プレイエリアの高さ（ピクセル）")]
    public float PlayAreaHeight = 1080f;
    
    [Header("境界設定")]
    [Tooltip("オブジェクトが画面外と判定される境界のマージン")]
    public float BoundaryMargin = 100f;
    
    // 計算用プロパティ
    public float LeftBoundary => -(PlayAreaWidth / 2f) - BoundaryMargin;
    public float RightBoundary => (PlayAreaWidth / 2f) + BoundaryMargin;
    public float TopBoundary => (PlayAreaHeight / 2f) + BoundaryMargin;
    public float BottomBoundary => -(PlayAreaHeight / 2f) - BoundaryMargin;
    
    [Header("プレイヤー設定")]
    [Tooltip("プレイヤーの通常移動速度")]
    public float PlayerNormalSpeed = 300f;
    
    [Tooltip("プレイヤーの低速移動速度")]
    public float PlayerSlowSpeed = 100f;
    
    [Header("弾丸設定")]
    [Tooltip("弾丸の画面外判定マージン")]
    public float BulletBoundaryMargin = 50f;
    
    // 弾丸用境界プロパティ
    public float BulletLeftBoundary => -(PlayAreaWidth / 2f) - BulletBoundaryMargin;
    public float BulletRightBoundary => (PlayAreaWidth / 2f) + BulletBoundaryMargin;
    public float BulletTopBoundary => (PlayAreaHeight / 2f) + BulletBoundaryMargin;
    public float BulletBottomBoundary => -(PlayAreaHeight / 2f) - BulletBoundaryMargin;
}
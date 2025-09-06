
# Conversation Guidelines

- 常に日本語で会話する

# Claude Code Development Guidelines

このプロジェクトでClaudeがコード開発を行う際のガイドラインです。

## コード規約

### 命名規則
- プライベート変数、プライベート関数は`private`修飾子は省略してもよい
- 明示的な型指定を推奨（varは型が明らかな場合のみ）

#### クラス名: PascalCase

```csharp
public class Hiyoko
{
    
}
```
#### インターフェース: Iプレフィックス + PascalCase
```csharp
public interface IPiyo
{
    
}
```
#### メソッド名: PascalCase
```csharp
void Piyo()
{
    
}
```
#### プライベートフィールド: アンダースコアプレフィックス+camelCase
```csharp
float _moveSpeed
```
#### パブリックフィールド: PascalCase
```csharp
public float MoveSpeed
```
#### ローカルフィールド変数名: camelCase
```csharp
float moveSpeed
```
#### 定数: PascalCase
```csharp
const float MoveSpeed
```

### フォーマット規則
```csharp
// 中括弧の配置（Allman スタイル）
if (condition)
{
    DoSomething();
}

// 単一行if文でも中括弧を使用
if (health <= 0)
{
    Die();
}
```
### メソッドの定義順
- Unityメソッド（Awake,Update等）
- publicメソッド
- privateメソッド

### パフォーマンス考慮事項
- `Update()`での重い処理を避ける
- オブジェクトプールの活用を検討
- `FindObjectOfType()`は初期化時のみ使用
- 対象のオブジェクトに対して`GetComponent`が2回以上呼ばれる場合はキャッシュで参照を持っておく

### ビルド・テスト
- **コンパイルコマンド**: MCPのUnity Natural機能を使用
- **テスト実行**: Unity Editor Test Runnerを使用

### Git運用
- Git-flowを使用
- コミット前にコンパイルエラーがないことを確認
- 機能単位での細かいコミット
- コミットメッセージは日本語で簡潔に
- 以下のブランチは必ず削除しない
  - develop
  - master

このガイドラインに従ってコード開発を行ってください。
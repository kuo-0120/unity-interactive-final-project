# Unity 互動接物遊戲

使用 Unity 製作的 3D 九宮格接物遊戲：玩家以滑鼠或觸控移動籃子，接到蘋果加 100 分，接到炸彈則分數減半，倒數結束後停止生成與計分。

## 功能

- 九宮格點選／觸控移動
- 隨機生成蘋果與炸彈
- 碰撞、音效、計分與倒數
- Editor 滑鼠測試與行動裝置觸控
- Unity 2022.3.40f1

## 開啟

1. 使用 Unity Hub 加入此資料夾。
2. 以 Unity 2022.3.40f1 開啟。
3. 開啟 `Assets/GameScene.unity` 後執行。

## 專案結構

- `Assets/*.cs`：遊戲邏輯
- `Assets/GameScene.unity`：主場景
- `Packages/`：Unity package manifest
- `ProjectSettings/`：可重現的專案設定
- `media/demo.mp4`：操作展示

[觀看操作影片](media/demo.mp4)

`Library/`、`Temp/`、`Logs/`、build、APK、IDE 專案檔與 TextMesh Pro 範例內容均未提交。TextMesh Pro 由 Unity package 管理；FBX 與音效為課程專案素材，公開再利用前仍應確認原始授權。

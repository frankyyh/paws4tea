# Title Screen / End Screen 设置指南（图片 + 按键）

目标：
- **Title Screen**：一张图片，**按空格开始游戏**
- **End Screen**：一张图片，游戏结束时显示
- **任何时候按 `R`**：直接回到 Title Screen（重新开始）

这些逻辑由 `Assets/Scripts/ScreenManager.cs` 控制。

---

## 1) 在 Canvas 里放两张图片

### 1.1 创建 Canvas
1. Hierarchy 右键 → **UI** → **Canvas**
2. （可选）Canvas 上的 **Canvas Scaler**
   - UI Scale Mode: **Scale With Screen Size**

### 1.2 Title 图片
1. 在 Canvas 下右键 → **UI** → **Image**
2. 重命名为：`TitleScreen`
3. 选中 `TitleScreen`，在 Inspector：
   - **Image** 组件的 **Source Image**：拖入你的 Title 图片 sprite
   - **RectTransform**：把图片铺满屏幕  
     - Anchor 设为 Stretch（左右上下拉满）
     - Left/Right/Top/Bottom 都设为 0

### 1.3 End 图片
1. 在 Canvas 下右键 → **UI** → **Image**
2. 重命名为：`EndScreen`
3. 同样把 **Source Image** 设为你的 End 图片 sprite，并铺满屏幕
4. 先把 `EndScreen` **取消勾选**（默认隐藏）

---

## 2) 创建 ScreenManager，并挂引用

1. Hierarchy 右键 → **Create Empty**
2. 重命名：`ScreenManager`
3. Add Component → 添加脚本：`ScreenManager`
4. 在 `ScreenManager` 组件里拖引用：
   - **Title Screen** → 拖 `TitleScreen`（那个 Image 对象）
   - **End Screen** → 拖 `EndScreen`
   - **Hud Root**（可选）→ 拖你的 HUD 根节点（比如显示分数/错过的那一组 UI）
     - 如果你暂时没有 HUD，可以先留空

---

## 3) BGMPlayer 的配合

`BGMPlayer` 现在**不会在场景加载时自动播放**，而是：
- **按空格开始游戏** → `ScreenManager` 会调用 `BGMPlayer.PlayBGM()`
- **游戏结束 / 按 R 回 Title** → `ScreenManager` 会调用 `BGMPlayer.StopBGM()`

你只需要确保场景里有一个 `BGMPlayer` 对象，并在它的 `BGM Clip` 里分配音乐。

---

## 4) 关于 “按 R 重新开始”

`R` 会做这些事：
- 回到 Title Screen
- 停止 BGM
- 清掉场上所有狗狗
- 重置分数/错过计数
- 游戏暂停（`Time.timeScale = 0`）

然后你按空格即可重新开始。

---

## 5) 最终 Hierarchy 参考

```
Main Camera
Canvas
  TitleScreen (Image)   [默认显示]
  EndScreen   (Image)   [默认隐藏]
  HUDRoot (可选)
    ScoreText
    MissedText
ScreenManager
GameManager
DogSpawner
DetectionZone
Player
BGMPlayer
EventSystem
```


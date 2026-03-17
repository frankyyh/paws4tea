# UI设置指南

## 快速设置UI

### 方法1：使用Unity UI系统（推荐）

1. **创建Canvas**
   - Hierarchy中右键 → `UI` → `Canvas`
   - 设置Canvas的Render Mode为 `Screen Space - Overlay`

2. **创建分数文本**
   - 在Canvas下右键 → `UI` → `Text`，命名为 `ScoreText`
   - 设置位置：左上角（例如：X=-400, Y=200）
   - 设置文本内容为："成功敬茶: 0"
   - 设置字体大小：24-30

3. **创建错过计数文本**
   - 在Canvas下右键 → `UI` → `Text`，命名为 `MissedText`
   - 设置位置：ScoreText下方（例如：X=-400, Y=150）
   - 设置文本内容为："错过: 0/3"
   - 设置字体大小：24-30

4. **创建游戏结束面板**
   - 在Canvas下右键 → `UI` → `Panel`，命名为 `GameOverPanel`
   - 设置Panel颜色为半透明黑色（Alpha约200）
   - 在GameOverPanel下创建两个Text：
     - `GameOverText`: "游戏结束！"（大字体，居中）
     - `FinalScoreText`: "你一共给 0 只狗狗成功敬茶了！"（中等字体，居中）

5. **设置UIManager**
   - 在Hierarchy中创建空GameObject，命名为 `UIManager`
   - 添加 `UIManager` 脚本组件
   - 将所有UI元素拖到对应的字段：
     - Score Text → ScoreText
     - Missed Text → MissedText
     - Game Over Panel → GameOverPanel
     - Game Over Text → GameOverText
     - Final Score Text → FinalScoreText

### 方法2：使用Console输出（临时测试）

如果暂时不想设置UI，游戏会在Console中输出信息：
- 成功敬茶时会显示："成功对狗狗敬茶！总数: X"
- 错过狗狗时会显示："错过了一只狗狗！错过数: X/3"
- 游戏结束时会显示："游戏结束！你成功给 X 只狗狗敬茶了！"

## 注意事项

- 确保Canvas的Sort Order足够高，这样UI会显示在最前面
- 如果使用TextMeshPro，需要将UIManager.cs中的`Text`改为`TextMeshProUGUI`
- GameOverPanel默认应该是隐藏的（在Inspector中取消勾选）

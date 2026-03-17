# UI设置详细步骤

## 📋 步骤概览

1. 创建Canvas（画布）
2. 创建分数显示文本
3. 创建错过计数文本
4. 创建游戏结束面板
5. 设置UIManager组件

---

## 🎨 详细步骤

### 步骤 1: 创建Canvas（画布）

1. 在 **Hierarchy** 窗口中，右键点击空白处
2. 选择 **UI** → **Canvas**
3. 这会自动创建：
   - Canvas（主画布）
   - EventSystem（事件系统，用于处理UI交互）

4. 选中 **Canvas**，在 Inspector 中确认：
   - **Render Mode**: `Screen Space - Overlay`（默认）
   - **Canvas Scaler** → **UI Scale Mode**: `Scale With Screen Size`（推荐）

---

### 步骤 2: 创建分数文本（成功敬茶数）

1. 在 **Hierarchy** 中，右键点击 **Canvas**
2. 选择 **UI** → **Text** → **Text**
3. 重命名为 `ScoreText`

4. 在 **Inspector** 中设置：
   - **Rect Transform**:
     - **Anchor Presets**: 点击左上角的方框，选择左上角（Top-Left）
     - **Pos X**: `-400`（或根据你的屏幕调整）
     - **Pos Y**: `200`（或根据你的屏幕调整）
     - **Width**: `300`
     - **Height**: `50`
   
   - **Text (Script)**:
     - **Text**: `成功敬茶: 0`
     - **Font Size**: `30`
     - **Color**: 白色或你喜欢的颜色
     - **Alignment**: 左对齐（左上角）

---

### 步骤 3: 创建错过计数文本

1. 在 **Hierarchy** 中，右键点击 **Canvas**
2. 选择 **UI** → **Text** → **Text**
3. 重命名为 `MissedText`

4. 在 **Inspector** 中设置：
   - **Rect Transform**:
     - **Anchor Presets**: 左上角（Top-Left）
     - **Pos X**: `-400`（与ScoreText相同）
     - **Pos Y**: `140`（在ScoreText下方，约60像素间距）
     - **Width**: `300`
     - **Height**: `50`
   
   - **Text (Script)**:
     - **Text**: `错过: 0/3`
     - **Font Size**: `30`
     - **Color**: 红色或黄色（警告色）
     - **Alignment**: 左对齐

---

### 步骤 4: 创建游戏结束面板

#### 4.1 创建Panel（面板）

1. 在 **Hierarchy** 中，右键点击 **Canvas**
2. 选择 **UI** → **Panel**
3. 重命名为 `GameOverPanel`

4. 在 **Inspector** 中设置：
   - **Rect Transform**:
     - **Anchor Presets**: 按住Alt键，点击中间的方框（全屏拉伸）
     - 确保 **Left, Right, Top, Bottom** 都是 `0`
   
   - **Image (Script)**:
     - **Color**: 点击颜色框，设置：
       - **R**: `0`
       - **G**: `0`
       - **B**: `0`
       - **A**: `200`（半透明黑色背景）

#### 4.2 在Panel中创建"游戏结束"文本

1. 在 **Hierarchy** 中，右键点击 **GameOverPanel**
2. 选择 **UI** → **Text** → **Text**
3. 重命名为 `GameOverText`

4. 在 **Inspector** 中设置：
   - **Rect Transform**:
     - **Anchor Presets**: 中间（Center）
     - **Pos X**: `0`
     - **Pos Y**: `50`（在中心上方一点）
     - **Width**: `400`
     - **Height**: `60`
   
   - **Text (Script)**:
     - **Text**: `游戏结束！`
     - **Font Size**: `48`
     - **Font Style**: **Bold**（粗体）
     - **Color**: 白色或红色
     - **Alignment**: 居中

#### 4.3 在Panel中创建最终得分文本

1. 在 **Hierarchy** 中，右键点击 **GameOverPanel**
2. 选择 **UI** → **Text** → **Text**
3. 重命名为 `FinalScoreText`

4. 在 **Inspector** 中设置：
   - **Rect Transform**:
     - **Anchor Presets**: 中间（Center）
     - **Pos X**: `0`
     - **Pos Y**: `-30`（在GameOverText下方）
     - **Width**: `500`
     - **Height**: `50`
   
   - **Text (Script)**:
     - **Text**: `你一共给 0 只狗狗成功敬茶了！`
     - **Font Size**: `36`
     - **Color**: 白色或黄色
     - **Alignment**: 居中

5. **重要**：在 **Hierarchy** 中，取消勾选 **GameOverPanel**（让它默认隐藏）

---

### 步骤 5: 设置UIManager组件

1. 在 **Hierarchy** 中，右键点击空白处
2. 选择 **Create Empty**
3. 重命名为 `UIManager`

4. 在 **Inspector** 中，点击 **Add Component**
5. 搜索并添加 `UIManager` 脚本

6. 将UI元素拖到对应字段：
   - **Score Text**: 从Hierarchy拖入 `ScoreText`
   - **Missed Text**: 从Hierarchy拖入 `MissedText`
   - **Game Over Panel**: 从Hierarchy拖入 `GameOverPanel`
   - **Game Over Text**: 从Hierarchy拖入 `GameOverText`
   - **Final Score Text**: 从Hierarchy拖入 `FinalScoreText`

---

## ✅ 最终检查清单

- [ ] Canvas已创建
- [ ] EventSystem已自动创建
- [ ] ScoreText已创建并设置好位置和文本
- [ ] MissedText已创建并设置好位置和文本
- [ ] GameOverPanel已创建，背景为半透明黑色
- [ ] GameOverText在Panel中，显示"游戏结束！"
- [ ] FinalScoreText在Panel中，显示得分信息
- [ ] GameOverPanel默认是隐藏的（未勾选）
- [ ] UIManager GameObject已创建
- [ ] UIManager脚本已添加
- [ ] UIManager的所有字段都已分配好对应的UI元素

---

## 🎮 测试

1. 点击 **Play** 按钮
2. 你应该看到：
   - 左上角显示"成功敬茶: 0"
   - 左上角下方显示"错过: 0/3"
3. 当游戏结束时：
   - 屏幕中央应该显示半透明黑色面板
   - 显示"游戏结束！"
   - 显示最终得分

---

## 💡 提示

- 如果文本太小或太大，调整 **Font Size**
- 如果位置不合适，在 **Scene** 视图中可以直接拖动UI元素
- 如果看不到UI，确保Canvas的 **Render Mode** 是 `Screen Space - Overlay`
- 可以在 **Game** 视图中实时预览UI效果

---

## 🎨 美化建议（可选）

- 给文本添加阴影效果：在Text组件中勾选 **Shadow**
- 调整字体：在Text组件中点击 **Font** 字段，选择其他字体
- 添加背景框：可以在文本下方添加一个半透明的Image作为背景

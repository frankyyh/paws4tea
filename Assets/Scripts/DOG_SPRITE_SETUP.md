# 多套狗狗Sprite设置指南

现在狗狗支持多套不同肤色的sprite，每只狗狗会随机选择一套。

## 📋 设置步骤

### 步骤 1: 准备Sprite资源

确保你有至少一套（建议3套）完整的sprite：
- **Normal Sprite**: 普通状态的sprite
- **Happy Sprite**: 开心状态的sprite  
- **Angry Sprite**: 生气状态的sprite

每套sprite应该对应同一种肤色的狗狗。

### 步骤 2: 设置Dog Prefab

1. 在 **Project** 窗口中找到你的 **Dog Prefab**
2. 选中Prefab，在 **Inspector** 中查看 **Dog Controller** 组件

3. 找到 **Dog Sprite Sets** 部分
   - 你会看到一个数组，默认大小为 0
   - 点击 **Size** 字段，设置为 **3**（或你想要的套数）

4. 为每一套sprite分配资源：
   - **Element 0**:
     - Normal Sprite: 拖入第一套的normal sprite
     - Happy Sprite: 拖入第一套的happy sprite
     - Angry Sprite: 拖入第一套的angry sprite
   
   - **Element 1**:
     - Normal Sprite: 拖入第二套的normal sprite
     - Happy Sprite: 拖入第二套的happy sprite
     - Angry Sprite: 拖入第二套的angry sprite
   
   - **Element 2**:
     - Normal Sprite: 拖入第三套的normal sprite
     - Happy Sprite: 拖入第三套的happy sprite
     - Angry Sprite: 拖入第三套的angry sprite

### 步骤 3: 保存Prefab

设置完成后，确保保存Prefab（Unity通常会自动保存）。

## 🎮 工作原理

- 当生成一只新狗狗时，系统会从你设置的所有sprite套中**随机选择一套**
- 这只狗狗会使用选中的那套sprite的所有状态（normal, happy, angry）
- 不同狗狗可能使用不同的肤色/外观

## 📝 示例设置

假设你有3种肤色的狗狗：

**Dog Sprite Sets (Size: 3)**
- **Element 0** (棕色狗狗):
  - Normal: `Dog_Brown_Normal`
  - Happy: `Dog_Brown_Happy`
  - Angry: `Dog_Brown_Angry`

- **Element 1** (白色狗狗):
  - Normal: `Dog_White_Normal`
  - Happy: `Dog_White_Happy`
  - Angry: `Dog_White_Angry`

- **Element 2** (黑色狗狗):
  - Normal: `Dog_Black_Normal`
  - Happy: `Dog_Black_Happy`
  - Angry: `Dog_Black_Angry`

## ⚠️ 注意事项

- 每套sprite必须包含所有三个状态（normal, happy, angry）
- 如果某套sprite缺少某个状态，游戏不会报错，但该状态不会显示
- 可以设置任意数量的sprite套（1套、3套、5套等）
- 每只狗狗生成时会随机选择，所以同一批可能看到不同肤色的狗狗

## 🐛 常见问题

**Q: 如果我只设置了一套sprite会怎样？**
A: 所有狗狗都会使用同一套sprite，功能正常，只是没有多样性。

**Q: 可以设置不同数量的sprite套吗？**
A: 可以！你可以设置1套、3套、5套或任意数量。

**Q: 如果某套sprite的某个状态（如happy）为空会怎样？**
A: 当狗狗需要切换到该状态时，sprite不会改变，但功能逻辑仍然正常。

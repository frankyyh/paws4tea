# 游戏设置完整指南

这是一个简单的敬茶游戏，玩家需要在狗狗经过屏幕中间时按空格键对狗狗鞠躬敬茶。

## 📋 前置准备

1. 确保Unity编辑器已打开项目
2. 打开场景：`Assets/Scenes/SampleScene.unity`

## 🎮 详细设置步骤

### 步骤 1: 创建测试用的Sprite（如果没有现成的）

如果你还没有sprite，可以先用Unity的默认形状：

1. 在Hierarchy中右键 → `2D Object` → `Sprite` → `Square`（创建4个：2个给狗狗，2个给玩家）
2. 或者使用Unity的默认sprite资源

### 步骤 2: 创建狗狗Prefab

1. **创建狗狗GameObject**
   - 在Hierarchy中右键 → `Create Empty`，命名为 `Dog`
   - 选中Dog，在Inspector中点击 `Add Component` → 搜索 `Sprite Renderer` 并添加
   - 再次点击 `Add Component` → 搜索 `Dog Controller` 并添加

2. **设置DogController组件**
   - 在DogController组件中：
     - `Normal Sprite`: 拖入狗狗的普通sprite（或临时使用一个Square sprite）
     - `Happy Sprite`: 拖入狗狗开心时的sprite（或临时使用另一个Square sprite，可以设置不同颜色区分）

3. **创建Prefab**
   - 在Project窗口中，创建一个文件夹 `Assets/Prefabs`（如果还没有）
   - 将Hierarchy中的 `Dog` 拖到 `Assets/Prefabs` 文件夹中
   - 现在Project中有了Dog Prefab
   - **删除Hierarchy中的Dog实例**（只保留Prefab）

### 步骤 3: 创建玩家对象

1. **创建玩家GameObject**
   - 在Hierarchy中右键 → `Create Empty`，命名为 `Player`
   - 选中Player，添加 `Sprite Renderer` 组件
   - 添加 `Player Controller` 脚本组件

2. **设置PlayerController组件**
   - `Normal Sprite`: 拖入玩家的普通sprite
   - `Bowing Sprite`: 拖入玩家鞠躬时的sprite
   - `Bow Duration`: 保持默认0.5秒（或根据需要调整）

3. **设置玩家位置**
   - 在Inspector的Transform中，设置Position：
     - X: 0
     - Y: -2（屏幕下半部分）
     - Z: 0

### 步骤 4: 创建检测区域

1. **创建DetectionZone GameObject**
   - 在Hierarchy中右键 → `Create Empty`，命名为 `DetectionZone`
   - 添加 `Detection Zone` 脚本组件

2. **设置检测区域参数**（可选调整）
   - `Zone Width`: 2（检测区域宽度）
   - `Zone Height`: 2（检测区域高度）
   - 脚本会自动将区域放置在屏幕中间（上半部分）

3. **查看检测区域**
   - 在Scene视图中，选中DetectionZone
   - 你应该能看到一个黄色的线框（Gizmos），这就是检测区域
   - 如果看不到，确保Scene视图的Gizmos选项已启用

### 步骤 5: 创建狗狗生成器

1. **创建DogSpawner GameObject**
   - 在Hierarchy中右键 → `Create Empty`，命名为 `DogSpawner`
   - 添加 `Dog Spawner` 脚本组件

2. **设置DogSpawner组件**
   - `Dog Prefab`: 将之前创建的Dog Prefab从Project窗口拖到这里
   - `Spawn Interval`: 2（每2秒生成一只狗狗）
   - `Initial Speed`: 2（初始移动速度）
   - `Speed Increase Per Dog`: 0.2（每只狗狗后速度增加0.2）
   - `Max Speed`: 10（最大速度限制）
   - `Spawn X`: -10（屏幕左侧外）
   - `Spawn Y`: 2（屏幕上半部分）

### 步骤 6: 创建游戏管理器

1. **创建GameManager GameObject**
   - 在Hierarchy中右键 → `Create Empty`，命名为 `GameManager`
   - 添加 `Game Manager` 脚本组件
   - 这个组件不需要任何额外设置，会自动工作

## 🎯 快速测试设置（使用默认形状）

如果你想快速测试游戏逻辑，可以这样做：

1. **创建简单的测试Sprite**
   - 创建4个Square sprite（Hierarchy → 右键 → 2D Object → Sprite → Square）
   - 分别命名为：DogNormal, DogHappy, PlayerNormal, PlayerBow
   - 可以通过改变Sprite Renderer的Color来区分它们

2. **快速配置**
   - Dog Prefab: 使用DogNormal和DogHappy
   - Player: 使用PlayerNormal和PlayerBow
   - 其他设置按上述步骤进行

## 🎮 运行游戏

1. 点击Unity编辑器上方的 `Play` 按钮
2. 你应该看到：
   - 狗狗从左侧出现，向右移动
   - 当狗狗到达屏幕中间时，按空格键
   - 如果时机正确，玩家会鞠躬，狗狗会变开心

## ⚙️ 调整参数

### 如果狗狗移动太快/太慢
- 调整 `DogSpawner` 的 `Initial Speed` 和 `Max Speed`

### 如果生成间隔不合适
- 调整 `DogSpawner` 的 `Spawn Interval`

### 如果检测区域位置不对
- 检查Camera设置（应该是Orthographic正交投影）
- 可以手动调整 `DetectionZone` 的Transform Position

### 如果检测区域大小不合适
- 调整 `DetectionZone` 的 `Zone Width` 和 `Zone Height`

## 🐛 常见问题

**Q: 看不到检测区域的黄色框**
- 确保在Scene视图中选中了DetectionZone
- 确保Scene视图的Gizmos已启用

**Q: 按空格没有反应**
- 确保GameManager存在于场景中
- 检查Console是否有错误信息

**Q: 狗狗没有切换sprite**
- 确保Dog Prefab的DogController中两个sprite都已分配
- 确保狗狗在检测区域内时按空格

**Q: 玩家sprite没有切换**
- 确保PlayerController中的两个sprite都已分配
- 检查Bow Duration是否设置合理

## 📝 最终Hierarchy结构

设置完成后，你的Hierarchy应该类似这样：

```
SampleScene
├── Main Camera
├── Directional Light
├── DogSpawner (DogSpawner脚本)
├── DetectionZone (DetectionZone脚本)
├── Player (PlayerController脚本 + SpriteRenderer)
└── GameManager (GameManager脚本)
```

## 🎨 下一步：美化游戏

设置完成后，你可以：
1. 替换为真正的狗狗和玩家sprite
2. 添加背景
3. 添加音效
4. 添加UI（分数、提示等）
5. 调整动画效果

祝你游戏开发顺利！🎮

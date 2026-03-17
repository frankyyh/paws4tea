# 游戏设置说明

这是一个简单的敬茶游戏，玩家需要在狗狗经过屏幕中间时按空格键对狗狗鞠躬敬茶。

## 场景设置步骤

### 1. 创建狗狗Prefab
1. 在Hierarchy中创建一个空的GameObject，命名为 "Dog"
2. 添加 `SpriteRenderer` 组件
3. 添加 `DogController` 脚本
4. 在 `DogController` 中设置：
   - Normal Sprite: 狗狗的普通sprite
   - Happy Sprite: 成功敬茶后的sprite
5. 将Dog拖到Project窗口的某个文件夹中创建Prefab
6. 删除Hierarchy中的Dog实例

### 2. 创建玩家对象
1. 在Hierarchy中创建一个空的GameObject，命名为 "Player"
2. 添加 `SpriteRenderer` 组件
3. 添加 `PlayerController` 脚本
4. 在 `PlayerController` 中设置：
   - Normal Sprite: 玩家的普通sprite
   - Bowing Sprite: 鞠躬时的sprite
   - Bow Duration: 鞠躬动画持续时间（默认0.5秒）
5. 将Player放置在屏幕下半部分（例如 Y = -2）

### 3. 创建检测区域
1. 在Hierarchy中创建一个空的GameObject，命名为 "DetectionZone"
2. 添加 `DetectionZone` 脚本
3. 脚本会自动将检测区域放置在屏幕中间
4. 可以在Inspector中调整：
   - Zone Width: 检测区域的宽度（默认2）
   - Zone Height: 检测区域的高度（默认2）

### 4. 创建狗狗生成器
1. 在Hierarchy中创建一个空的GameObject，命名为 "DogSpawner"
2. 添加 `DogSpawner` 脚本
3. 将之前创建的Dog Prefab拖到 `Dog Prefab` 字段
4. 调整参数：
   - Spawn Interval: 生成间隔（秒，默认2秒）
   - Initial Speed: 初始速度（默认2）
   - Speed Increase Per Dog: 每只狗狗后速度增加量（默认0.2）
   - Max Speed: 最大速度（默认10）
   - Spawn X: 生成X坐标（默认-10，屏幕左侧外）
   - Spawn Y: 生成Y坐标（默认2，屏幕上半部分）

### 5. 创建游戏管理器
1. 在Hierarchy中创建一个空的GameObject，命名为 "GameManager"
2. 添加 `GameManager` 脚本

## 游戏玩法

- 狗狗会从屏幕左侧出现，向右移动
- 狗狗的速度会逐渐加快
- 当狗狗移动到屏幕中间（检测区域）时，玩家按空格键
- 如果成功，玩家会切换为鞠躬sprite，狗狗会切换为开心sprite
- 玩家可以继续对后续的狗狗敬茶

## 注意事项

- 确保Camera的Projection设置为Orthographic（正交投影）
- 确保所有sprite都已导入到项目中
- 可以在Scene视图中看到黄色的检测区域框（Gizmos）

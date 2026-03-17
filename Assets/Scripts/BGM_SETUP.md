# BGM播放器设置指南

BGM播放器会在游戏开始时自动循环播放背景音乐，游戏结束时自动停止。

## 📋 设置步骤

### 步骤 1: 准备BGM音频文件

1. 将背景音乐文件（.wav, .mp3, .ogg等）导入到Unity项目中
2. 建议放在 `Assets/Audio` 文件夹中
3. 对于循环播放的BGM，建议音频文件本身也是无缝循环的

### 步骤 2: 创建BGMPlayer GameObject

1. 在 **Hierarchy** 中，右键点击空白处
2. 选择 **Create Empty**
3. 重命名为 `BGMPlayer`

### 步骤 3: 添加BGMPlayer脚本

1. 选中 **BGMPlayer** GameObject
2. 在 **Inspector** 中，点击 **Add Component**
3. 搜索并添加 `BGM Player` 脚本

### 步骤 4: 分配BGM音频文件

1. 在 **BGM Player** 组件中，找到 **BGM Settings** 部分
2. 找到 **BGM Clip** 字段
3. 将你的背景音乐文件从Project窗口拖到这个字段中

### 步骤 5: 调整AudioSource设置（可选）

脚本会自动添加AudioSource组件，如果需要调整：

1. 在 **Inspector** 中找到 **Audio Source** 组件
2. 可以调整以下设置：
   - **Volume**: 音量大小（0-1，建议0.3-0.7）
   - **Play On Awake**: 保持取消勾选（脚本会控制播放）
   - **Loop**: 自动设置为true（循环播放）
   - **Priority**: 可以设置为较低的值（如128），让音效优先

## ✅ 完成

设置完成后，BGM会在游戏开始时自动播放，游戏结束时自动停止。

## 🎮 工作原理

- **游戏开始**：BGMPlayer在Start()时自动开始播放BGM
- **循环播放**：AudioSource的Loop属性设置为true
- **游戏结束**：GameManager在EndGame()时调用BGMPlayer.StopBGM()

## ⚠️ 注意事项

- 如果BGM Clip未分配，游戏不会报错，只是不会播放音乐
- BGMPlayer使用单例模式，确保场景中只有一个BGM播放器
- 如果需要在多个场景间保持BGM，脚本已经包含了DontDestroyOnLoad
- 建议BGM音量设置得比音效低一些，避免盖过音效

## 🔧 高级设置

### 淡入淡出效果（可选）

如果需要淡入淡出效果，可以修改BGMPlayer脚本，添加协程来实现音量渐变。

### 多首BGM切换（可选）

如果需要根据游戏状态切换不同的BGM，可以扩展BGMPlayer脚本，添加多个BGM Clip和切换方法。

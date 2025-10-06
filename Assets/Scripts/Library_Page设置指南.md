# Library_Page 设置指南

## 🎯 功能概述

Library_Page 用于展示已购买的游戏，支持：
- 5列网格布局
- 每页显示10个游戏
- 超过10个游戏时显示滚动条
- 实时更新已购买游戏
- 显示游戏图标和名称

## 📁 新增脚本

### 1. `LibraryPageUI.cs` - 游戏库页面控制器
- 管理游戏库的显示和布局
- 处理滚动视图
- 实时更新游戏列表

### 2. `LibraryItemUI.cs` - 单个游戏显示组件
- 显示游戏图标和名称
- 支持悬停效果

## 🛠️ 设置步骤

### 第一步：创建LibraryItemPrefab（游戏库项目预制体）

#### 1.1 创建预制体基础结构
```
在Project窗口右键 → Create → Prefab → 命名为 "LibraryItemPrefab"
```

#### 1.2 设置预制体的GameObject
1. **选中预制体**
2. **添加组件：**
   - `Image` (作为背景)
   - `LibraryItemUI` (添加脚本)

#### 1.3 创建子UI元素
在预制体下创建以下子对象：

**GameIcon (游戏图标)**
- 创建：右键预制体 → UI → Image
- 重命名为 "GameIcon"
- 组件：`Image`
- RectTransform设置：
  - Anchor: Stretch (0, 0) to (1, 1)
  - 四个边距都设为10

**GameTitle (游戏标题)**
- 创建：右键预制体 → UI → Text
- 重命名为 "GameTitle"
- 组件：`Text`
- RectTransform设置：
  - Anchor: Bottom (0, 0) to (1, 0)
  - Position: (0, 20)
  - Size: (0, 30)
- Text设置：
  - Font Size: 14
  - Alignment: Center
  - Color: White

#### 1.4 配置LibraryItemUI脚本
选中预制体，在LibraryItemUI组件中拖入：
- `Game Icon Image`: 拖入GameIcon
- `Game Title Text`: 拖入GameTitle

### 第二步：创建Library_Page场景

#### 2.1 创建场景
```
File → New Scene → 保存为 "Library_Page"
```

#### 2.2 创建Canvas和基础结构
**Canvas**
- 创建：右键Hierarchy → UI → Canvas
- 组件：`Canvas`, `Canvas Scaler`, `Graphic Raycaster`
- Canvas Scaler设置：
  - UI Scale Mode: Scale With Screen Size
  - Reference Resolution: 1920x1080

**Background**
- 创建：右键Canvas → UI → Image
- 重命名为 "Background"
- 设置背景色

#### 2.3 创建顶部区域
**TopPanel**
- 创建：右键Canvas → UI → Panel
- 重命名为 "TopPanel"
- RectTransform设置：
  - Anchor: Top (0, 1) to (1, 1)
  - Position: (0, -50)
  - Size: (0, 100)

在TopPanel下创建：

**BackToStoreButton**
- 创建：右键TopPanel → UI → Button
- 组件：`Button`
- RectTransform设置：
  - Anchor: Left (0, 0.5)
  - Position: (100, 0)
  - Size: (150, 50)

**LibraryTitleText**
- 创建：右键TopPanel → UI → Text
- 组件：`Text`
- 文本内容：设置为空
- RectTransform设置：
  - Anchor: Center (0.5, 0.5)
  - Position: (0, 0)
  - Size: (300, 50)

**GameCountText**
- 创建：右键TopPanel → UI → Text
- 组件：`Text`
- 文本内容：设置为空
- RectTransform设置：
  - Anchor: Right (1, 0.5)
  - Position: (-100, 0)
  - Size: (200, 50)

#### 2.4 创建滚动游戏展示区域
**ScrollView**
- 创建：右键Canvas → UI → Scroll View
- 重命名为 "GamesScrollView"
- RectTransform设置：
  - Anchor: Stretch (0, 0.1) to (1, 0.9)
  - 四个边距都设为20

**GamesContainer**
- 在ScrollView的Content下，重命名为 "GamesContainer"
- 组件：`Image` (设为透明)
- RectTransform设置：
  - Anchor: Top (0, 1) to (1, 1)
  - Position: (0, 0)
  - Size: (0, 0)

### 第三步：添加管理脚本

#### 3.1 添加LibraryPageUI脚本
1. **选中Canvas**
2. **Add Component → LibraryPageUI**
3. **配置脚本引用：**
   - `Back To Store Button`: 拖入BackToStoreButton
   - `Library Title Text`: 拖入LibraryTitleText
   - `Game Count Text`: 拖入GameCountText
   - `Games Container`: 拖入GamesContainer
   - `Library Item Prefab`: 拖入LibraryItemPrefab
   - `Scroll Rect`: 拖入GamesScrollView的ScrollRect组件

#### 3.2 添加管理器对象
**GameSceneManager**
- 创建：右键Hierarchy → Create Empty
- 重命名为 "GameSceneManager"
- Add Component → GameSceneManager

**GameLibrary**
- 创建：右键Hierarchy → Create Empty
- 重命名为 "GameLibrary"
- Add Component → GameLibrary

### 第四步：配置场景设置

#### 4.1 添加到Build Settings
1. **File → Build Settings**
2. **Add Open Scenes**
3. **确保场景在列表中**

#### 4.2 更新GameSceneManager
在GameSceneManager组件中设置：
- `Library Scene Name`: "Library_Page"

## 🎮 使用方法

### 测试购买功能：
1. **运行游戏**
2. **进入商城页面**
3. **点击"购买"按钮或添加游戏到购物车**
4. **在购物车页面点击"结算"**
5. **进入游戏库页面查看已购买的游戏**

### 调试功能：
- 右键LibraryPageUI脚本 → "刷新游戏库显示"
- 右键LibraryPageUI脚本 → "打印游戏库信息"

## 📋 布局参数

### LibraryPageUI脚本设置：
```csharp
[Header("布局设置")]
public int itemsPerRow = 5;         // 每行显示的游戏数量
public int itemsPerPage = 10;       // 每页显示的游戏数量
public float itemSpacing = 10f;     // 项目间隔
public Vector2 itemSize = new Vector2(150, 200); // 项目大小
```

### GridLayoutGroup自动设置：
- 5列网格布局
- 项目大小：150x200
- 间隔：10像素
- 自动换行

## ✅ 功能特性

1. **实时更新** - 购买游戏后立即在游戏库中显示
2. **网格布局** - 5列整齐排列
3. **滚动支持** - 超过10个游戏时显示滚动条
4. **响应式设计** - 适配不同屏幕尺寸
5. **简洁显示** - 只显示游戏图标和名称

## 🚀 扩展功能

### 可以添加的功能：
- 游戏分类筛选
- 搜索功能
- 游戏详情页面
- 收藏功能
- 游戏启动功能

这个Library_Page现在可以完美展示已购买的游戏，支持实时更新和滚动浏览！



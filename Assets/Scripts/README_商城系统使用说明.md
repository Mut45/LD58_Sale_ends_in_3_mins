# Steam风格商城UI系统使用说明

## 概述
这是一个类似Steam商城的Unity UI系统，包含游戏卡片展示、悬停详情、购买功能、购物车和游戏库管理。

## 文件结构
- `GameData.cs` - 游戏数据结构
- `GameCard.cs` - 单个游戏卡片逻辑
- `StoreUI.cs` - 商城主界面控制器
- `GameLibrary.cs` - 游戏库管理（单例）
- `ShoppingCart.cs` - 购物车管理（单例）
- `GameDataManager.cs` - 游戏数据管理器（单例）

## 主要功能

### 1. 游戏卡片悬停效果
- 鼠标悬停在游戏卡片上时显示详细信息面板
- 包含：标题、评分、原价、折扣、最终价格、类型
- 包含：购买按钮和添加到购物车按钮
- 鼠标移开时面板消失

### 2. 购买功能
- 点击"Purchase"按钮直接购买游戏
- 购买后卡片变暗，按钮禁用
- 游戏自动添加到游戏库

### 3. 购物车功能
- 点击"Add to Cart"按钮添加到购物车
- 购物车有最大容量限制（默认10个）
- 右上角购物车按钮显示商品数量

### 4. 导航功能
- 购物车按钮：跳转到购物车界面
- 游戏库按钮：跳转到游戏库界面

## Unity设置步骤

### 1. 创建UI层级结构
```
Canvas
├── StoreUI (StoreUI脚本)
│   ├── GameCard1 (GameCard脚本)
│   │   ├── GameIcon (Image)
│   │   ├── CollisionBox2D (Collider2D + GameCard脚本)
│   │   └── HoverPanel (GameObject)
│   │       ├── TitleText (Text)
│   │       ├── RatingText (Text)
│   │       ├── PriceText (Text)
│   │       ├── DiscountText (Text)
│   │       ├── FinalPriceText (Text)
│   │       ├── TypeText (Text)
│   │       ├── PurchaseButton (Button)
│   │       └── AddToCartButton (Button)
│   ├── GameCard2 (同上结构)
│   ├── GameCard3 (同上结构)
│   ├── CartButton (Button + 购物车图标)
│   └── LibraryButton (Button + 游戏库图标)
```

### 2. 组件配置
- 为每个GameCard添加Collider2D组件
- 将GameCard脚本添加到每个游戏卡片上
- 将StoreUI脚本添加到主UI对象上
- 创建GameLibrary、ShoppingCart、GameDataManager的空GameObject并添加对应脚本

### 3. 脚本引用设置
在StoreUI脚本中：
- 将三个GameCard拖拽到gameCards列表
- 设置cartButton和libraryButton引用
- 设置cartCountText引用（可选）

在GameCard脚本中：
- 设置所有UI组件的引用
- 配置normalColor和purchasedColor

## 与合作同学代码集成

### 方法1：使用GameDataManager
```csharp
// 合作同学的代码可以这样添加游戏数据
GameDataManager.Instance.AddGameData(new GameData(
    id: 1,
    title: "游戏名称",
    gameIcon: sprite,
    rating: 4.5f,
    originalPrice: 29.99f,
    discount: 20f,
    type: "动作"
));

// 然后更新UI
StoreUI storeUI = FindObjectOfType<StoreUI>();
storeUI.SetAllGameData(GameDataManager.Instance.GetAllGamesData());
```

### 方法2：直接调用StoreUI
```csharp
// 直接更新特定卡片
StoreUI storeUI = FindObjectOfType<StoreUI>();
storeUI.UpdateGameData(0, new GameData(...)); // 更新第一个卡片
```

## 扩展功能

### 添加更多游戏卡片
1. 在Scene中复制GameCard对象
2. 在StoreUI的gameCards列表中添加新的GameCard引用
3. 更新SetupSampleGames方法添加更多示例数据

### 自定义UI样式
- 修改GameCard脚本中的颜色设置
- 调整HoverPanel的显示效果
- 自定义按钮样式和文本字体

### 添加音效
在按钮点击事件中添加音效播放代码：
```csharp
// 在OnPurchaseClicked和OnAddToCartClicked方法中添加
AudioSource.PlayClipAtPoint(purchaseSound, Camera.main.transform.position);
```

## 注意事项
1. 确保所有单例脚本（GameLibrary、ShoppingCart、GameDataManager）在场景中存在
2. UI组件的引用必须正确设置
3. Collider2D组件用于检测鼠标悬停事件
4. 购买状态会在场景切换后保持（因为使用了DontDestroyOnLoad）




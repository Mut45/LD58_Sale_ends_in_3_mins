# Unity商城UI详细设置步骤

## 1. 创建Canvas和基本结构

### 第一步：创建Canvas
1. 右键 Hierarchy → UI → Canvas
2. 这创建了一个Canvas GameObject，包含：
   - **Transform 组件**：位置、旋转、缩放
   - **Canvas 组件**：UI渲染设置
   - **Canvas Scaler 组件**：屏幕适配
   - **Graphic Raycaster 组件**：UI交互检测

### 第二步：创建StoreUI容器
1. 右键 Canvas → UI → Panel
2. 重命名为 "StoreUI"
3. 这个Panel GameObject包含：
   - **Transform 组件**
   - **RectTransform 组件**（UI专用Transform）
   - **Canvas Renderer 组件**（渲染）
   - **Image 组件**（背景图片，可以设为透明）

### 第三步：添加StoreUI脚本
1. 选中StoreUI GameObject
2. 在Inspector中点击"Add Component"
3. 搜索并添加"StoreUI"脚本
4. 现在StoreUI GameObject包含：
   - **Transform 组件**
   - **RectTransform 组件**
   - **Canvas Renderer 组件**
   - **Image 组件**
   - **StoreUI 脚本组件** ⭐

## 2. 创建游戏卡片结构

### GameCard GameObject设置
对于每个游戏卡片，你需要创建：

```
GameCard (GameObject)
├── Transform 组件
├── RectTransform 组件
├── Canvas Renderer 组件
├── Image 组件 (显示游戏图标)
├── Collider2D 组件 (检测鼠标悬停)
└── GameCard 脚本组件 ⭐
```

**具体创建步骤：**
1. 右键 StoreUI → UI → Image
2. 重命名为 "GameCard1"
3. 添加组件：
   - **BoxCollider2D** (Add Component → Physics2D → BoxCollider2D)
   - **GameCard** 脚本 (Add Component → Scripts → GameCard)

### HoverPanel GameObject设置
```
HoverPanel (GameObject) - 作为GameCard的子对象
├── Transform 组件
├── RectTransform 组件
├── Canvas Renderer 组件
├── Image 组件 (半透明背景)
└── 子对象们...
    ├── TitleText (Text 组件)
    ├── RatingText (Text 组件)
    ├── PriceText (Text 组件)
    ├── DiscountText (Text 组件)
    ├── FinalPriceText (Text 组件)
    ├── TypeText (Text 组件)
    ├── PurchaseButton (Button 组件)
    └── AddToCartButton (Button 组件)
```

**具体创建步骤：**
1. 右键 GameCard1 → UI → Image
2. 重命名为 "HoverPanel"
3. 设置Image颜色为半透明黑色 (0,0,0,150)
4. 默认隐藏：取消勾选Inspector中的复选框
5. 创建子对象：
   - 右键 HoverPanel → UI → Text → 重命名为 "TitleText"
   - 右键 HoverPanel → UI → Text → 重命名为 "RatingText"
   - 右键 HoverPanel → UI → Button → 重命名为 "PurchaseButton"
   - 右键 HoverPanel → UI → Button → 重命名为 "AddToCartButton"

## 3. 创建导航按钮

### CartButton GameObject
```
CartButton (GameObject)
├── Transform 组件
├── RectTransform 组件
├── Canvas Renderer 组件
├── Image 组件 (按钮背景)
├── Button 组件 ⭐
└── CartCountText (子对象)
    ├── Transform 组件
    ├── RectTransform 组件
    ├── Canvas Renderer 组件
    └── Text 组件 ⭐
```

**创建步骤：**
1. 右键 StoreUI → UI → Button
2. 重命名为 "CartButton"
3. 添加子对象：右键 CartButton → UI → Text
4. 重命名为 "CartCountText"
5. 调整位置到按钮右上角

### LibraryButton GameObject
```
LibraryButton (GameObject)
├── Transform 组件
├── RectTransform 组件
├── Canvas Renderer 组件
├── Image 组件
└── Button 组件 ⭐
```

## 4. 脚本引用配置

### StoreUI脚本配置
选中StoreUI GameObject，在Inspector中找到StoreUI脚本：

**Game Cards 部分：**
- Game Cards (Size: 3)
  - Element 0: 拖拽 GameCard1 GameObject
  - Element 1: 拖拽 GameCard2 GameObject
  - Element 2: 拖拽 GameCard3 GameObject

**Navigation Buttons 部分：**
- Cart Button: 拖拽 CartButton GameObject
- Library Button: 拖拽 LibraryButton GameObject

**Cart Button UI 部分：**
- Cart Count Text: 拖拽 CartCountText GameObject

### GameCard脚本配置
选中每个GameCard GameObject，配置GameCard脚本：

**UI References 部分：**
- Game Icon Image: 拖拽该GameCard的Image组件
- Hover Panel: 拖拽该GameCard下的HoverPanel GameObject
- Title Text: 拖拽 HoverPanel下的TitleText GameObject
- Rating Text: 拖拽 HoverPanel下的RatingText GameObject
- Price Text: 拖拽 HoverPanel下的PriceText GameObject
- Discount Text: 拖拽 HoverPanel下的DiscountText GameObject
- Final Price Text: 拖拽 HoverPanel下的FinalPriceText GameObject
- Type Text: 拖拽 HoverPanel下的TypeText GameObject
- Purchase Button: 拖拽 HoverPanel下的PurchaseButton GameObject
- Add To Cart Button: 拖拽 HoverPanel下的AddToCartButton GameObject

**Visual Effects 部分：**
- Card Background: 拖拽该GameCard的Image组件
- Normal Color: 设置为白色 (1,1,1,1)
- Purchased Color: 设置为灰色 (0.5,0.5,0.5,1)

## 5. 创建管理器GameObjects

### GameLibrary管理器
1. 右键 Hierarchy → Create Empty
2. 重命名为 "GameLibrary"
3. 添加组件：GameLibrary 脚本
4. 这个GameObject包含：
   - **Transform 组件**
   - **GameLibrary 脚本组件** ⭐

### ShoppingCart管理器
1. 右键 Hierarchy → Create Empty
2. 重命名为 "ShoppingCart"
3. 添加组件：ShoppingCart 脚本
4. 这个GameObject包含：
   - **Transform 组件**
   - **ShoppingCart 脚本组件** ⭐

### GameDataManager管理器
1. 右键 Hierarchy → Create Empty
2. 重命名为 "GameDataManager"
3. 添加组件：GameDataManager 脚本
4. 这个GameObject包含：
   - **Transform 组件**
   - **GameDataManager 脚本组件** ⭐

## 6. 最终层级结构

```
Canvas
├── StoreUI (Panel + StoreUI脚本)
│   ├── GameCard1 (Image + BoxCollider2D + GameCard脚本)
│   │   └── HoverPanel (Image)
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
│   ├── CartButton (Button)
│   │   └── CartCountText (Text)
│   └── LibraryButton (Button)
├── GameLibrary (Empty GameObject + GameLibrary脚本)
├── ShoppingCart (Empty GameObject + ShoppingCart脚本)
└── GameDataManager (Empty GameObject + GameDataManager脚本)
```

## 重要提醒

1. **StoreUI不是Empty GameObject**，它是一个UI Panel，包含必要的UI组件
2. **管理器是Empty GameObject**，只包含脚本组件，用于逻辑控制
3. **每个UI元素都需要正确的组件组合**才能正常工作
4. **脚本引用必须正确配置**，否则功能无法正常使用




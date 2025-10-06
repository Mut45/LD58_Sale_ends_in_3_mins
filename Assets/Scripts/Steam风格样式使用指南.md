# Steam风格样式使用指南

## 🎯 概述

本指南将详细说明如何在Unity UI Toolkit中使用已集成的Steam风格样式表。

## 📁 文件结构

```
Assets/Format/
├── StyleSheet.uss          # Steam风格样式表
├── UIDocument.uxml         # UI布局文件
└── Theme.tss              # 主题文件
```

## 🚀 使用方法

### 1. 在UXML文件中使用样式类

#### 1.1 基本按钮样式
```xml
<!-- Steam蓝色按钮 -->
<Button text="购买游戏" class="steam-button" />

<!-- Steam绿色按钮 -->
<Button text="添加到购物车" class="steam-button steam-button-green" />

<!-- Steam红色按钮 -->
<Button text="删除" class="steam-button steam-button-red" />
```

#### 1.2 容器和面板样式
```xml
<!-- Steam风格面板 -->
<VisualElement class="steam-panel">
    <Label text="游戏信息" class="steam-panel-header" />
    <Label text="游戏描述内容..." class="steam-text-primary" />
</VisualElement>

<!-- Steam风格卡片 -->
<VisualElement class="steam-card">
    <Label text="游戏标题" class="steam-label" />
    <Label text="游戏价格: ¥99" class="steam-label-secondary" />
</VisualElement>

<!-- 深色容器 -->
<VisualElement class="dark-container">
    <Label text="深色背景内容" class="steam-text-primary" />
</VisualElement>

<!-- 浅色容器 -->
<VisualElement class="light-container">
    <Label text="浅色背景内容" class="steam-text-primary" />
</VisualElement>
```

#### 1.3 文本样式
```xml
<!-- 主要文本 -->
<Label text="游戏标题" class="steam-label" />

<!-- 次要文本 -->
<Label text="游戏描述" class="steam-text-secondary" />

<!-- 静音文本 -->
<Label text="发布时间" class="steam-text-muted" />
```

#### 1.4 输入框样式
```xml
<!-- Steam风格输入框 -->
<TextField placeholder="搜索游戏..." class="steam-input" />
<TextField placeholder="用户名" class="steam-input" />
```

#### 1.5 进度条样式
```xml
<!-- Steam风格进度条 -->
<ProgressBar value="0.5" class="steam-progress-bar" />
```

### 2. 完整的游戏卡片示例

```xml
<?xml version="1.0" encoding="utf-8"?>
<engine:UXML xmlns:engine="UnityEngine.UIElements">
    
    <!-- 游戏卡片容器 -->
    <VisualElement class="steam-card">
        
        <!-- 游戏图标 -->
        <VisualElement name="GameIcon" style="width: 200px; height: 150px; background-color: var(--gpSystemDarkGrey);" />
        
        <!-- 游戏信息区域 -->
        <VisualElement name="GameInfo" style="flex-direction: column; padding: 10px;">
            <Label text="游戏标题" class="steam-label" />
            <Label text="游戏开发商" class="steam-text-secondary" />
            <Label text="游戏类型: RPG" class="steam-text-muted" />
        </VisualElement>
        
        <!-- 价格和按钮区域 -->
        <VisualElement name="PriceSection" style="flex-direction: row; justify-content: space-between; align-items: center; padding: 10px;">
            <VisualElement style="flex-direction: column;">
                <Label text="¥99" class="steam-text-primary" />
                <Label text="-20%" class="steam-text-secondary" />
            </VisualElement>
            <Button text="购买" class="steam-button" />
        </VisualElement>
        
    </VisualElement>
    
</engine:UXML>
```

### 3. 导航栏示例

```xml
<!-- Steam风格导航栏 -->
<VisualElement class="steam-nav" style="height: 60px; flex-direction: row; align-items: center; padding: 0 20px;">
    
    <!-- Logo区域 -->
    <VisualElement style="width: 40px; height: 40px; background-color: var(--gpColor-Blue);" />
    
    <!-- 导航按钮 -->
    <Button text="商店" class="steam-button" style="margin-left: 20px;" />
    <Button text="库" class="steam-button" />
    <Button text="社区" class="steam-button" />
    
    <!-- 右侧区域 -->
    <VisualElement style="flex-grow: 1;" />
    
    <!-- 搜索框 -->
    <TextField placeholder="搜索..." class="steam-input" style="width: 300px;" />
    
    <!-- 用户头像 -->
    <VisualElement style="width: 30px; height: 30px; background-color: var(--gpSystemGrey); border-radius: 15px; margin-left: 10px;" />
    
</VisualElement>
```

### 4. 购物车页面示例

```xml
<!-- 购物车页面布局 -->
<VisualElement class="steam-bg-store" style="flex-grow: 1;">
    
    <!-- 顶部导航 -->
    <VisualElement style="height: 60px; flex-direction: row; align-items: center; padding: 0 20px; background-color: var(--gpStoreDarkerGrey);">
        <Button text="← 返回商店" class="steam-button" />
        <Label text="购物车" class="steam-label" style="margin-left: 20px; font-size: 20px;" />
        <VisualElement style="flex-grow: 1;" />
        <Label text="总计: ¥299" class="steam-text-primary" />
    </VisualElement>
    
    <!-- 购物车内容区域 -->
    <ScrollView class="scrollView" style="flex-grow: 1; padding: 20px;">
        
        <!-- 购物车项目 -->
        <VisualElement class="steam-card" style="margin-bottom: 10px;">
            <VisualElement style="flex-direction: row; align-items: center;">
                <!-- 游戏图标 -->
                <VisualElement style="width: 80px; height: 60px; background-color: var(--gpSystemDarkGrey); margin-right: 15px;" />
                
                <!-- 游戏信息 -->
                <VisualElement style="flex-grow: 1;">
                    <Label text="游戏标题" class="steam-label" />
                    <Label text="游戏类型" class="steam-text-secondary" />
                    <Label text="¥99" class="steam-text-primary" />
                </VisualElement>
                
                <!-- 操作按钮 -->
                <Button text="移除" class="steam-button steam-button-red" />
            </VisualElement>
        </VisualElement>
        
        <!-- 更多购物车项目... -->
        
    </ScrollView>
    
    <!-- 底部结算区域 -->
    <VisualElement style="height: 80px; background-color: var(--gpStoreDarkerGrey); flex-direction: row; align-items: center; justify-content: space-between; padding: 0 20px;">
        <Label text="总价: ¥299" class="steam-text-primary" style="font-size: 18px;" />
        <Button text="结算" class="steam-button steam-button-green" style="width: 120px; height: 40px;" />
    </VisualElement>
    
</VisualElement>
```

## 🎨 样式类参考

### 容器类
- `.dark-container` - 深色容器背景
- `.light-container` - 浅色容器背景（带悬停效果）
- `.mid-container` - 中等色调容器
- `.steam-panel` - Steam风格面板
- `.steam-card` - Steam风格卡片（带悬停效果）

### 按钮类
- `.steam-button` - 标准Steam蓝色按钮
- `.steam-button-green` - 绿色按钮
- `.steam-button-red` - 红色按钮

### 文本类
- `.steam-text-primary` - 主要文本（亮白色）
- `.steam-text-secondary` - 次要文本（浅灰色）
- `.steam-text-muted` - 静音文本（深灰色）
- `.steam-label` - 标准标签文本
- `.steam-label-secondary` - 次要标签文本

### 输入类
- `.steam-input` - Steam风格输入框

### 背景类
- `.steam-bg-store` - Steam商店背景色
- `.steam-bg-library` - Steam库背景色
- `.steam-bg-gradient` - Steam渐变背景

### 导航类
- `.steam-nav` - Steam风格导航栏

### 进度条类
- `.steam-progress-bar` - Steam风格进度条

## 🔧 在脚本中应用样式

### C#脚本中的样式应用
```csharp
using UnityEngine;
using UnityEngine.UIElements;

public class SteamStyleExample : MonoBehaviour
{
    public UIDocument uiDocument;
    
    void Start()
    {
        var root = uiDocument.rootVisualElement;
        
        // 创建Steam风格按钮
        var button = new Button("购买游戏");
        button.AddToClassList("steam-button");
        root.Add(button);
        
        // 创建Steam风格卡片
        var card = new VisualElement();
        card.AddToClassList("steam-card");
        
        var title = new Label("游戏标题");
        title.AddToClassList("steam-label");
        card.Add(title);
        
        var description = new Label("游戏描述");
        description.AddToClassList("steam-text-secondary");
        card.Add(description);
        
        root.Add(card);
        
        // 创建Steam风格输入框
        var input = new TextField("搜索游戏...");
        input.AddToClassList("steam-input");
        root.Add(input);
    }
}
```

## 🎯 最佳实践

### 1. 样式组合
```xml
<!-- 可以组合多个样式类 -->
<Button text="操作" class="steam-button steam-button-green" />
```

### 2. 自定义样式覆盖
```xml
<!-- 使用内联样式覆盖默认样式 -->
<Button text="自定义按钮" class="steam-button" style="width: 200px; height: 50px;" />
```

### 3. 响应式设计
```xml
<!-- 使用Flexbox进行响应式布局 -->
<VisualElement style="flex-direction: row; flex-wrap: wrap;">
    <VisualElement class="steam-card" style="width: 300px; margin: 10px;" />
    <VisualElement class="steam-card" style="width: 300px; margin: 10px;" />
</VisualElement>
```

## 🔍 调试技巧

### 1. 检查样式应用
- 在Unity编辑器中选中UI元素
- 查看Inspector面板中的StyleSheet引用
- 确认样式类已正确添加到元素

### 2. 样式优先级
- 内联样式 > 样式类 > 默认样式
- 使用`!important`可以强制应用样式

### 3. 常见问题
- 确保StyleSheet.uss文件已正确引用到UIDocument
- 检查样式类名拼写是否正确
- 验证CSS变量是否正确定义

## 📝 注意事项

1. **字体设置**: 确保在StyleSheet中正确引用字体资源
2. **颜色变量**: 所有颜色都使用CSS变量，便于主题切换
3. **悬停效果**: 某些样式包含悬停效果，需要适当的鼠标事件支持
4. **性能优化**: 避免过度嵌套样式类，保持结构简洁

通过这个指南，您可以轻松地在Unity项目中创建具有Steam风格的用户界面！


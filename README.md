# UnityCommonSolution - UnityEditorExtensions

欢迎来到 **Unity 通用解决方案 - Unity 编辑器扩展** 仓库！该项目旨在提供一组实用的 Unity 编辑器扩展和工具，以优化您的 Unity
开发工作流程。无论您是在处理 UI 组件、自定义检查器还是编辑器窗口，本仓库都提供了一系列可重用的脚本和工具，以增强您的 Unity
编辑器体验。

## 目录

- [功能](#功能)
- [安装](#安装)
- [使用方法](#使用方法)
- [文件结构](#文件结构)

---

## 功能

本仓库包含以下主要功能模块：

1. **组件扩展**
    - `AnimFoldGroup`：动画折叠组，用于在编辑器中动态展开和折叠内容。
    - `MenuOptionGroup`：菜单选项组，支持水平和垂直布局的菜单选项管理。

2. **编辑器绘制工具**
    - `EditorDrawer`：提供自定义的编辑器绘制方法，简化 UI 元素的创建。
    - `EditorGUIIcons` 和 `EditorGUIStyles`：内置图标和样式的快速访问工具。

3. **编辑器窗口扩展**
    - `EditorWindowCatcher`：用于捕获和管理自定义编辑器窗口的工具。

4. **扩展方法**
    - `EditorExtensions` 和 `RectExtensions`：提供对 Unity 编辑器功能的扩展方法，例如 Rect 操作和编辑器工具链的简化。

5. **资源管理**
    - 内置图标资源（如 GitHub、Gitee 等图标），方便在项目中使用。

---

## 安装

1. **克隆仓库**
   将本仓库克隆到您的 Unity 项目的 `Assets` 目录下：

   ```bash
   git clone https://github.com/YourUsername/UnityCommonSolution-UnityEditorExtensions.git Assets/UnityCommonSolution-UnityEditorExtensions
   ```

2. **导入 Unity 项目**
   打开 Unity 编辑器，确保所有文件已正确导入。

3. **使用 Assembly Definition**
   本仓库已包含 `Pixelsmao.UnityCommonSolution.UnityEditorExtensions.asmdef` 文件，确保您的项目支持 Assembly Definition
   功能。

---

## 使用方法

### 1. 使用 `MenuOptionGroup`

`MenuOptionGroup` 提供了水平和垂直布局的菜单选项管理功能。您可以通过以下方式使用：

```csharp
var menuOptions = new List<MenuOption>
{
    new MenuOption("Option 1", () => Debug.Log("Option 1 Selected")),
    new MenuOption("Option 2", () => Debug.Log("Option 2 Selected"))
};

var horizontalMenu = new HorizontalMenuOptionGroup(menuOptions);
horizontalMenu.Draw();
```

### 2. 使用 `EditorGUIIcons`

`EditorGUIIcons` 提供了内置图标的快速访问功能。例如，加载 GitHub 图标：

```csharp
Texture2D githubIcon = EditorGUIIcons.GetGitHubIcon();
GUI.DrawTexture(new Rect(0, 0, 64, 64), githubIcon);
```

### 3. 使用 `RectExtensions`

`RectExtensions` 提供了对 `Rect` 的扩展方法，例如调整大小或位置：

```csharp
Rect rect = new Rect(0, 0, 100, 100);
rect = rect.Expand(10); // 扩展 Rect 的大小
```

---

## 文件结构

以下是仓库的文件结构说明：

```
├── Editor/                         # 编辑器相关脚本
│   ├── Components/                 # 组件扩展
│   │   ├── AnimFoldGroup.cs        # 动画折叠组
│   │   └── MenuOptionGroup/        # 菜单选项组
│   ├── EditorDrawer.cs             # 编辑器绘制工具
│   ├── EditorGUI/                  # 编辑器 GUI 工具
│   │   ├── EditorGUIIcons.cs       # 内置图标工具
│   │   └── EditorGUIStyles.cs      # 内置样式工具
│   ├── EditorWindow/               # 编辑器窗口扩展
│   │   └── EditorWindowCatcher.cs  # 编辑器窗口捕获工具
│   ├── Extensions/                 # 扩展方法
│   │   ├── EditorExtensions.cs     # 编辑器扩展方法
│   │   └── RectExtensions.cs       # Rect 扩展方法
│   └── Resources/                  # 资源文件
│       └── Icons/                  # 内置图标
├── LICENSE                         # 许可证文件
├── README.md                       # 项目说明文档
└── package.json                    # 包配置文件
```

---

## 许可证

本项目基于 [MIT 许可证](LICENSE) 开源，您可以自由使用、修改和分发代码。

---

## 贡献

如果您有任何建议或发现问题，欢迎提交 Issue 或 Pull Request！您的贡献将帮助我们不断改进项目。

---

Happy Coding! 🚀
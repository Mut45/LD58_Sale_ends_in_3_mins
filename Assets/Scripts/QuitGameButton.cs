using UnityEngine;
using UnityEngine.UI;

public class QuitGameButton : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button quitButton;

    private void Start()
    {
        // 如果没有手动指定按钮，尝试获取当前GameObject上的Button组件
        if (quitButton == null)
        {
            quitButton = GetComponent<Button>();
        }

        // 如果找到了按钮，添加点击事件
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
        else
        {
            Debug.LogError("QuitGameButton: No Button component found! Please assign a button or add Button component to this GameObject.");
        }
    }

    private void OnDestroy()
    {
        // 清理事件监听器
        if (quitButton != null)
        {
            quitButton.onClick.RemoveListener(QuitGame);
        }
    }

    public void QuitGame()
    {
        Debug.Log("退出游戏按钮被点击");
        
        // 在编辑器中停止播放
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        
        // 在构建版本中退出应用程序
        Application.Quit();
    }

    // 公共方法：手动设置按钮引用
    public void SetQuitButton(Button button)
    {
        // 移除旧按钮的监听器
        if (quitButton != null)
        {
            quitButton.onClick.RemoveListener(QuitGame);
        }

        // 设置新按钮
        quitButton = button;

        // 添加新按钮的监听器
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
    }
}

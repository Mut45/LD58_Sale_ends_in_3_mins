using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [Header("Scene Transition Settings")]
    [SerializeField] private Button transitionButton;
    [SerializeField] private string targetSceneName;
    
    private void Start()
    {
        // 如果没有手动分配按钮，尝试从当前GameObject获取
        if (transitionButton == null)
        {
            transitionButton = GetComponent<Button>();
        }
        
        // 绑定按钮点击事件
        if (transitionButton != null)
        {
            transitionButton.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("SceneTransition: 没有找到按钮组件！请确保GameObject上有Button组件或手动分配按钮。");
        }
    }
    
    private void OnButtonClick()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("SceneTransition: 目标场景名称为空！请在Inspector中设置targetSceneName。");
        }
    }
    
    private void LoadScene(string sceneName)
    {
        Debug.Log($"SceneTransition: 正在加载场景 {sceneName}");
        
        // 检查场景是否存在
        if (SceneExists(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"SceneTransition: 场景 {sceneName} 不存在于Build Settings中！请检查Build Settings。");
        }
    }
    
    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameFromPath = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneNameFromPath == sceneName)
            {
                return true;
            }
        }
        return false;
    }
    
    private void OnDestroy()
    {
        // 清理事件监听
        if (transitionButton != null)
        {
            transitionButton.onClick.RemoveListener(OnButtonClick);
        }
    }
}

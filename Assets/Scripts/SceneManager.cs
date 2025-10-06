using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }
    
    [Header("Scene Names")]
    [SerializeField] private string storeSceneName = "Shop_Page";
    [SerializeField] private string cartSceneName = "Cart_Page";
    [SerializeField] private string librarySceneName = "Library_Page";
    [SerializeField] private string checkoutSceneName = "CheckoutScene";
    [SerializeField] private string conclusion1 = "conclusion1";
    [SerializeField] private string conclusion2 = "conclusion2";
    [SerializeField] private string Ending01 = "Ending01";
    [SerializeField] private string Ending02 = "Ending02";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // 跳转到商城场景
    public void LoadStoreScene()
    {
        Debug.Log($"LoadStoreScene called, 目标场景: {storeSceneName}");
        LoadScene(storeSceneName);
    }
    
    // 跳转到购物车场景
    public void LoadCartScene()
    {
        LoadScene(cartSceneName);
    }
    
    // 跳转到游戏库场景
    public void LoadLibraryScene()
    {
        LoadScene(librarySceneName);
    }
    
    // 跳转到支付场景
    public void LoadCheckoutScene()
    {
        LoadScene(checkoutSceneName);
    }

    public void LoadConclusion1()
    {
        LoadScene(conclusion1);
    }

    public void LoadConclusion2()
    {
        LoadScene(conclusion2);
    }

    public void Ending1()
    {
        LoadScene(Ending01);
    }

    public void Ending2()
    {
        LoadScene(Ending02);
    }

    public void OnQuit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // 通用场景加载方法
    private void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            Debug.Log($"Loading scene: {sceneName}");
            
            // 检查场景是否存在
            if (SceneExists(sceneName))
            {
                Debug.Log($"场景 {sceneName} 存在，正在加载...");
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError($"场景 {sceneName} 不存在于Build Settings中！请检查Build Settings。");
            }
        }
        else
        {
            Debug.LogError($"Scene name is empty or null: {sceneName}");
        }
    }
    
    // 获取当前场景名称
    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    
    // 检查场景是否存在
    public bool SceneExists(string sceneName)
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
}

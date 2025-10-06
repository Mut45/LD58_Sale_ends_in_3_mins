using UnityEngine;
using UnityEngine.UI;

public class PanelRaycastDisable : MonoBehaviour
{
    [Header("射线检测设置")]
    [SerializeField] private bool disableRaycastTarget = true;
    
    private void Start()
    {
        // 禁用Panel的射线检测
        if (disableRaycastTarget)
        {
            DisableRaycastTarget();
        }
    }
    
    private void DisableRaycastTarget()
    {
        // 获取Image组件并禁用Raycast Target
        var image = GetComponent<Image>();
        if (image != null)
        {
            image.raycastTarget = false;
            Debug.Log($"已禁用 {gameObject.name} 的射线检测");
        }
        
        // 如果Panel有其他UI组件，也禁用它们的射线检测
        var canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false;
        }
    }
    
    // 公共方法：手动禁用射线检测
    public void DisableRaycast()
    {
        DisableRaycastTarget();
    }
    
    // 公共方法：手动启用射线检测
    public void EnableRaycast()
    {
        var image = GetComponent<Image>();
        if (image != null)
        {
            image.raycastTarget = true;
        }
        
        var canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true;
        }
    }
}





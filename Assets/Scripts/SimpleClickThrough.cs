using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleClickThrough : MonoBehaviour, IPointerClickHandler
{
    [Header("穿透设置")]
    [SerializeField] private bool enableClickThrough = true;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!enableClickThrough) return;
        
        // 禁用当前GameObject的GraphicRaycaster
        var raycaster = GetComponent<GraphicRaycaster>();
        if (raycaster != null)
        {
            raycaster.enabled = false;
        }
        
        // 重新执行射线检测
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        
        // 重新启用GraphicRaycaster
        if (raycaster != null)
        {
            raycaster.enabled = true;
        }
        
        // 找到第一个不是当前对象的UI元素
        foreach (var result in results)
        {
            if (result.gameObject != gameObject)
            {
                // 执行点击事件
                ExecuteEvents.Execute(result.gameObject, eventData, ExecuteEvents.pointerClickHandler);
                Debug.Log($"点击穿透到: {result.gameObject.name}");
                break;
            }
        }
    }
    
    // 公共方法：启用/禁用穿透
    public void SetClickThrough(bool enabled)
    {
        enableClickThrough = enabled;
    }
}

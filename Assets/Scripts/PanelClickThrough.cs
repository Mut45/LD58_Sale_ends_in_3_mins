using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PanelClickThrough : MonoBehaviour, IPointerClickHandler
{
    [Header("穿透设置")]
    [SerializeField] private bool allowClickThrough = true;
    [SerializeField] private bool allowDragThrough = true;
    
    private GraphicRaycaster graphicRaycaster;
    private EventSystem eventSystem;
    
    private void Start()
    {
        // 获取组件引用
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        eventSystem = EventSystem.current;
        
        // 如果没有GraphicRaycaster，添加一个
        if (graphicRaycaster == null)
        {
            graphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!allowClickThrough) return;
        
        // 获取所有被点击的UI元素
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        
        // 过滤掉当前Panel
        var filteredResults = new System.Collections.Generic.List<RaycastResult>();
        foreach (var result in results)
        {
            if (result.gameObject != gameObject)
            {
                filteredResults.Add(result);
            }
        }
        
        // 如果有其他UI元素被点击，将事件传递给它们
        if (filteredResults.Count > 0)
        {
            // 找到最前面的UI元素
            var targetResult = filteredResults[0];
            
            // 创建新的事件数据
            var newEventData = new PointerEventData(eventSystem)
            {
                position = eventData.position,
                delta = eventData.delta,
                scrollDelta = eventData.scrollDelta,
                pressPosition = eventData.pressPosition,
                clickTime = eventData.clickTime,
                clickCount = eventData.clickCount,
                button = eventData.button,
                eligibleForClick = eventData.eligibleForClick,
                dragging = eventData.dragging,
                useDragThreshold = eventData.useDragThreshold,
                pointerId = eventData.pointerId
            };
            
            // 执行点击事件
            ExecuteEvents.Execute(targetResult.gameObject, newEventData, ExecuteEvents.pointerClickHandler);
            
            Debug.Log($"点击穿透到: {targetResult.gameObject.name}");
        }
    }
    
    // 公共方法：设置穿透模式
    public void SetClickThrough(bool enabled)
    {
        allowClickThrough = enabled;
    }
    
    // 公共方法：设置拖拽穿透模式
    public void SetDragThrough(bool enabled)
    {
        allowDragThrough = enabled;
    }
}

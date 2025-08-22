using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonView : View
{
    [Header("Button Setup")]
    public RectTransform fullScreenRectCanvas;
    public UnityEvent onClick; 
    
    private RectTransform rectTransform;
    private Vector2 dragOffset;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError(gameObject.name + " has no rect");
            return;
        }
        Init();
    }

    public override bool IsLocalPointInView(Vector2 mousePositionLocal)
    {
        if (rectTransform == null) return false;
        
        Vector2 localPointToThis = rectTransform.InverseTransformPoint(fullScreenRectCanvas.parent.TransformPoint(mousePositionLocal));
        
        Vector2 center = rectTransform.rect.center;
        float distance = Vector2.Distance(localPointToThis, center);
        float radius = Mathf.Min(width, height) / 2f;
        
        return distance <= radius;
    }

    public override void HandleDragCont(Vector2 mousePositionLocal)
    {
        return;
    }

    public override void HandleDragStart(Vector2 mousePositionLocal)
    {
        return;
    }

    public override void HandleDragEnd(Vector2 mousePositionLocal)
    {
        return;
    }

    public override void HandleClick()
    {
        onClick.Invoke();
    }

    public override void Highlight()
    {
        return;
    }

    public override void Delight()
    {
        return;
    }

    private void Init()
    {
        isDraggable = false;
        Resize(width, height);
    }

    public void Resize(float newWidth, float newHeight)
    {
        width = newWidth;
        height = newHeight;
        rectTransform.sizeDelta = new Vector2(width, height);
    }
}

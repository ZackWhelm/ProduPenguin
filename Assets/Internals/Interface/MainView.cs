using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : View
{
    [Header("Setup")]
    public Color highlightColor = Color.yellow;
    public Color delightColor = Color.red;
    public Image image;
    public Vector2 InitRelPosToBottomRight = Vector2.zero;

    private RectTransform rectTransform;
    private Vector2 dragOffset;

    
    void Start()
    {
        image = GetComponent<Image>();
        if (image == null)
        {
            Debug.LogError(gameObject.name + " has no image");
            return;
        }

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
        
        Vector3 worldPoint = rectTransform.parent.TransformPoint(mousePositionLocal);
        Vector2 localPointToThis = rectTransform.InverseTransformPoint(worldPoint);
        return rectTransform.rect.Contains(localPointToThis);
    }

    public override void HandleDragCont(Vector2 mousePositionLocal)
    {
        if (rectTransform == null) return;
        
        rectTransform.anchoredPosition = mousePositionLocal - dragOffset;
        isDragging = true;
    }

    public override void HandleDragStart(Vector2 mousePositionLocal)
    {
        if (isDraggable)
        {
            dragOffset = mousePositionLocal - rectTransform.anchoredPosition;
            isDragging = true;
        }
    }

    public override void HandleDragEnd(Vector2 mousePositionLocal)
    {
        rectTransform.anchoredPosition = mousePositionLocal - dragOffset;
        isDragging = false;
    }

    public override void Highlight()
    {
        image.color = highlightColor;
    }

    public override void Delight()
    {
        image.color = delightColor;
    }
    
    private void Init() {
        MoveToPos(InitRelPosToBottomRight);
        Resize(width, height);
    }

    public void Resize(float newWidth, float newHeight)
    {
        width = newWidth;
        height = newHeight;
        rectTransform.sizeDelta = new Vector2(width, height);
    }
    
    public void MoveToPos(Vector2 newPos)
    {
        if (rectTransform.parent == null) return;
        
        RectTransform parentRect = rectTransform.parent as RectTransform;
        if (parentRect == null) return;
        
        Vector2 parentSize = parentRect.rect.size;
        Vector2 bottomRightCorner = new Vector2(parentSize.x / 2f, -parentSize.y / 2f);
        Vector2 offsetFromBottomRight = new Vector2(-newPos.x, -newPos.y);
        Vector2 finalPosition = bottomRightCorner + offsetFromBottomRight;
        
        rectTransform.anchoredPosition = finalPosition;
    }
}

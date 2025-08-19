using System;
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
    private const string POSITION_SAVE_KEY = "MainView_Position";

    
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
        
        Vector2 newPosition = mousePositionLocal - dragOffset;
        newPosition = ViewSnappingHelper.Instance.SnapToScreen(newPosition, new Vector2(width, height), rectTransform.parent as RectTransform);
        
        rectTransform.anchoredPosition = newPosition;
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
        Vector2 newPosition = mousePositionLocal - dragOffset;
        newPosition = ViewSnappingHelper.Instance.SnapToScreen(newPosition,  new Vector2(width, height), rectTransform.parent as RectTransform);
        
        rectTransform.anchoredPosition = newPosition;
        isDragging = false;
        SavePosition();
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
        LoadPosition();
        Resize(width, height);
    }

    private void LoadPosition()
    {
        if (PlayerPrefs.HasKey(POSITION_SAVE_KEY))
        {
            string savedPosition = PlayerPrefs.GetString(POSITION_SAVE_KEY);
            Vector2 position = JsonUtility.FromJson<Vector2>(savedPosition);
            rectTransform.anchoredPosition = position;
        }
        else
        {
            MoveToPos(InitRelPosToBottomRight);
        }
    }

    private void SavePosition()
    {
        if (rectTransform != null)
        {
            string positionJson = JsonUtility.ToJson(rectTransform.anchoredPosition);
            PlayerPrefs.SetString(POSITION_SAVE_KEY, positionJson);
            PlayerPrefs.Save();
        }
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
        
        finalPosition = ViewSnappingHelper.Instance.SnapToScreen(finalPosition,  new Vector2(width, height), parentRect);
        rectTransform.anchoredPosition = finalPosition;
    }
}

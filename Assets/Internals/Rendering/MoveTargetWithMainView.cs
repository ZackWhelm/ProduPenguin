using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetWithMainView : MonoBehaviour
{
    [Header("Main View Reference")]
    public RectTransform mainView;
    public RectTransform canvasRect;
    
    [Header("X Position Mapping")]
    public float minX = -10f;
    public float maxX = 10f;
    public bool useXAxis = true;
    public bool useYAxis = false;
    
    [Header("Smoothing")]
    public float smoothSpeed = 5f;
    
    private Vector3 targetPosition;
    private Vector3 originalPosition;
    
    void Start()
    {
        originalPosition = transform.position;
        targetPosition = originalPosition;
    }
    
    void Update()
    {
        if (mainView == null) return;
        
        UpdateXPosition();
        SmoothMoveToTarget();
    }
    
    private void UpdateXPosition()
    {
        Vector2 viewportPoint = GetMainViewViewportPosition();
        
        float xValue = 0f;
        
        if (useXAxis && useYAxis)
        {
            xValue = (viewportPoint.x + viewportPoint.y) * 0.5f;
        }
        else if (useXAxis)
        {
            xValue = viewportPoint.x;
        }
        else if (useYAxis)
        {
            xValue = viewportPoint.y;
        }
        
        float mappedX = Mathf.Lerp(minX, maxX, xValue);
        targetPosition = new Vector3(mappedX, originalPosition.y, originalPosition.z);
    }
    
    private Vector2 GetMainViewViewportPosition()
    {
        if (canvasRect == null) return Vector2.zero;
        
        Vector2 canvasSize = canvasRect.rect.size;
        Vector2 mainViewLocalPos = mainView.localPosition;
        
        float normalizedX = (mainViewLocalPos.x + canvasSize.x * 0.5f) / canvasSize.x;
        float normalizedY = (mainViewLocalPos.y + canvasSize.y * 0.5f) / canvasSize.y;
        
        return new Vector2(normalizedX, normalizedY);
    }
    
    private void SmoothMoveToTarget()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
    
    public void SetMainView(RectTransform newMainView)
    {
        mainView = newMainView;
        if (canvasRect == null)
        {
            canvasRect = mainView.GetComponentInParent<RectTransform>();
        }
    }
    
    public void SetXRange(float newMinX, float newMaxX)
    {
        minX = newMinX;
        maxX = newMaxX;
    }
}

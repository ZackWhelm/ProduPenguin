using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class View : MonoBehaviour
{
    [Header("Size Settings")]
    public float width = 400f;
    public float height = 400f;
    
    [Header("Dragging Settings")]
    public bool isDraggable = true;

    [Header("State")]
    public bool isDragging = true;
    
    public abstract bool IsLocalPointInView(Vector2 mousePositionLocal);

    public abstract void HandleDragCont(Vector2 mousePositionLocal);
    public abstract void HandleDragStart(Vector2 mousePositionLocal);
    public abstract void HandleDragEnd(Vector2 mousePositionLocal);

    // DEV MODE STUFFS
    public abstract void Highlight();
    public abstract void Delight();
}

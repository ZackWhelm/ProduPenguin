using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_STANDALONE_WIN
using System.Runtime.InteropServices;
#endif

public class Interactor : MonoBehaviour
{
    [Header("Dependencies")]
    public TransparentWindowController transparentWindowController;

    
    #if UNITY_STANDALONE_WIN
    [DllImport("user32.dll")] private static extern short GetAsyncKeyState(int vKey);

    #endif
    
    public static Interactor Instance { get; private set; }
    
    private Canvas canvas;

    [Header("State")]
    public bool isDraggingAView = false;
    public View viewBeingDragged = null;

    void Awake()
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
    
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Can vas isn't setup");
        }
        Application.focusChanged += OnApplicationFocusChanged;
    }
    
    void Update()
    {
        HandleMouseInput();
    }
    

    
    private void OnApplicationFocusChanged(bool hasFocus)
    {
        if (hasFocus)
        {
            isDraggingAView = false;
            viewBeingDragged = null;
        }
    }
    
    private bool wasMouseDown = false;
    private View viewUnderMouseOnPress = null;
    
    private void HandleMouseInput()
    {
        bool isMouseDown = GetMouseButtonState();
        View viewUnderMouse = GetViewUnderMouse();

        if (!wasMouseDown && isMouseDown)
        {
            viewUnderMouseOnPress = viewUnderMouse;
            
            if (viewUnderMouse != null && viewUnderMouse.isDraggable)
            {
                StartDragging(viewUnderMouse);
            }
        }
        
        if (wasMouseDown && !isMouseDown)
        {
            if (viewUnderMouseOnPress != null && viewUnderMouseOnPress.isClickable && 
                viewUnderMouse == viewUnderMouseOnPress)
            {
                HandleClick(viewUnderMouseOnPress);
            }
            
            if (isDraggingAView) {
                StopDragging();
            }
            
            viewUnderMouseOnPress = null;
        }
        
        if (isDraggingAView && viewBeingDragged != null)
        {
            viewBeingDragged.HandleDragCont(GetMousePositionLocalInCanvas());
        }
        
        wasMouseDown = isMouseDown;
    }
    
    private bool GetMouseButtonState()
    {
        #if UNITY_STANDALONE_WIN
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            short state = GetAsyncKeyState(0x01);
            return (state & 0x8000) != 0;
        }
        #endif
        return Input.GetMouseButton(0);
    }
    
    private View GetViewUnderMouse()
    {
        if (canvas == null) return null;
        
        View[] allViews = FindObjectsOfType<View>();
        foreach (View view in allViews)
        {
            if (view.IsLocalPointInView(GetMousePositionLocalInCanvas()))
            {
                if (DevSettings.Instance.HighlightViews) {
                    view.Highlight();
                }
                return view;
            }
            else {
               if (DevSettings.Instance.HighlightViews) {
                    view.Delight();            
                }
            }
        }
        
        return null;
    }
    
    private void StartDragging(View view)
    {
        isDraggingAView = true;
        viewBeingDragged = view;
        viewBeingDragged.HandleDragStart(GetMousePositionLocalInCanvas());
    }

    private void HandleClick(View view){
        view.HandleClick();
    }
    
    private void StopDragging()
    {
        isDraggingAView = false;
        if (viewBeingDragged != null)
        {
            viewBeingDragged.HandleDragEnd(GetMousePositionLocalInCanvas());
            viewBeingDragged = null;
        }
    }
    
    public bool IsMouseOverAView()
    {
        return GetViewUnderMouse() != null;
    }
    
    public bool IsDraggingAView
    {
        get { return isDraggingAView; }
        set { isDraggingAView = value; }
    }
    
    public View GetCurrentDraggedView()
    {
        return viewBeingDragged;
    }

    private Vector2 GetMousePositionLocalInCanvas() {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            mousePosition,
            canvas.worldCamera,
            out localPoint
        );
        return localPoint;
    }
}
using UnityEngine;
using Cinemachine;

public class ActivityCameraController : MonoBehaviour
{
    [Header("Virtual Cameras")]
    public CinemachineVirtualCamera idleCamera;
    public CinemachineVirtualCamera miningCamera;
    
    [Header("Camera Priorities")]
    public int inactivePriority = 0;
    public int activePriority = 10;
    
    private CinemachineVirtualCamera currentActiveCamera;
    
    void Start()
    {
        SetIdleCamera();
    }
    
    public void SetIdleCamera()
    {
        SetCameraActive(idleCamera);
    }
    
    public void SetMiningCamera()
    {
        SetCameraActive(miningCamera);
    }
    
    private void SetCameraActive(CinemachineVirtualCamera targetCamera)
    {
        if (targetCamera == null) return;
        
        SetAllCamerasInactive();
        
        targetCamera.Priority = activePriority;
        currentActiveCamera = targetCamera;
    }
    
    private void SetAllCamerasInactive()
    {
        if (idleCamera != null)
            idleCamera.Priority = inactivePriority;
            
        if (miningCamera != null)
            miningCamera.Priority = inactivePriority;
    }
    
    public CinemachineVirtualCamera GetCurrentActiveCamera()
    {
        return currentActiveCamera;
    }
}

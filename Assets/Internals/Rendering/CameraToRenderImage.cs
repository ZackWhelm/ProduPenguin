using UnityEngine;
using UnityEngine.UI;

public class CameraToRenderImage : MonoBehaviour
{
    public Camera renderCamera;
    public Image targetImage;
    public Vector2Int textureSize = new Vector2Int(960, 960);
    
    private RenderTexture renderTexture;
    private Texture2D texture2D;
    
    void Start()
    {
        renderTexture = new RenderTexture(textureSize.x, textureSize.y, 24);
        renderTexture.Create();
        
        texture2D = new Texture2D(textureSize.x, textureSize.y);
        
        renderCamera.targetTexture = renderTexture;
        
        Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, textureSize.x, textureSize.y), Vector2.one * 0.5f);
        targetImage.sprite = sprite;
    }
    
    void Update()
    {
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, textureSize.x, textureSize.y), 0, 0);
        texture2D.Apply();
        RenderTexture.active = null;
    }
    
    void OnDestroy()
    {
        if (renderTexture != null)
        {
            renderTexture.Release();
        }
        if (texture2D != null)
        {
            DestroyImmediate(texture2D);
        }
    }
}

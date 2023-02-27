
using System;
using UnityEngine;
using UnityEngine.Rendering;
[ExecuteAlways]
public class FrustumCamera : MonoBehaviour
{
    private const string BENDING_FEATURE = "ENABLE_BENDING";
    

    private void Awake()
    {
        if (Application.isPlaying)
        {
            Shader.EnableKeyword(BENDING_FEATURE);
            
        }
        else
        {
            Shader.DisableKeyword(BENDING_FEATURE);
        }
    }
    
    private void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
        
    }

    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }

    private static void OnBeginCameraRendering(ScriptableRenderContext context, Camera cam)
    {
        float w = Screen.width /2;
        float h = Screen.height /2;
        cam.cullingMatrix = Matrix4x4.Ortho(-w, w, -h, h, 0.001f, 500) * cam.worldToCameraMatrix;
    }

    private static void OnEndCameraRendering(ScriptableRenderContext context, Camera cam)
    {
        cam.ResetCullingMatrix();
    }

  
}

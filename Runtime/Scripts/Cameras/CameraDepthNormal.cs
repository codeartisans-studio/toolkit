using UnityEngine;

[ExecuteInEditMode]
public class CameraDepthNormal : MonoBehaviour
{
    public DepthTextureMode depthMode = DepthTextureMode.DepthNormals;

    private void OnEnable()
    {
        GetComponent<Camera>().depthTextureMode = depthMode;
    }

    private void OnDisable()
    {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.None;
    }

}
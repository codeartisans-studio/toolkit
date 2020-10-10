using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class DeferredDecal : MonoBehaviour
{
    private MeshFilter meshFilter = null;
    private MeshRenderer meshRenderer = null;

    private CommandBuffer commandBuffer = null;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        commandBuffer = new CommandBuffer();
        commandBuffer.SetRenderTarget(BuiltinRenderTextureType.GBuffer0, BuiltinRenderTextureType.CameraTarget);
        commandBuffer.DrawMesh(meshFilter.sharedMesh, transform.localToWorldMatrix, meshRenderer.sharedMaterial);
        Camera.main.AddCommandBuffer(CameraEvent.BeforeLighting, commandBuffer);
    }

    private void OnDisable()
    {
        Camera.main.RemoveCommandBuffer(CameraEvent.BeforeLighting, commandBuffer);
    }
}
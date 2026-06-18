using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay: MonoBehaviour
{
    Renderer textureRenderer;
    MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    [HideInInspector]
    public GameObject meshObject;

    public void DrawTexture(Texture2D texture)
    {
        textureRenderer = meshObject.GetComponent<Renderer>();
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData meshData, Texture2D texture){
       

        meshFilter = meshObject.GetComponent<MeshFilter>();
        meshRenderer = meshObject.GetComponent<MeshRenderer>();
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}

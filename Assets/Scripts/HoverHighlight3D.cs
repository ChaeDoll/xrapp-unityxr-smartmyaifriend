using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverHighlight3D : MonoBehaviour
{
    public Renderer targetRenderer;
    public Material element0;
    public Material element1;

    void Start()
    {
        Material[] materials = targetRenderer.materials;
        materials[0] = element0;
        if (materials.Length > 1)
        {
            System.Array.Resize(ref materials, 1); // materials �迭 ũ�⸦ 1�� ����
        }
        targetRenderer.materials = materials;
    }

    public void ShowElement0Only()
    {
        Material[] materials = targetRenderer.materials;
        materials[0] = element0;
        if (materials.Length > 1)
        {
            System.Array.Resize(ref materials, 1); // materials �迭 ũ�⸦ 1�� ����
        }
        targetRenderer.materials = materials;
    }

    public void ShowBothElements()
    {
        Material[] materials = new Material[2];
        materials[0] = element0;
        materials[1] = element1;
        targetRenderer.materials = materials;
    }
}

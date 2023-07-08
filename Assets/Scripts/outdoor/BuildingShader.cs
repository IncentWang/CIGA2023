using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingShader : MonoBehaviour
{

    Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // 获取材质
        Material material = renderer.material;
        Debug.Log(material.name);
        // 设置Culling属性为Front
        material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Front);
    }

}

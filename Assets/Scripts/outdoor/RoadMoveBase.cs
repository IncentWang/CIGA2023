using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMoveBase : MonoBehaviour
{
    private float speed;
    private WallMove wallMove;

    Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        wallMove = FindObjectOfType<WallMove>();
        speed = 20f;

    }

    // Update is called once per frame
    void Update()
    {
        // 获取材质
        Material material = renderer.material;
        // 获取Offset参数的值
        Vector2 offset = material.GetTextureOffset("_MainTex");
        // 计算新的Offset值
        float offsetX = offset.x + speed;
        float offsetY = offset.y + speed/100000f ;
        Vector2 newOffset = new Vector2(offsetX, offsetY);

        // 设置新的Offset值
        material.SetTextureOffset("_MainTex", newOffset);
    }

    public void SetSpeed(float sp)
    {
        speed = sp;
    }
}

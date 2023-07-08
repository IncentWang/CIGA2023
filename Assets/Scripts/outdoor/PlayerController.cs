using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private WallMove wallMove;
    // Start is called before the first frame update
    void Start()
    {
        wallMove = FindObjectOfType<WallMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

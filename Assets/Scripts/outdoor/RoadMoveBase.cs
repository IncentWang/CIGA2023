using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMoveBase : MonoBehaviour
{
    private float speed;
    private float positionZ;

    public Transform endPoint;

    public Transform createPoint;

    private WallMove wallMove;

    private bool isBuilt;
    // Start is called before the first frame update
    void Start()
    {
        wallMove = FindObjectOfType<WallMove>();
        speed = 20f;
        createPoint = GameObject.FindGameObjectWithTag("RoadCreatePoint").transform;
    }
    private void OnEnable()
    {
        isBuilt = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime), Space.World);
        //产生新的road
        if(endPoint.position.z > -30 && endPoint.position.z <= 120  && !isBuilt)
        {
            RoadMoveBase roadMoveBase = wallMove.queueRoadMoveBase.Dequeue();
            wallMove.queueRoadMoveBase.Enqueue(this);
            roadMoveBase.gameObject.SetActive(true);
            roadMoveBase.transform.position = createPoint.position;
            isBuilt = true; 
        }
        //自我销毁
        if(endPoint.position.z <= -30)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetSpeed(float sp)
    {
        speed = sp;
    }
}

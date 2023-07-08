using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public Queue<RoadMoveBase> queueRoadMoveBase;

    public RoadMoveBase roadMoveBase1;
    public RoadMoveBase roadMoveBase2;
    public RoadMoveBase roadMoveBase3;
    // Start is called before the first frame update
    void Start()
    {
        queueRoadMoveBase = FindObjectOfType<WallMove>().queueRoadMoveBase;
        queueRoadMoveBase.Enqueue(roadMoveBase1);
        queueRoadMoveBase.Enqueue(roadMoveBase2);
        queueRoadMoveBase.Enqueue(roadMoveBase3);
        queueRoadMoveBase.Dequeue();
        roadMoveBase2.gameObject.SetActive(false);
        roadMoveBase3.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }
}

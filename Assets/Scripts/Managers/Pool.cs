using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public Transform Container
    {
        get; private set;

    }

    public Queue<GameObject> Objects; // используем очередь, а не список, потому что когда мы берем объект из очереди, то он автоматически удаляется

    public Pool (Transform container)
    {
        Container = container;
        Objects = new Queue<GameObject>();
    }
}

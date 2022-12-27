using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // цель, за которой следует камера
    public float smoothing; // параметр для того, чтобы камера не следовала резко, а с некоторой задержкой

    Vector3 offset;         // вектор между камерой и целью

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;

        //есть текущая позиция и та, где я хочу быть и как быстро к ней я двигаюсь (smoothing помноженная на дельтаТайм (50 раз в секунду)), вот что делает эта ф-ция
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

}

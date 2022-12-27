using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed;

    [SerializeField]
    JoystickController joysrick;

    Vector3 movement;
    Animator anim;
    Rigidbody rb;
    int floorMask;
    float camRayLength = 100f;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        speed = GetComponent<UnitParameters>().Speed;
    }

    void FixedUpdate()
    {
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");

        JoystickMovement();
        Animating2();
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);

        //нормализуем вектор, чтобы у нас не было преимущества по диагонали (т.к два вектора одновремеено = 1.4) и умножаем на скорость и дельтатайм, чтобы он двигался одинаково независимо от фпс
        movement = movement.normalized * speed * Time.deltaTime;

        //изменяем позиции игрока - та позиция. на которой он в данный момент плюс вектор, который мы придали
        rb.MovePosition(transform.position + movement);
    }

    void JoystickMovement()
    {
        //movement.Set(h, 0f, v);
        if (joysrick.speed > 0f)
        {
            movement = new Vector3(joysrick.direction.x, 0f, joysrick.direction.y);
            //нормализуем вектор, чтобы у нас не было преимущества по диагонали (т.к два вектора одновремеено = 1.4) и умножаем на скорость и дельтатайм, чтобы он двигался одинаково независимо от фпс
            movement = movement.normalized * speed * Time.deltaTime;

            //изменяем позиции игрока - та позиция. на которой он в данный момент плюс вектор, который мы придали
            rb.MovePosition(transform.position + movement);

            Quaternion newRotation = Quaternion.LookRotation(new Vector3(joysrick.direction.x, 0f, joysrick.direction.y));
            rb.MoveRotation(newRotation);
        }           
    }

    void Turning()
    {
        // создаем луч из камеры, туда куда хотим повернуться. куда тыкнул игрок - появляется невидимый луч.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) // если мы тыкнули что-то, тыкнули в точку на полу
        {
            //то создаем вектор от игрока в точку, куда тыкнули мышью
            Vector3 playerToMouse = floorHit.point - transform.position;

            //хотим, чтобы игрок повернулся, но чтобы не откидывался назад
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool running = h != 0f || v != 0f;
        anim.SetBool("IsRunning", running);
    }

    void Animating2()
    {
        float h = joysrick.direction.x;
        float v = joysrick.direction.y;
        bool running = h != 0f || v != 0f;
        anim.SetBool("IsRunning", running);
    }
}



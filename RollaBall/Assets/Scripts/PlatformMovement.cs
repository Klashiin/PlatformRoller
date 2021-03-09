using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Vector3 distance;
    public float speed;
    public bool isTrigger;

    private Vector3 starting, current, final;
    private float wait;
    private bool isMoving;
    private bool moveOnTrigger;
    private GameObject player;
    private bool stopMoving = false;

    void Awake()
    {
        starting = GetComponent<Transform>().position;
        final = starting + distance;
        if (distance.x != 0) { speed /= distance.x; distance.x = 1; }
        if (distance.y != 0) { speed /= distance.y; distance.y = 1; }
        if (distance.z != 0) { speed /= distance.z; distance.z = 1; }
    }

    void FixedUpdate()
    {
        if (!stopMoving)
        {
            if (isTrigger == false)
            {
                isMoving = true;
                Move(distance);
                Loop();
            }
            else
            {
                if (moveOnTrigger && Time.time >= wait + 0.5)
                {
                    isMoving = true;
                    Move(distance);
                    Loop();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isTrigger && !moveOnTrigger)
        {
            wait = Time.time;
            moveOnTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && isTrigger && moveOnTrigger)
        {
            stopMoving = false;
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player" && isMoving)
        {
            player = other.gameObject;
            player.transform.Translate(distance / speed * Time.deltaTime, Space.World);
            player.GetComponent<Rigidbody>().angularDrag += 0.4f;
        }
        else if (other.gameObject.tag == "Player" && !isMoving)
        {
            player = other.gameObject;
            player.GetComponent<Rigidbody>().angularDrag -= 0.4f;
        }
    }

    void Move(Vector3 direction)
    {
        transform.Translate(direction / speed * Time.deltaTime, Space.World);
        current = GetComponent<Transform>().position;
    }

    void Loop()
    {
        if (isTrigger == false)
        {
            if (current.x >= final.x) { ReverseCheck(); }
            if (current.y >= final.y) { ReverseCheck(); }
            if (current.z >= final.z) { ReverseCheck(); }

            if (current.x <= starting.x) { ReverseCheck(); }
            if (current.y <= starting.y) { ReverseCheck(); }
            if (current.z <= starting.z) { ReverseCheck(); }
        }
        else
        {
            if (current.x < final.x) { StopCheck(); }
            if (current.y < final.y) { StopCheck(); }
            if (current.z < final.z) { StopCheck(); }

            // if (current.x <= starting.x) { ReverseCheck(); }
            // if (current.y <= starting.y) { ReverseCheck(); }
            // if (current.z <= starting.z) { ReverseCheck(); }
        }
    }

    void ReverseCheck()
    {
        speed = -speed;
    }

    void StopCheck()
    {
        stopMoving = true;
        isMoving = false;
    }
}

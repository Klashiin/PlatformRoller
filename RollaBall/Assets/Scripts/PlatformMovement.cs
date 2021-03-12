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
    private bool moveOnTrigger = false;
    private GameObject player;
    private bool stopMoving = false;
    private bool playerColl;

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
        if (!stopMoving && !isTrigger)
        {
            isMoving = true;
            Move(distance);
            Loop();
        }
        else
        {
            if (!stopMoving && moveOnTrigger && Time.time >= wait + 0.5f)
            {
                isMoving = true;
                Move(distance);
                Loop();
            }
            else if (!playerColl && stopMoving)
            {
                stopMoving = false;
                Reverse();
                Move(distance);
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

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerColl = true;
            if (isMoving)
            {
                player = other.gameObject;
                player.transform.Translate(distance / speed * Time.deltaTime, Space.World);
                player.GetComponent<Rigidbody>().angularDrag += 0.4f;
            }
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerColl = false;
            if (!isMoving)
            {
                player = other.gameObject;
                player.GetComponent<Rigidbody>().angularDrag -= 0.4f;
            }
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
            if (final.x + final.y + final.z > starting.x + starting.y + starting.z)
            {
                if (current.x + current.y + current.z > final.x + final.y + final.z) { Reverse(); }
                if (current.x + current.y + current.z < starting.x + starting.y + starting.z) { Reverse(); }
            }
            else
            {
                if (current.x + current.y + current.z < final.x + final.y + final.z) { Reverse(); }
                if (current.x + current.y + current.z > starting.x + starting.y + starting.z) { Reverse(); }
            }
        }
        else
        {
            if (final.x + final.y + final.z > starting.x + starting.y + starting.z)
            {
                if (current.x + current.y + current.z >= final.x + final.y + final.z) { Stop(); }
                if (current.x + current.y + current.z <= starting.x + starting.y + starting.z) { Stop(); moveOnTrigger = false; }
            }
            else
            {
                if (current.x + current.y + current.z <= final.x + final.y + final.z) { Stop(); }
                if (current.x + current.y + current.z >= starting.x + starting.y + starting.z) { Stop(); moveOnTrigger = false; }
            }
        }
    }

    void Reverse()
    {
        speed = -speed;
    }

    void Stop()
    {
        stopMoving = true;
        isMoving = false;
    }
}

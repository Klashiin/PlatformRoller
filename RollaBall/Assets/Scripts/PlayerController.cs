using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public TextMeshProUGUI score;
    public GameObject stageClearObj;

    private Rigidbody rb;
    private float X;
    private float Y;
    private int pickupCount;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pickupCount = 0;
        score.SetText($"Score: {pickupCount}");
        stageClearObj.SetActive(false);
    }

    private void FixedUpdate()
    {
        Vector3 v3 = new Vector3(X, 0.0f, Y);
        rb.AddForce(v3 * speed);
    }

    private void OnMove(InputValue mv)
    {
        Vector2 v2 = mv.Get<Vector2>();
        X = v2.x;
        Y = v2.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);
            pickupCount++;
            score.SetText($"Score: {pickupCount}");
            if(pickupCount >= 4)
            {
                stageClearObj.SetActive(true);
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public TextMeshProUGUI score;
    public GameObject stageClearObj;
    public GameObject ramp;
    public GameObject infotext;
    public GameObject finalcube;

    private Rigidbody rb;
    private float X;
    private float Y;
    private bool isMoving = true;
    private int pickupCount = 0;
    private int rampGoal = 100;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pickupCount = 0;
        score.SetText($"Score: {pickupCount}");
        stageClearObj.SetActive(false);
        ramp.SetActive(false);
        infotext.GetComponent<TextMesh>().text = $"COLLECT {rampGoal}";
        infotext.SetActive(true);
    }

    private void FixedUpdate()
    {
        Vector3 v3 = new Vector3(X, 0.0f, Y);
        rb.AddForce(v3 * speed);
    }

    private void OnMove(InputValue mv)
    {
        if (isMoving)
        {
            Vector2 v2 = mv.Get<Vector2>();
            X = v2.x;
            Y = v2.y;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);
            pickupCount++;
            score.SetText($"Score: {pickupCount}");
            infotext.GetComponent<TextMesh>().text = $"COLLECT {rampGoal - pickupCount}";
            if (pickupCount >= rampGoal)
            {
                infotext.SetActive(false);
                ramp.SetActive(true);
            }
        }
        else if (other.gameObject.tag == "Bounds")
        {
            SceneManager.LoadScene(0);
        }
        else if (other.gameObject.tag == "FinalCube")
        {
            StageClear();
        }
    }
    void StageClear()
    {
        stageClearObj.SetActive(true);
        isMoving = false;
        speed = 2;
    }
}
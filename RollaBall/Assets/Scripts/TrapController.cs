using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrapController : MonoBehaviour
{
    public GameObject disable;
    public GameObject message;
    public GameObject trap;

    void Start()
    {
        message.SetActive(false);
        disable.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            message.SetActive(true);
            disable.SetActive(false);
            trap.SetActive(false);
        }
    }
}

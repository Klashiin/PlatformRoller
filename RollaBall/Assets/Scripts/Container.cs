using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public GameObject[] Objects;

    void Awake()
    {
        foreach (GameObject obj in Objects)
        {
            if (obj.tag == "Platform")
            {
                obj.GetComponent<PlatformMovement>().enabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject obj in Objects)
            {
                if (obj.tag == "Platform")
                {
                    obj.GetComponent<PlatformMovement>().enabled = true;
                }
            }
        }
    }
}

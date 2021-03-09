using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveTrigger : MonoBehaviour
{
    public GameObject obj;

    private bool disable = false;

    void Awake()
    {
        obj.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && disable == false)
        {
            obj.SetActive(true);
        }
        disable = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRotate : MonoBehaviour
{
    private Vector3 angle = new Vector3(15, 40, 40);

    void FixedUpdate()
    {
        transform.Rotate(angle * Time.deltaTime, Space.World);
    }
}

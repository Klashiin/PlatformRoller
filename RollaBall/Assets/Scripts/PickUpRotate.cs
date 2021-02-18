using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRotate : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(20, 40, 45) * Time.deltaTime);
    }
}

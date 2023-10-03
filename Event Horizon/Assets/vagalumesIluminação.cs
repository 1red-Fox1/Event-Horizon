using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vagalumesIluminação : MonoBehaviour
{
    public Transform targetObject;

    void LateUpdate()
    {
        transform.position = targetObject.position;
    }
}

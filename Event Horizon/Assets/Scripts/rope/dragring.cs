using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragring : MonoBehaviour
{
    private float mZCoord;
    private Vector3 mOffset;

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        Vector3 newPosition = GetMouseWorldPos() + mOffset;
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}

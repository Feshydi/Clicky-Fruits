using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    public GameObject trail;

    private bool isMouseDown;

    private void Update()
    {
        trail.transform.position = GetCursorPosition();
    }

    private Vector3 GetCursorPosition()
    {
        Vector3 trailPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        trailPos.z = 1;
        return trailPos;
    }

    public void OnMouseDown()
    {
        trail.SetActive(true);
        isMouseDown = true;
    }

    public void OnMouseUp()
    {
        trail.SetActive(false);
        isMouseDown = false;
    }

    public bool IsMouseDown()
    {
        return isMouseDown;
    }

}

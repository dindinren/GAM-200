using System;
using UnityEngine;

public class PackageManager : MonoBehaviour
{
    bool isDragging = false;
    Vector3 offset;
    float zCoord;

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = zCoord; // keep original distance
            transform.position = Camera.main.ScreenToWorldPoint(mousePoint) + offset;
        }
    }

    private void OnMouseDown()
    {
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z; // save depth
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;

        offset =  transform.position - Camera.main.ScreenToWorldPoint(mousePoint);
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    //if package hits the floor, change layer to default
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.gameObject.layer = LayerMask.NameToLayer("Default");
        }

        //if (collision.gameObject.CompareTag("Paddle"))
        //{
        //    this.GetComponent<Rigidbody2D>().simulated = false;
        //}
    }
}

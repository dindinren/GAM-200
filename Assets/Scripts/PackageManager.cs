using System;
using UnityEngine;

public class PackageManager : MonoBehaviour
{
    bool isDragging = false;
    Vector3 offset;
    float zCoord;

    int packageHP = 2;
    int packageValue = 10; //prob need to change when using json

    SpawnManager spawnManager;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spawnManager = GameObject.Find("Canvas").GetComponent<SpawnManager>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get this package's renderer
    }
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

    void PackageHPColor()
    {
        var spriteRenderer = spawnManager.GetPackageClone().GetComponent<SpriteRenderer>();

        switch (packageHP)
        {
            default:
                //this.gameObject.GetComponent<SpriteRenderer>().material.color = new Color();
                break;
            case 1:
                spriteRenderer.color = new Color(255f/255f, 169f/255f, 0f/255f); //why need to divide why unity i dun understand 
                packageValue = packageValue /= 2;

                break;
            case 0:
                spriteRenderer.color = Color.red;
                packageValue = packageValue / 2;
                break;
        }
    }
    public int GetPackageValue()
    {
        return packageValue;
    }

    int GetPackageHP()
    {
        return packageHP;
    }

    //if package hits the floor, change layer to default
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            --packageHP;

            this.gameObject.layer = LayerMask.NameToLayer("Default");

            ///change the package color 
            PackageHPColor();

            Debug.Log($"PACKAGE TOO DAMAGE! CURRENTLY AT {packageHP}");
            
        }

        //if (collision.gameObject.CompareTag("Paddle"))
        //{
        //    this.GetComponent<Rigidbody2D>().simulated = false;
        //}
    }
}

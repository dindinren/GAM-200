using System;
using UnityEngine;

public class PackageManager : MonoBehaviour
{
    [Header("PACKAGE DETAILS")]
    public int packageID;
    public string receipientName;
    public string location;
    public string description;
    public int weight;
    public int packageHP = 2; //how many times a package can handle damage
    public int packageValue; //Money value of package
    public GameObject packagePrefab; //Image of package




    [Header("Dont edit this")]
    public bool isSelected; // Add this field
    bool package_floor_hit;

    bool isDragging = false;
    Vector3 offset;
    float zCoord;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get this package's renderer
    }

    private void Start()
    {
        //weight = gameObject.GetComponent<Rigidbody2D>().mass;
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
        package_floor_hit = false;
    }

    void PackageHPColor()
    {
        var lotsOfPackages = GameObject.FindGameObjectsWithTag("Package");
        foreach(var lot in lotsOfPackages)
        {
            var spriterender = lot.GetComponent<SpriteRenderer>();
        }

        switch (packageHP)
        {
            default:
                break;
            case 1:
                spriteRenderer.color = new Color(255f/255f, 169f/255f, 0f/255f); //why need to divide why unity i dun understand 
                packageValue = packageValue / 2;

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

    //if package hits the floor, change layer to default
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && package_floor_hit == false)
        {
            --packageHP;

            this.gameObject.layer = LayerMask.NameToLayer("Default"); //change it so it could be picked up again

            PackageHPColor(); 
            Debug.Log($"PACKAGE TOO DAMAGE! CURRENTLY AT {packageHP}");

            package_floor_hit = true;
        }
    }
}

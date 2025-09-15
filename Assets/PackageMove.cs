using UnityEngine;
using System.Collections.Generic;

public class PackageMove : MonoBehaviour
{
    public GameObject packageParent;
    private List<GameObject> attachedPackages = new List<GameObject>();

    private void Awake()
    {
        packageParent = GameObject.FindGameObjectWithTag("Paddle");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Package") && packageParent != null)
        {
            GameObject package = collision.gameObject;

            // Check if this package is not already attached
            if (!attachedPackages.Contains(package))
            {
                package.transform.SetParent(packageParent.transform);
                attachedPackages.Add(package);
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Package"))
        {
            GameObject package = collision.gameObject;

            // Check if this package is in our list
            if (attachedPackages.Contains(package))
            {
                package.transform.SetParent(null);
                attachedPackages.Remove(package);
            }
        }
    }

    public void Force() //adds force to the packages to make it so it wont fall off (but it looks janky)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (GameObject package in attachedPackages)
            {
                Rigidbody2D rb = package.GetComponent<Rigidbody2D>();
                if (Input.GetAxisRaw("Horizontal") >= 0 && package.transform.position.x >= 0) //move right and add the force 
                {
                    rb.AddForceX(155, ForceMode2D.Force);
                }
                if (Input.GetAxisRaw("Horizontal") <= 0 && package.transform.position.x <= 0)
                {
                    rb.AddForceX(-155, ForceMode2D.Force);
                }
            }
        }
    }

    // Optional: Clean up destroyed packages from the list
    private void Update()
    {
        attachedPackages.RemoveAll(package => package == null);

    }
}



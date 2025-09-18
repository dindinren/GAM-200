using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PackageMove : MonoBehaviour
{
    public GameObject packageParent;
    private List<GameObject> attachedPackages = new List<GameObject>();

    public GameObject receipient; //temp  
    
    // Track if we're in a valid state to modify parent relationships
    private bool canModifyParents = true;

    private void Awake()
    {
        // Ensure we have a package parent reference
        if (packageParent == null)
        {
            Debug.LogWarning("PackageParent is not assigned in the inspector. Using this GameObject as parent.");
            packageParent = gameObject;
        }

    }

    private void Start()
    {
        receipient.SetActive(false);
    }
    private void OnEnable()
    {
        // Only allow parent modifications in play mode
        canModifyParents = Application.isPlaying;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canModifyParents) return;

        GameObject package = collision.gameObject;

        if (collision.transform.CompareTag("Package") && packageParent != null)
        {
            // Check if this package is not already attached and is valid
            if (!attachedPackages.Contains(package) && package != null)
            {
                package.transform.SetParent(packageParent.transform);
                attachedPackages.Add(package);

                //set the pacakge to ignore raycast
                package.layer = LayerMask.NameToLayer("Ignore Raycast");

            }

            //allow player to talk to the receipient
            receipient.SetActive(true);

        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //}

    public List<GameObject> GetAttachedPackagesList()
    {
        return attachedPackages;
    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!canModifyParents) return;

        if (collision.transform.CompareTag("Package"))
        {
            GameObject package = collision.gameObject;

            // Check if this package is in our list and is valid
            if (attachedPackages.Contains(package) && package != null)
            {
                package.transform.SetParent(null);
                attachedPackages.Remove(package);
            }

        }
    }

    

    // Clean up destroyed packages from the list
    private void Update()
    {
        if (!canModifyParents) return;

        attachedPackages.RemoveAll(package => package == null);

    }

    // This is called when the component is being destroyed
    private void OnDestroy()
    {
        // Prevent any further parent modifications
        canModifyParents = false;

        // Safely clear the list without modifying parents
        for (int i = attachedPackages.Count - 1; i >= 0; i--)
        {
            var package = attachedPackages[i];
            if (package != null)
            {
                // Don't modify parent during destruction, just remove from list
                attachedPackages.RemoveAt(i);
            }
        }
    }

    // Additional safety: Called when the application is quitting
    private void OnApplicationQuit()
    {
        canModifyParents = false;
        attachedPackages.Clear();
    }
}
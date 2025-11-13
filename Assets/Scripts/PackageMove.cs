using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.ReloadAttribute;
public class PackageMove : MonoBehaviour
{
    public GameObject packageParent;
    private List<GameObject> attachedPackages = new List<GameObject>();

    //public GameObject receipient; //temp  

    float distPackageMove = 5;
    
    // Add these variables for smooth rotation return
    private float rotationReturnSpeed = 2f; // Adjust this to control how fast it returns to 0
    private bool isReturningToZero = false;
    private float tiltRotation = 0;

    bool canPlacePackage = false;

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
        //if (receipient)
        //{
        //    receipient.SetActive(false);
        //}
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

        if (collision.transform.CompareTag("Package") && packageParent != null    )
        {
            // Check if this package is not already attached and is valid
            if (!attachedPackages.Contains(package) && package != null)
            {
                package.transform.SetParent(packageParent.transform);

                attachedPackages.Add(package);

                UIPanel1Manager.currentWeight += package.GetComponent<PackageManager>().weight;

                //set the pacakge to ignore raycast
                package.layer = LayerMask.NameToLayer("Ignore Raycast");
                //stop from deforming                
                package.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
            }

            ////allow player to talk to the receipient
            //if (receipient)
            //{
            //    receipient.SetActive(true);
            //}
        }
    }

    #region Tilting
    public void TiltMove()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput < 0)
        {
            isReturningToZero = false;
            if (tiltRotation > -Mathf.PI / 72.0f) // 2.5 degrees in radians
            {
                packageParent.transform.Rotate(0, 0, -10 * Time.deltaTime);
                //move the package to the right


            }
            tiltRotation = packageParent.transform.rotation.z;
        }
        else if (horizontalInput > 0)
        {
            isReturningToZero = false;
            if (tiltRotation < Mathf.PI / 72.0f) // 2.5 degrees in radians
            {
                packageParent.transform.Rotate(0, 0, 10 * Time.deltaTime);
            }
            tiltRotation = packageParent.transform.rotation.z;
        }
        else
        {
            // When no input, gradually return to zero rotation
            ReturnToZeroRotation();
        }
    }

    private void ReturnToZeroRotation()
    {
        // Get current rotation in degrees for easier calculation
        float currentZRotation = packageParent.transform.rotation.eulerAngles.z;

        // Convert to -180 to 180 range for easier interpolation
        if (currentZRotation > 180f)
            currentZRotation -= 360f;

        // If we're already very close to zero, snap to zero
        if (Mathf.Abs(currentZRotation) < 0.1f)
        {
            packageParent.transform.rotation = Quaternion.identity;
            tiltRotation = 0f;
            isReturningToZero = false;
            return;
        }

        // Smoothly interpolate back to zero
        float targetRotation = Mathf.Lerp(currentZRotation, 0f, rotationReturnSpeed * Time.deltaTime);

        // Apply the rotation
        Vector3 currentRotation = packageParent.transform.rotation.eulerAngles;
        packageParent.transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, targetRotation);

        // Update tiltRotation for our limits
        tiltRotation = packageParent.transform.rotation.z;
        isReturningToZero = true;
    }
    #endregion


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
                UIPanel1Manager.currentWeight -= package.GetComponent<PackageManager>().weight;
                //package.GetComponent<Rigidbody2D>().simulated = true;
                //add force to the package?

            }

        }
    }


    // Clean up destroyed packages from the list
    private void Update()
    {
        TiltMove();
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
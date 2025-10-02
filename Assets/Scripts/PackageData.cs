using System;
using UnityEngine;
[CreateAssetMenu(menuName = "Packgae")]
public class PackageData : ScriptableObject
{
    public int packageID;
    public int packageHP = 2;
    public int packageValue;
    public GameObject type;

    public string packageReceipientName;
    public string packageDescription; //this will be used later when i get the delivery to work | used for selecting which package to deliver


    public bool isSelected; // Add this field

    public void PackageHPColorData()
    {
        //var spriteRenderer = spawnManager.GetPackageClone().GetComponent<SpriteRenderer>();
        var spriteRenderer = type.GetComponent<SpriteRenderer>();
        switch (packageHP)
        {
            default:
                break;
            case 1:
                spriteRenderer.color = new Color(255f / 255f, 169f / 255f, 0f / 255f); //why need to divide why unity i dun understand 
                packageValue = packageValue /= 2;

                break;
            case 0:
                spriteRenderer.color = Color.red;
                packageValue = packageValue / 2;
                break;
        }
    }
}

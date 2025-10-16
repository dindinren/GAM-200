using UnityEngine;

public class PlayerPackageCheck : MonoBehaviour
{
    /// <summary>
    /// Check if the player has any packages on top before heading out
    /// </summary>

    public GameObject invisibleWall;
    public GameObject SceneChangeTriggerArea;
    public PackageMove PackageMove;
    //player packages

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PackageMove = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PackageMove>();

        SceneChangeTriggerArea.SetActive(false);
        invisibleWall.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Checker()
    {
        Debug.Log($"packages: {PackageMove.GetAttachedPackagesList().Count} and required: {WarehouseSpawnManager.requiredNumber}");
        if (PackageMove.GetAttachedPackagesList().Count == WarehouseSpawnManager.requiredNumber)
        {
            SceneChangeTriggerArea.SetActive(true);
            invisibleWall.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Checker();
    }

}

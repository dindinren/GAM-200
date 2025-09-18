using UnityEngine;

[CreateAssetMenu (fileName = "New Receipient", menuName = "Receipient")]
public class ReceipientData : ScriptableObject
{
    public int recipientID;
    public string receipientName;
    public float receipientPosX; // spawn location of the receipients
    public float receipientPosY;

    public void Print()
    {
        Debug.Log($"The receipient is [{receipientName}], ID is [{recipientID}] and spawned at [{receipientPosX}],[{receipientPosY}]");
    }
}

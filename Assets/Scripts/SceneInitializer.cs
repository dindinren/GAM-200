using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    public Transform startLocation;
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = startLocation.position;
    }
}

using UnityEngine;

public class StopCameraFollow : MonoBehaviour
{

    private void Awake()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraFollow.isFollowing = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraFollow.isFollowing = true;
        }
    }
}

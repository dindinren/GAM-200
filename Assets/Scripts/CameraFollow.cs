using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset;
    public float xOffset;
    public Transform target;

    public static bool isFollowing;

    private Vector3 newPos;

    public GameObject stopCameraFollowArea;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CameraUpdate();
    }

    public void CameraUpdate()
    {
        if (isFollowing == true)
        {
            newPos = new Vector3(target.position.x + xOffset, target.position.y + yOffset, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        }
    }


}

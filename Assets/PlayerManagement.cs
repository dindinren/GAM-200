using UnityEngine;
using System.Collections;
public class PlayerManagement : MonoBehaviour
{
    public GameObject player;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }


    public void Movement()
    {
        var horizontalinput = Input.GetAxisRaw("Horizontal");

        Vector3 movement = new Vector3(horizontalinput, 0, 0);
        transform.Translate(movement *  speed * Time.deltaTime);

    }

}

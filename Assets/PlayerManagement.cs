using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
public class PlayerManagement : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float jumpForce;
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

        var horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector3 movement = new Vector3(horizontalInput, 0, 0);
        transform.Translate(movement *  speed * Time.deltaTime);


        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if(Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector3(rb.linearVelocityY,jumpForce));
        }

    }


}

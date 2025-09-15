using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
public class PlayerManagement : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float jumpForce;
    public bool touchingFloor;

    public GameObject packageTriggerArea; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        packageTriggerArea.SetActive(true);
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

        if(Input.GetKeyDown(KeyCode.Space) && touchingFloor == true)
        {
            rb.AddForce(new Vector3(rb.linearVelocityY,jumpForce));
            touchingFloor = false;
            packageTriggerArea.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Ground")
        {
            touchingFloor = true;
            packageTriggerArea.SetActive(true);
        }
    }

}

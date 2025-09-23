using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
public class PlayerManagement : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float jumpForce;
    public bool touchingFloor;
    public int money;

    public GameObject packageTriggerArea; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        packageTriggerArea.SetActive(true);

        ///temp placement of music
        SoundManager.PlayBGM(BGM.MENU);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public int SetMoneySatus(int addMoney)
    {
        money+= addMoney;
        Debug.Log($"Player now has: {money}");

        return money;
    }

    public int GetMoneySatus()
    {
        return money;
    }

    public void Movement()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector3 movement = new Vector3(horizontalInput, 0, 0);
        transform.Translate(movement *  speed * Time.deltaTime);


        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        ///JUMP
        if(Input.GetKeyDown(KeyCode.Space) && touchingFloor == true)
        {
            rb.AddForce(new Vector3(rb.linearVelocityY,jumpForce));
            touchingFloor = false;
            packageTriggerArea.SetActive(false);

            //play jump SFX
            SoundManager.PlaySound(SoundType.JUMP);
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

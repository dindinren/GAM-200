using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
public class PlayerManagement : MonoBehaviour
{
    public GameObject player;
    public GameObject interactionButton;
    public GameObject packageTriggerArea;

    public PackageMove PackageMove;

    [Header("PLAYER STATS")]
    public float speed;
    public float jumpForce;
    public bool touchingFloor;
    public int money;


    private void Awake()
    {
        if(PackageMove == null)
        {
            PackageMove = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PackageMove>();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        packageTriggerArea.SetActive(true);
        interactionButton.SetActive(false);

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
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        SpriteRenderer spriteFlip = player.GetComponent<SpriteRenderer>();

        Vector3 movement = new Vector3(horizontalInput, 0, 0);
        transform.Translate(movement *  speed * Time.deltaTime);

        ///flip the sprite
        if(horizontalInput < 0)
        {
            spriteFlip.flipX = true;
        }
        else
        {
            spriteFlip.flipX= false;
        }

        Jump();

    }

    void Jump()
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        ///JUMP
        if (Input.GetKeyDown(KeyCode.Space) && touchingFloor == true)
        {
            rb.AddForce(new Vector3(rb.linearVelocityY,jumpForce));
            foreach (GameObject pack in PackageMove.GetAttachedPackagesList())
            {
                Debug.Log($"packages carried are: {pack}");
                if (pack.gameObject.tag == "Package")
                {
                    Rigidbody2D rb2 = pack.GetComponent<Rigidbody2D>();
                    rb2.AddForceX(50, ForceMode2D.Force);
                }
            }

            touchingFloor = false;

            ///Add coroutine to delay turning off 
            //StartCoroutine(DelayTriggerAreaOff());
            packageTriggerArea.SetActive(false);

            //play jump SFX
            SoundManager.PlaySound(SoundType.JUMP);
        }
    }

    //private IEnumerator DelayTriggerAreaOff()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    packageTriggerArea.SetActive(false);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Ground")
        {
            touchingFloor = true;
            packageTriggerArea.SetActive(true);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Receipient")
        {
            interactionButton.SetActive(true);
        }
        //Debug.Log("SHOW F");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Receipient")
        {
            interactionButton.SetActive(false);
        }
    }

}

using UnityEngine;

public class PackageMove2 : MonoBehaviour
{
    //if packageMove is so good, why is there no packageMove2
    //holy shit

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForceY(200, ForceMode2D.Force);
        }
    }


}

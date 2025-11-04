using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    public GameObject dialogueBox;
    public PlayerManagement player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagement>();
    }
    void BumpIntoWall()
    {
        dialogueBox.SetActive(true);
        Dialogue dlog = dialogueBox.GetComponent<Dialogue>();
        dlog.DialogueAppear();
        player.enabled = true;
        Dialogue.count++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BumpIntoWall();
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Dialogue.dialogueEnded = false;
    }

    private void FixedUpdate()
    {
        if (Dialogue.dialogueEnded)
        {
            player.enabled = true;
        }
    }
}

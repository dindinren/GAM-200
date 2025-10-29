using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogueBox;
    public GameObject continueButton;

    //public static Dialogue instance;

    [Header("LINES")]
    public string[] lines;
    public TextMeshProUGUI text;
    public float textSpeed;

    private int index;
    public static int count;

    public static bool dialogueEnded;
    bool key_Is_Pressed;
    int keyPresses;

    private void Awake()
    {

    }
    private void Start()
    {

        dialogueEnded = false;

        count = 0;

        //Set the dialogue box to not show first
        dialogueBox.SetActive(false);
        continueButton.SetActive(false);

    }
    private void Update()
    {
        

    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            key_Is_Pressed = true;
            keyPresses++;
            Advance();
        }
        else
        {
            key_Is_Pressed = false;
            keyPresses = 0;
        }

    }

    void Advance()
    {
        //Advance to the next line
        if (key_Is_Pressed && keyPresses == 1)
        {
            if (text.text == lines[index])
            {
                continueButton.SetActive(true);
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                //get the current line
                text.text = lines[index];
                continueButton.SetActive(true);
            }

            Debug.Log($"index: {index}");

        }


    }
    public void DialogueAppear()
    {
        text.text = string.Empty;
        StartDialogue();
    }
    void StartDialogue()
    {
        if(count == 0)
        {
            dialogueEnded = false;

            index = 0;

            StartCoroutine(TypeLine());
        }

        Debug.Log($"Dialogue Count: {Dialogue.count}");

    }

    IEnumerator TypeLine()
    {
        //lines[index] = string.Empty;
        text.text = string.Empty;
        foreach (char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            StopAllCoroutines();
            continueButton.SetActive(false);

            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogueEnded = true;

            dialogueBox.SetActive(false);
            continueButton.SetActive(false);
        }
    }


}

using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogueBox;

    [Header("LINES")]
    public string[] lines;
    public TextMeshProUGUI text;
    public float textSpeed;

    private int index;

    public static bool dialogueEnded;

    private void Start()
    {
        dialogueEnded = false;

        //Set the dialogue box to not show first
        dialogueBox.SetActive(false);

    }
    private void Update()
    {
        
        //Advance to the next line
        if (Input.GetMouseButtonDown(0))
        {
            if(text.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                //get the current line
                text.text = lines[index];

            }
        }
    }

    public void DialogueAppear()
    {
        text.text = string.Empty;
        StartDialogue();
    }
    void StartDialogue()
    {
        dialogueEnded = false;

        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {

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

            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogueEnded = true;

            dialogueBox.SetActive(false);
        }
    }


}

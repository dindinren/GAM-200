using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogue_Tutorial : MonoBehaviour
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

    public bool proceed; //for tutorial dialogue

    public static bool spawned;
    bool key_Is_Pressed;
    int keyPresses;

    private void Awake()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void ChangedActiveScene(Scene current, Scene next) //here cus the build does not sometimes play the music 
    {
        dialogueEnded = false;
        proceed = false;
        spawned = false;
        count = 0;
    }

    private void Start()
    {
        //Set the dialogue box to not show first
        //dialogueBox.SetActive(false);
        DialogueAppear();
        continueButton.SetActive(false);

    }
    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (text.text == lines[index])
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            key_Is_Pressed = true;
            keyPresses++;
        }
        else
        {
            key_Is_Pressed = false;
            keyPresses = 0;
        }


        Advance();
    }

    void Advance()
    {
        if (key_Is_Pressed && keyPresses == 1)
        {
            if (text.text == lines[index])
            {
                NextLine();
                Debug.Log($"index: {index}");
                Debug.Log($"count: {count}");
            }
            else
            {
                StopAllCoroutines();
                text.text = lines[index]; //get the current line
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
        if (!spawned)
        {
            if (count == 0)
            {
                dialogueEnded = false;
                index = 0;

                StartCoroutine(TypeLine());

            }
        }

        Debug.Log($"Dialogue Count: {Dialogue.count}");
        Debug.Log($"DIALOGUE SPAWNED {spawned}");
    }

    IEnumerator TypeLine()
    {
        text.text = string.Empty;
        foreach (char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        continueButton.SetActive(true);
    }

    void NextLine()
    {
        if (index < lines.Length - 1 && dialogueEnded == false)
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
            proceed = true;
            dialogueBox.SetActive(false);
            count = 0;
        }
    }


}

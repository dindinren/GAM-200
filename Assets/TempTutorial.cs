using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class TempTutorial : MonoBehaviour
{
    public List<GameObject> panels = new List<GameObject>();
    int index = 0;
    int newIndex = 0;
    public Button nextButton;
    public Button backButton;
    public Button closeButton;


    public void OpenPage()
    {
        panels[0].SetActive(true);
        backButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
    }

    public void ClosePage()
    {
        panels[index].SetActive(false);

        backButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
    }

    public void NextPage()
    {
        newIndex = newIndex + 1;


        if (index < panels.Count)
        {
            Debug.Log($"index: {index}");
            Debug.Log($"previndex: {newIndex}");

            panels[index].SetActive(false);
            panels[newIndex].SetActive(true);

            ++index;

            if (index > 0)
            {
                nextButton.gameObject.SetActive(false);
                backButton.gameObject.SetActive(true);
                closeButton.gameObject.SetActive(true);
            }

        }
        else
        {
            backButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);
        }

    }

    public void PrevPage()
    {
        newIndex = newIndex - 1;


        if (index < panels.Count)
        {
            Debug.Log($"index: {index}");
            Debug.Log($"previndex: {newIndex}");

            panels[index].SetActive(false);
            panels[newIndex].SetActive(true);

            --index;

            if (index == 0)
            {
                nextButton.gameObject.SetActive(true);
                backButton.gameObject.SetActive(false);
                closeButton.gameObject.SetActive(false);
            }

        }
        else
        {
            backButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        backButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
    }
}

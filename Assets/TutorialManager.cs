using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("Section 1")]
    //public List<GameObject> dialoguesT = new List<GameObject>();
    public GameObject dialogueT_1;
    public GameObject arrow_1;

    private void Awake()
    {
        //First section
        dialogueT_1.SetActive(false);
        arrow_1.SetActive(false);


    }
    private void Start()
    {
       StartCoroutine(Dialogue_1_Sequence());
    }

    private void Update()
    {
        Dialogue_1_EndChecker();
    }

    IEnumerator Dialogue_1_Sequence()
    {
        yield return new WaitForSeconds(0.5f);
        dialogueT_1.SetActive(true);
    }

    void Dialogue_1_EndChecker()
    {
        if (dialogueT_1.GetComponent<Dialogue_Tutorial>().proceed == true)
        {
            arrow_1.SetActive(true);
        }
    }
}

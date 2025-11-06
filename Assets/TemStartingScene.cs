using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TemStartingScene : MonoBehaviour
{
    public List<Image> cutscenes = new List<Image>();
    public Animator anim;

    public string SceneName;
    int index = 0;
    //int count = 0;
    bool key_is_pressed;
    //bool Continue;

    private void Start()
    {
        index = 0;
        foreach(Image image in cutscenes)
        {
            image.gameObject.SetActive(false);
        }
        cutscenes[index].gameObject.SetActive(true);
    }

    private void Update()
    {
        if(key_is_pressed /*&& count == 0*/)
        {
            NewScene();
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            key_is_pressed = true;
        }
        else
        {
            key_is_pressed = false;
        }

    }


    void NewScene()
    {
        //count++;

        if (key_is_pressed)
        {
            index++;
            key_is_pressed = false;

            //Debug.Log($"Count: {count}");
            Debug.Log($"index: {index}");
            if (index > 0 && index < cutscenes.Count)
            {
                cutscenes[index].gameObject.SetActive(true);
                cutscenes[index - 1].gameObject.SetActive(false);
            }

            if (index == cutscenes.Count)
            {
                StartCoroutine(RightToLeftTransit());
                SceneChange.changed = true;   
            }
        }

        //count = 0;
        //Debug.Log($"Count: {count}");
        //Debug.Log($"key_is_pressed: {key_is_pressed}");

    }



    IEnumerator RightToLeftTransit()
    {
        //play the transition start
        anim.Play("CrossFade_RightToLeft_Start");
        //wait for a while
        yield return new WaitForSeconds(1);
        //load 
        SceneManager.LoadScene(SceneName);
    }

}

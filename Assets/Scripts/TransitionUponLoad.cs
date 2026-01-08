using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionUponLoad : MonoBehaviour
{
    public Animator transition;
    private void Awake()
    {
        transition = GetComponent<Animator>();
        if(SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "OpeningCutScene")
        {
            transition.Play("CrossFade_Default2");
        }
        else
        {
            transition.Play("CrossFade_Default");
        }

        Checker();
    }
    void Checker()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "WarehouseOutside":
                transition.Play("CrossFade_LeftToRight_End");
                break;
            case "WarehouseV2":
                transition.Play("CrossFade_RightToLeft_End");
                break;
            case "WarehouseV2_Tutorial":
                transition.Play("CrossFade_RightToLeft_End");
                break;
            case "TownArea":
                transition.Play("CrossFade_RightToLeft_End");
                break;
            //case "OpeningCutScene":
            //    transition.Play("CrossFade_RightToLeft_End");
            //    break;

            // TEMP TO DELETE ONCE EVERYTHING IS GOOD
            case "TempStartingScene":
                transition.Play("CrossFade_RightToLeft_End");
                break;
            case "TempNewDay":
                transition.Play("CrossFade_RightToLeft_End");
                break;
        }
    }
}

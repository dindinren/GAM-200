using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionUponLoad : MonoBehaviour
{
    public Animator transition;
    private void Awake()
    {
        transition = GetComponent<Animator>();
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
            case "TownArea":
                transition.Play("CrossFade_RightToLeft_End");
                break;
        }
    }
}

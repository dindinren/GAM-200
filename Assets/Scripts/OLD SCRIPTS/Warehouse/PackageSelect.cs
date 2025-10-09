using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PackageSelect : MonoBehaviour
{
    int selectedTotal=0;
    public GameObject cockBlock;
    public GameObject confirm;
    Toggle c;
    public bool[] selected;

    public Canvas canvas;
    public void selecMe(Toggle me)
    {
        if (me.isOn)
        {
            selectedTotal++;

        }
        else
        {
            selectedTotal--;
        }
        c = me;

    }
    public int GetSelectedTotal()
    {
        return selectedTotal;
    }
    public void updateSelection(int toggleNo)
    {
        selected[toggleNo] = c.isOn;
    }
    public void Recount()
    {
        if (selectedTotal > 1)
        {
            cockBlock.SetActive(false);
            confirm.SetActive(true);
        }
        else
        {
            cockBlock.SetActive(true);
            confirm.SetActive(false);
        }
    }

    public void GoNextScreen()
    {
        SceneManager.LoadScene("Stack");
        DontDestroyOnLoad(canvas);

    }
}

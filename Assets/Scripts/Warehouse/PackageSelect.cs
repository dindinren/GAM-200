using UnityEngine;
using UnityEngine.UI;

public class PackageSelect : MonoBehaviour
{
    int selectedTotal;
    public GameObject cockBlock;
    public GameObject confirm;
    Toggle c;
    public bool[] selected;

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
}

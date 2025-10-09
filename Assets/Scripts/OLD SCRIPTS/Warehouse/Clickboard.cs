using UnityEngine;

public class Clickboard : MonoBehaviour
{
    public GameObject TheList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void OnMouseDown()
    {
        TheList.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

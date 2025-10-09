using UnityEngine;
using UnityEngine.SceneManagement;

public class TempSceneChange : MonoBehaviour
{
    public GameObject player;
    public GameObject Belt;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("SampleScene");
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(Belt);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneInitializer : MonoBehaviour
{
    public Transform startLocation;
    //public Animator transition;
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = startLocation.position;

    }

}

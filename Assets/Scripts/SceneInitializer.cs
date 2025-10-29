using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneInitializer : MonoBehaviour
{
    public Transform startLocation;
    public Animator anim;
    //public Animator transition;
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = startLocation.position;

        anim = GameObject.Find("CrossFade").GetComponent<Animator>();

        anim.Play("CrossFade_Default");


        if(SceneManager.GetActiveScene().name == "WarehouseOutside")
        {
            CameraFollow.isFollowing = true;
        }

    }

}

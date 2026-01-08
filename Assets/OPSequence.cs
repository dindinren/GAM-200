using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class OPSequence : MonoBehaviour
{
    public GameObject opening;
    public VideoPlayer videoPlayer;
    public string SceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(OpeningStarto());

    }

    void OnVideoEnd(VideoPlayer vp)
    {
        transitionFade.Instance.RightToLeftTransition(SceneName);
    }

    IEnumerator OpeningStarto()
    {
        opening.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        opening.SetActive(true);
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // Update is called once per frame
    void Update()
    {
    }
}

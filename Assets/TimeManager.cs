using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public GameObject sceneSwitch; //everytime the player steps here it's one hour off
    public TextMeshProUGUI textComponent;

    public static bool gameOngoing = true;

    int currentTime;
    public static int startingTime = 12;
    public int endTime;
    int elapsedTime = 1;
    public static int currentMin;

    private void Start()
    {
        textComponent.text = string.Empty;
        gameOngoing = true;
    }
    private void Update()
    {
        if (gameOngoing)
        {
            TimeManagement();
        }
    }

    public void TimeManagement()
    {
        if(startingTime == 0)
        {
            startingTime = 12;
        }
        
        if(startingTime > 12)
        {
            startingTime = 1;
            //Debug.Log($"startingTime = {startingTime}");
        }
        string pm = "PM";

        textComponent.text = string.Format("{0:00}:{1:00} {2}", startingTime, currentMin, pm);
    }

    /// <summary>
    /// 1) if the min = 0, min = 30 while don't add hour 
    /// 2) if the min = 30, min = 0 and hour+1
    /// THIS IS TO GIVE THE PSEUDO EFFECT OF PASSING TIME
    /// </summary>
    public void TimeAdvance()
    {
        if(currentMin == 0)
        {
            currentMin = 30;
        }
        else
        { 
            currentMin = 0;
            startingTime += elapsedTime;
        }

        if (startingTime < endTime)
        {
            gameOngoing = false;
        }
        else
        {
            gameOngoing = true;
        }

        //Debug.Log($"gameOngoing = {gameOngoing}");
    }
}


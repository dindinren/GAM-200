using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerManagement playerManage;
    public TextMeshProUGUI moneyText;


    private void Awake()
    {
        playerManage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagement>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisaplayMoney();
    }

    public void DisaplayMoney()
    {
        moneyText.text = $"Money: ${playerManage.GetMoneySatus()}";
    }
}

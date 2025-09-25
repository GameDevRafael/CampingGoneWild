using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

    public TMP_Text coinUIText;
    private GameObject playerInstance;
    private PlayerInventory playerBag;

    private string coinText = "Coins:";
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInstance = GameObject.FindGameObjectWithTag("Player");
        playerBag = playerInstance.GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        coinUIText.text = coinText + playerBag.coins;
    }
}

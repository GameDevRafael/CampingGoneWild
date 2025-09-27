using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public TMP_Text coinUIText;
    private GameObject playerInstance;
    private PlayerInventory playerBag;

    public GameObject deathInterface;

    public static UIController Instance;

    public void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInstance = GameObject.FindGameObjectWithTag("Player");
        playerBag = playerInstance.GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        coinUIText.text = playerBag.coins.ToString();
    }

    public void MainMenuButton() {
        deathInterface.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayAgain() {
        deathInterface.SetActive(false);
        SceneManager.LoadScene("GamePlay");
    }
}

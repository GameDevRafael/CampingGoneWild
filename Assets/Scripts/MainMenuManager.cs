using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Play() {
        SceneManager.LoadScene("Gameplay");
    }

    public void ShowInstructions() {

    }

    public void Settings() {

    }

    public void Quit() {
        Application.Quit();
    }
}

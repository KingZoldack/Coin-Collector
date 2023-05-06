using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    SceneManager _mainMenuScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainMenuScene() => SceneManager.LoadScene("Main Menu");
    public void LoadMainScene() => SceneManager.LoadScene("Main Scene");
    public void LoadShopScene() => SceneManager.LoadScene("Shop");



}

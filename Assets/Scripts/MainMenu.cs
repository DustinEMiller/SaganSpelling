using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.DeleteAll();;
        Debug.Log(PlayerPrefs.GetString( "UseAllWords"));
        Debug.Log(PlayerPrefs.GetString( "Tutorial"));
        
        transform.Find("Play").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });
        transform.Find("Quit").GetComponent<Button>().onClick.AddListener(() =>
        {
            Application.Quit();
        });
        transform.Find("All Words").GetComponent<Toggle>().onValueChanged.AddListener((value) =>
        {
            PlayerPrefs.SetString( "UseAllWords", value.ToString());
            Debug.Log(PlayerPrefs.GetString( "UseAllWords"));
        });
        transform.Find("Tutorial").GetComponent<Toggle>().onValueChanged.AddListener((value) =>
        {
            PlayerPrefs.SetString( "Tutorial", value.ToString());
            Debug.Log(PlayerPrefs.GetString( "Tutorial"));
        });
        
        
    }
}

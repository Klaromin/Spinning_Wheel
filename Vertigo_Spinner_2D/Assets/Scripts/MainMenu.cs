using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;

    private void Start()
    {
        AddEvents();
    }

    private void OnDisable()
    {
        RemoveEvents(); //DENE
    }

    private void Play()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    private void Quit()
    {
       Application.Quit();
    }

    private void AddEvents()
    {
        _playButton.onClick.AddListener(Play);
        _quitButton.onClick.AddListener(Quit);
    }



    private void RemoveEvents()
    {
        
    }
}

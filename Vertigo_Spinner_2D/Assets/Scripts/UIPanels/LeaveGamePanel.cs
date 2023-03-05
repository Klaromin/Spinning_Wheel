using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaveGamePanel : MonoBehaviour
{
    [SerializeField] private Button _leaveGameButton;

    private void Start()
    {
        AddEvents();
    }

    private void AddEvents()
    {
        _leaveGameButton.onClick.AddListener(LeaveGame);
    }

    private void LeaveGame()
    {
        if (IsQuitAvailable())
        {
            SceneManager.LoadScene("MenuScene");
        }
        
    }

    private bool IsQuitAvailable()
    {
        return GameManager.Instance.State == GameState.Start;
    }
}

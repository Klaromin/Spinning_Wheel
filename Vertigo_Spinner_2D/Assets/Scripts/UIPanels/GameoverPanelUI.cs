using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameoverPanelUI : MonoBehaviour
{
    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _menuButton;
    private Vector3 _startingScale;
    void Start()
    {
        AddEvents();
       _startingScale = transform.localScale;
       transform.localScale = Vector3.zero;

    }

    public void Pop()
    {
        if (GameManager.Instance.State == GameState.GameOver)
        {
            transform.DOScale(_startingScale, 1f).SetEase(Ease.OutBack);
        }
        
    }

    public void Hide()
    {
        transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InBack);
    }

    private void AddEvents()
    {
        GameManager.Instance.OnRewardDecidedEvent += OnRewardDecided;
        _retryButton.onClick.AddListener(Hide);
        _menuButton.onClick.AddListener(MainMenuLoader);
    }

    private void MainMenuLoader()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void OnRewardDecided(object sender, EventArgs e)
    {
        Pop();
    }
}

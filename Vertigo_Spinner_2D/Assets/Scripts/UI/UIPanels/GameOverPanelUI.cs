using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VertigoDemo.Data;
using VertigoDemo.Managers;

namespace VertigoDemo.UI.UIPanels
{
    public class GameOverPanelUI : MonoBehaviour
    {
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _menuButton;
    
        private Vector3 _startingScale;
    
        private void Start()
        {
            AddEvents();
            _startingScale = transform.localScale;
            transform.localScale = Vector3.zero;
        }

        private void OnDisable()
        {
            RemoveEvents();
        }

        private void Pop()
        {
            if (GameManager.Instance.State == GameState.GameOver)
            {
                transform.DOScale(_startingScale, 1f).SetEase(Ease.OutBack);
            }
        }

        private void Hide()
        {
            transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InBack);
        }

        private void MainMenuLoader()
        {
            SceneManager.LoadScene("MenuScene");
        }

        private void AddEvents()
        {
            GameManager.Instance.OnRewardDecidedEvent += OnRewardDecided;
            _retryButton.onClick.AddListener(Hide);
            _menuButton.onClick.AddListener(MainMenuLoader);
        }

        private void RemoveEvents()
        {
            GameManager.Instance.OnRewardDecidedEvent -= OnRewardDecided;
            _retryButton.onClick.RemoveAllListeners();
            _menuButton.onClick.RemoveAllListeners();
        }

        private void OnRewardDecided(object sender, EventArgs e)
        {
            Pop();
        }
    }
}
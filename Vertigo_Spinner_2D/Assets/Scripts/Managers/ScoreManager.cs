using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VertigoDemo.Data;
using VertigoDemo.Helpers;
using VertigoDemo.UI.RewardMVC;

namespace VertigoDemo.Managers
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        [SerializeField] private TextMeshProUGUI _goldAmount;
        [SerializeField] private TextMeshProUGUI _moneyAmount;
        [SerializeField] private TextMeshProUGUI _medkitAmount;
        [SerializeField] private TextMeshProUGUI _c4Amount;
        [SerializeField] private TextMeshProUGUI _empAmount;
        [SerializeField] private TextMeshProUGUI _healthshotAmount;
    
        private RewardView _selectedReward;
        private List<ItemScore> _scores = new();

        void Start()
        {
            AddEvents();
            SetInitialScore();
            InitTexts();
        }

        private void OnDisable()
        {
            RemoveEvents();
        }

        private void SetInitialScore()
        {
            foreach (var saferewardData in Configuration.RewardData.AllSafeRewardData)
            {
                var score = new ItemScore()
                {
                    Score = 0,
                    RewardType = saferewardData.RewardType
                };
                _scores.Add(score);
            }
        }

        private void InitTexts()
        {
            foreach (var score in _scores)
            {
                var amountText = GetTextForRewardType(score.RewardType);
                amountText.text = score.Score.ToString();
            }
        }

        private TextMeshProUGUI GetTextForRewardType(RewardType type)
        {
            return type switch
            {
                RewardType.C4 => _c4Amount,
                RewardType.Gold => _goldAmount,
                RewardType.EMP => _empAmount,
                RewardType.Money => _moneyAmount,
                RewardType.MedKit => _medkitAmount,
                RewardType.HealthShot => _healthshotAmount,
                _ => throw new NotImplementedException("This type is not Implemented")
            };
        }

        private void SetScore()
        {
            if (GameManager.Instance.State == GameState.Reward)
            {
                UpdateScore();
            }
            if (GameManager.Instance.State == GameState.GameOver)
            {
                SetInitialScore();
            }
            InitTexts();
        }

        private void UpdateScore()
        {
            var rewardAmount = _selectedReward.GetRewardAmount();
            var rewardType = _selectedReward.GetRewardType();

            foreach (var score in _scores)
            {
                if (score.RewardType == rewardType)
                {
                    score.Score += rewardAmount;
                } 
            }
        }

        private void AddEvents()
        {
            GameManager.Instance.OnRewardDecidedEvent += OnRewardDecided;
        }

        private void RemoveEvents()
        {
            GameManager.Instance.OnRewardDecidedEvent -= OnRewardDecided;
        }

        private void OnRewardDecided(object sender, EventArgs e)
        {
            _selectedReward = RewardManager.Instance.GetSelectedReward();
            SetScore();
        }
    }
}
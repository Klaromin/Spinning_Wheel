using System;
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

        private int _gold;
        private int _money;
        private int _c4;
        private int _emp;
        private int _medkit;
        private int _healthshot;

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
            _gold = 0;
            _money = 0;
            _c4 = 0;
            _emp = 0;
            _medkit = 0;
            _healthshot = 0;
        }

        private void InitTexts()
        {
            _goldAmount.text = _gold.ToString();
            _moneyAmount.text = _money.ToString();
            _medkitAmount.text = _medkit.ToString();
            _c4Amount.text = _c4.ToString();
            _empAmount.text = _emp.ToString();
            _healthshotAmount.text = _healthshot.ToString();
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
            switch (rewardType)
            {
                case RewardType.C4:
                    _c4 += rewardAmount;
                    break;
                case RewardType.Gold:
                    _gold += rewardAmount;
                    break;
                case RewardType.EMP:
                    _emp += rewardAmount;
                    break;
                case RewardType.Money:
                    _money += rewardAmount;
                    break;
                case RewardType.MedKit:
                    _medkit += rewardAmount;
                    break;
                case RewardType.HealthShot:
                    _healthshot += rewardAmount;
                    break;
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
using System;
using System.Collections.Generic;
using UnityEngine;
using VertigoDemo.Data;
using VertigoDemo.Managers;
using VertigoDemo.UI.RewardMVC;
using Random = UnityEngine.Random;

namespace VertigoDemo.UI.UIPanels
{
    public class RewardPanel : MonoBehaviour
    {
        [SerializeField] private List<RewardView> _rewardViews;
        private RewardView _currentRewardView;
        private int _counter;
        /* Test amaçlı
        
        private int _currentAmount;
        private bool _isCurrency;
        private Sprite _currentImage;
        private RewardType _currentRewardType; */
        private void Awake()
        {
            InitInnerMVCs();
        }

        private void Start()
        {
            AddEvents();
        }

        private void OnDisable()
        {
            RemoveEvents();
        }

        public void InitInnerMVCs()
        {
            foreach (var view in GetComponentsInChildren<RewardView>())
            {
                RewardModel model = new RewardModel();
                RewardView rewardView = view;
                RewardController controller = new RewardController(model, rewardView);
                controller.Init();
                _rewardViews.Add(rewardView);
            }
        }
    

        private void AddEvents()
        {
            GameManager.Instance.OnRewardDecidedEvent += OnRewardDecided;
            GameManager.Instance.OnSilverSpinReachedEvent += OnSilverSpinReached;
            GameManager.Instance.OnSuperSpinReachedEvent += OnSuperSpinReached;
            GameManager.Instance.OnBronzeSpinEvent += OnBronzeSpinReached;
        }

        private void RemoveEvents()
        {
            GameManager.Instance.OnRewardDecidedEvent -= OnRewardDecided;
            GameManager.Instance.OnSilverSpinReachedEvent -= OnSilverSpinReached;
            GameManager.Instance.OnSuperSpinReachedEvent -= OnSuperSpinReached;
            GameManager.Instance.OnBronzeSpinEvent -= OnBronzeSpinReached;
        }
    
        private void OnRewardDecided(object sender, EventArgs e)
        {
            if (GameManager.Instance.State == GameState.GameOver)
            {
                foreach (var view in GetComponentsInChildren<RewardView>())
                {
                    view.Init();
                }
            }

            if (_currentRewardView != null)
            {
                Debug.Log(_currentRewardView.GetRewardAmount());
            }
        }
        
        private void OnSilverSpinReached(object sender, EventArgs e)
        {
            
            RewardDataTemplate safeReward = Configuration.RewardData.AllSafeRewardData[
                Random.Range(0, Configuration.RewardData.AllSafeRewardData.Count)];
            
            foreach (var rewardView in _rewardViews)
            {
                if (!rewardView.Model.IsExplosive)
                {
                    if (safeReward.RewardType == rewardView.GetRewardType())
                    {
                        _currentRewardView = rewardView;
                        Debug.Log(_currentRewardView.GetRewardType());
                        Debug.Log(_currentRewardView.GetRewardAmount());
                        Debug.Log(_currentRewardView.Model.IsCurrency);
                        Debug.Log(_currentRewardView.GetRewardImage().sprite);
                    }
                }

                else if (rewardView.Model.IsExplosive)
                {               
                    rewardView.Model.IsExplosive = false;
                    rewardView.GetRewardImage().sprite = _currentRewardView.GetRewardImage().sprite;
                    rewardView.Model.RewardAmount = _currentRewardView.GetRewardAmount();
                    rewardView.Model.IsCurrency = _currentRewardView.Model.IsCurrency;
                    rewardView.Model.RewardType = _currentRewardView.GetRewardType();
                }

            }

        }
        
  
        private void OnBronzeSpinReached(object sender, EventArgs e)
        {
            _counter = 0;
            foreach (var rewardView in _rewardViews)
            {
                if (rewardView.Model.IsExplosive)
                {
                    _counter++;
                }
            }

            if (_counter != 0) return;
            int random = Random.Range(0, _rewardViews.Count);
            var explosiveReward = _rewardViews[random];
            explosiveReward.Model.IsExplosive = true;
            explosiveReward.Model.IsCurrency = false;
            explosiveReward.GetRewardImage().sprite =
                Resources.Load<Sprite>(
                    "Art/vertigo_games_game_dev_demo_sprites/ui_items/ui_icon_render_cons_grenade_m67");
            explosiveReward.Model.RewardAmount = 0;

        }
        
        private void OnSuperSpinReached(object sender, EventArgs e)
        {
            
            RewardDataTemplate safeReward = Configuration.RewardData.AllSafeRewardData[
                Random.Range(0, Configuration.RewardData.AllSafeRewardData.Count)];
            
            foreach (var rewardView in _rewardViews)
            {
                if (!rewardView.Model.IsExplosive)
                {
                    if (safeReward.RewardType == rewardView.GetRewardType())
                    {
                        _currentRewardView = rewardView;
                        Debug.Log(_currentRewardView.GetRewardType());
                        Debug.Log(_currentRewardView.GetRewardAmount());
                        Debug.Log(_currentRewardView.Model.IsCurrency);
                        Debug.Log(_currentRewardView.GetRewardImage().sprite);
                    }
                }

                else if (rewardView.Model.IsExplosive)
                {               
                    rewardView.Model.IsExplosive = false;
                    rewardView.GetRewardImage().sprite = _currentRewardView.GetRewardImage().sprite;
                    rewardView.Model.RewardAmount = _currentRewardView.GetRewardAmount();
                    rewardView.Model.IsCurrency = _currentRewardView.Model.IsCurrency;
                    rewardView.Model.RewardType = _currentRewardView.GetRewardType();
                }

            }
        }

    }
}
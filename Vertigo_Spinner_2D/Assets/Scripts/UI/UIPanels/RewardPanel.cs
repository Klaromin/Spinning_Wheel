using System;
using System.Collections.Generic;
using UnityEngine;
using VertigoDemo.Data;
using VertigoDemo.Managers;
using VertigoDemo.UI.RewardMVC;

namespace VertigoDemo.UI.UIPanels
{
    public class RewardPanel : MonoBehaviour
    {
        [SerializeField] private List<RewardView> _rewardViews;
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
        }
    
        private void RemoveEvents()
        {
            GameManager.Instance.OnRewardDecidedEvent -= OnRewardDecided;
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
        }

    }
}
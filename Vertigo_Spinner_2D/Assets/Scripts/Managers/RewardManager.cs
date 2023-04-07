using System;
using VertigoDemo.Helpers;
using VertigoDemo.UI.RewardMVC;

namespace VertigoDemo.Managers
{
    public class RewardManager : Singleton<RewardManager>
    {
        public event EventHandler<bool> OnRewardSelectedEvent;
    
        private RewardView _selectedReward;

        public RewardView GetSelectedReward()
        {
            return _selectedReward;
        }
    
        public void SetSelectedReward(RewardView reward)
        {
            _selectedReward = reward;
            OnRewardSelectedEvent?.Invoke(this, reward.Model.IsExplosive);
        }
    }
}



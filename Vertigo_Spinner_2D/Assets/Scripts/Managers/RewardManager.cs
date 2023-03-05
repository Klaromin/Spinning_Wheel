using System;
using UnityEngine;

public class RewardManager : MonoBehaviour
{

    public event EventHandler<bool> OnRewardSelectedEvent;
    public static RewardManager Instance { get; private set; }
    private RewardView _selectedReward;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one RewardView Manager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }



    public RewardView GetSelectedReward()
    {
        return _selectedReward;
    }
    
    public void SetSelectedReward(RewardView reward)
    {
        _selectedReward = reward;
         OnRewardSelectedEvent?.Invoke(this, reward.Model.IsBoom);
    }
}



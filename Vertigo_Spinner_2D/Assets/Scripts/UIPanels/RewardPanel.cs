using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

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
    
    private void AddEvents()
    {
        GameManager.Instance.OnRewardDecidedEvent += OnRewardDecided;
        GameManager.Instance.OnBronzeSpinEvent += OnBronzeSpin;

    }

    private void OnBronzeSpin(object sender, EventArgs e)
    {
        //
        // if (_rewardViews.Where(c => c.Model.IsBoom == false).ToList().Count == 0)
        // {
        //     
        // }
        //
        //
        // var rw = _rewardViews[Random.Range(0, _rewardViews.Count)];
        // rw.Model.IsBoom = Resources.Load<RewardDataTemplate>("Data/Reward Datas/Boom").IsBoom;
    }
}
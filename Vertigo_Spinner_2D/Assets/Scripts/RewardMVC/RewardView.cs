using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Random = UnityEngine.Random;

public class RewardView : BaseView<RewardModel>
{
    [SerializeField] private RewardDataTemplate _rewardDataTemplate;
    [SerializeField] private Image _rewardImage;
    [SerializeField] private TextMeshProUGUI _rewardAmount;
    [SerializeField] private int _amount;
    [SerializeField] private Transform _rewardEarned;
    [SerializeField] private Transform _selectedRewardEndPoint;

    private void OnValidate()
    {
        Init();
    }

    private void Start()
    {
        AddEvents();
        SetInitialData();
        UpdateAmount();
    }

    private void OnDisable()
    {
        RemoveEvents();
        DeInit();
    }

    public override void Init()
    {
        InitRewardImage();
        InitAmount();
    }

    public override void DeInit()
    {
        DeInitRewardImage();
        DeInitAmount();
    }

    private void InitRewardImage()
    {
        _rewardImage.sprite = _rewardDataTemplate.RewardImage;
    }

    private void InitAmount()
    {
        _rewardAmount.text = _rewardDataTemplate.RewardAmount.ToString();
    }
    
    private void DeInitRewardImage()
    {
        _rewardImage.sprite = null;
    }

    private void DeInitAmount()
    {
        _rewardAmount.text = null;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("boom");
        RewardManager.Instance.SetSelectedReward(this);
    }
    
    private void SetInitialData()
    {
         Model.RewardAmount = _rewardDataTemplate.RewardAmount;
         Model.IsBoom = _rewardDataTemplate.IsBoom;
        Model.RewardType = _rewardDataTemplate.RewardType;
         Model.RewardID = _rewardDataTemplate.RewardID;
        // Model.RewardData.RewardAmount = _rewardDataTemplate.RewardAmount;
        // Model.RewardData.IsBoom = _rewardDataTemplate.IsBoom;
        // Model.RewardData.RewardType = _rewardDataTemplate.RewardType;
        // Model.RewardData.RewardID = _rewardDataTemplate.RewardID;

    }

    private void UpdateAmount()
    {
        _rewardAmount.text = Model.RewardAmount.ToString();
    }

    private void OnSuccessfulSpin()
    {
        if (Model.RewardType is RewardType.C4 or RewardType.HealthShot or RewardType.MedKit or RewardType.EMP)
        {
            Model.RewardAmount++;
        }
        
        if (Model.RewardType is RewardType.Gold or RewardType.Money)
        {
            Model.RewardAmount += _rewardDataTemplate.RewardAmount;
        }
    }

    private void OnUnsuccessfulSpin()
    {
        Model.RewardAmount = _rewardDataTemplate.RewardAmount;
    }

    public int GetRewardAmount()
    {
        return Model.RewardAmount;
    }

    private void OnSuccessfulSpin(object sender, EventArgs e)
    {
        OnSuccessfulSpin();
        UpdateAmount();
        Debug.Log("Reward amountları güncellendi.");
    }

    public void RewardCollection()
    {
        Sprite rewardImage = _rewardDataTemplate.RewardImage;
        _rewardEarned.GetComponentInChildren<Image>().sprite = rewardImage;
        var go = Instantiate(_rewardEarned, transform);
        go.DOMove(_selectedRewardEndPoint.position, 1f).OnComplete(() => Destroy(go.gameObject));
    }
    
    private void OnRewardDecided(object sender, EventArgs e)
    {
        if (GameManager.Instance.State == GameState.GameOver)
        {
            OnUnsuccessfulSpin();
        }
    }


    private void OnSuperSpinReached(object sender, EventArgs e)
    {
        RewardDataTemplate safeReward = Configuration.RewardData.AllSafeRewardData[
            Random.Range(0, Configuration.RewardData.AllSafeRewardData.Count)];
        if (Model.IsBoom)
        {
            _rewardImage.sprite = safeReward.RewardImage;
        }
    }

    private void OnSilverSpinReached(object sender, EventArgs e)
    {
        RewardDataTemplate safeReward = Configuration.RewardData.AllSafeRewardData[
            Random.Range(0, Configuration.RewardData.AllSafeRewardData.Count)];
        if (Model.IsBoom)
        {
            _rewardImage.sprite = safeReward.RewardImage;
            _rewardAmount.text = safeReward.RewardAmount.ToString();
        }
    }
    

    
    private void AddEvents()
    {
        GameManager.Instance.OnSuccessfulSpinEvent += OnSuccessfulSpin;
        GameManager.Instance.OnRewardDecidedEvent += OnRewardDecided;
        GameManager.Instance.OnSilverSpinReachedEvent += OnSilverSpinReached;
        GameManager.Instance.OnSuperSpinReachedEvent += OnSuperSpinReached;

    }



    private void RemoveEvents()
    {
        GameManager.Instance.OnSuccessfulSpinEvent -= OnSuccessfulSpin;
        GameManager.Instance.OnRewardDecidedEvent -= OnRewardDecided;
    }


}
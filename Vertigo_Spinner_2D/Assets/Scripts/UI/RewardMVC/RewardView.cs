using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VertigoDemo.Data;
using VertigoDemo.Helpers.BaseMVC;
using VertigoDemo.Managers;


namespace VertigoDemo.UI.RewardMVC
{
    public class RewardView : BaseView<RewardModel>
    {
        [SerializeField] private RewardDataTemplate _rewardDataTemplate;
        [SerializeField] private Image _rewardImage;
        [SerializeField] private TextMeshProUGUI _rewardAmount;
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
    
        private void UpdateAmount()
        {
            _rewardAmount.text = Model.RewardAmount.ToString();
        }

        private void SetInitialData()
        {
            Model.RewardAmount = _rewardDataTemplate.RewardAmount;
            Model.IsExplosive = _rewardDataTemplate.IsExplosive;
            Model.IsCurrency = _rewardDataTemplate.IsCurrency;
            Model.RewardType = _rewardDataTemplate.RewardType;
        }

        private void ArrangeRewardAmount()
        {
            if (Model.IsExplosive)
            {
                return;
            }
            if (!Model.IsCurrency)
            {
                Model.RewardAmount++;
            }
            else
            {
                Model.RewardAmount += _rewardDataTemplate.RewardAmount;
            }
        }

        public int GetRewardAmount()
        {
            return Model.RewardAmount;
        }

        public Image GetRewardImage()
        {
            return _rewardImage;
        }

        public RewardType GetRewardType()
        {
            return Model.RewardType;
        }

        public void RewardCollection()
        {
            _rewardEarned.GetComponentInChildren<Image>().sprite = GetRewardImage().sprite;
            var go = Instantiate(_rewardEarned, transform);
            go.DOMove(_selectedRewardEndPoint.position, 1f).OnComplete(() => Destroy(go.gameObject));
        }
   
        private void AddEvents()
        {
            GameManager.Instance.OnSuccessfulSpinEvent += OnSuccessfulSpin;
            GameManager.Instance.OnRewardDecidedEvent += OnRewardDecided;
        }

        private void RemoveEvents()
        {
            GameManager.Instance.OnSuccessfulSpinEvent -= OnSuccessfulSpin;
            GameManager.Instance.OnRewardDecidedEvent -= OnRewardDecided;
        }
    
        private void OnTriggerEnter2D(Collider2D col)
        {
            RewardManager.Instance.SetSelectedReward(this);
        }
    
        private void OnSuccessfulSpin(object sender, EventArgs e)
        {
            ArrangeRewardAmount();
            UpdateAmount();
        }

        private void OnRewardDecided(object sender, EventArgs e)
        {
            if (GameManager.Instance.State == GameState.GameOver)
            {
                OnUnsuccessfulSpin();
            }
        }

        private void OnUnsuccessfulSpin()
        {
            Model.RewardAmount = _rewardDataTemplate.RewardAmount;
            Model.IsExplosive = _rewardDataTemplate.IsExplosive;
            Model.IsCurrency = _rewardDataTemplate.IsCurrency;
        }
    }
}
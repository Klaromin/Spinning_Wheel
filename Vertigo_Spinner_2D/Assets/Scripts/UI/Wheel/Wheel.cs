using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using VertigoDemo.Data;
using VertigoDemo.Managers;
using Random = UnityEngine.Random;

namespace VertigoDemo.UI.Wheel
{
    public class Wheel : MonoBehaviour
    {
        [SerializeField] private Button _spinButton;
        [SerializeField] private GameObject _wheel;
        [SerializeField] private Collider2D _indicatorCollider;
        [SerializeField] private Image _indicatorImage;
        [SerializeField] private Image _wheelImage;
        [SerializeField] private Configuration _configuration;

        private void Start()
        {
            AddEvents();
            DisableCollision();
        }
    
        private void OnDisable()
        {
            RemoveEvents();
        }

        private void Update()
        {
            if(GameManager.Instance.State == GameState.Decision)
            {
                EnableCollision();
            }
            else
            {
                DisableCollision();
            }
        }

        private void Rotate()
        {
            float fullSpin = 360f;
            var randomAngle = Random.Range(1, 9) * 45f;
            Vector3 rotateVector = new Vector3(0, 0, fullSpin+randomAngle);
            var duration = 2.5f;
            _wheel.transform.DORotate(rotateVector, duration, RotateMode.WorldAxisAdd).SetEase(Ease.OutBack);
        }

        private bool IsSpinAllowed()
        {
            return GameManager.Instance.State == GameState.Start;
        }

        private void InitSilverWheelImage()
        { 
            Sprite silverSpinSprite = Resources.Load<Sprite>("Art/vertigo_games_game_dev_demo_sprites/ui_spins/UI_spin_silver_base");
            Sprite silverIndicatorSprite = Resources.Load<Sprite>("Art/vertigo_games_game_dev_demo_sprites/ui_spins/UI_spin_silver_indicator");
            _wheelImage.sprite = _configuration.SilverSpinSprite;
            _indicatorImage.sprite = _configuration.SilverIndicatorSprite;
        }

        private void InitSuperWheelImage()
        {
            Sprite superSpinSprite = Resources.Load<Sprite>("Art/vertigo_games_game_dev_demo_sprites/ui_spins/UI_spin_golden_base");
            Sprite superIndicatorSprite = Resources.Load<Sprite>("Art/vertigo_games_game_dev_demo_sprites/ui_spins/UI_spin_golden_indicator");
            _wheelImage.sprite = _configuration.SuperSpinSprite;
            _indicatorImage.sprite = _configuration.SuperIndicatorSprite;
        }

        private void InitBronzeWheelImage()
        {
            Sprite bronzeSpinSprite = Resources.Load<Sprite>("Art/vertigo_games_game_dev_demo_sprites/ui_spins/UI_spin_bronze_base");
            Sprite bronzeIndicatorSprite = Resources.Load<Sprite>("Art/vertigo_games_game_dev_demo_sprites/ui_spins/UI_spin_bronze_indicator");
            _wheelImage.sprite = _configuration.BronzeSpinSprite;
            _indicatorImage.sprite = _configuration.BronzeIndicatorSprite;
        }

        private void EnableCollision()
        {
            _indicatorCollider.enabled = true;
        }
    
        private void DisableCollision()
        {
            _indicatorCollider.enabled = false;
        }
    
        private void AddEvents()
        {
            _spinButton.onClick.AddListener(OnSpinButtonClicked);
            GameManager.Instance.OnSilverSpinReachedEvent += OnSilverSpinReached;
            GameManager.Instance.OnSuperSpinReachedEvent += OnSuperSpinReached;
            GameManager.Instance.OnBronzeSpinEvent += OnBronzeSpin;
        }
    
        private void RemoveEvents()
        {
            _spinButton.onClick.RemoveAllListeners();
            GameManager.Instance.OnSilverSpinReachedEvent -= OnSilverSpinReached;
            GameManager.Instance.OnSuperSpinReachedEvent -= OnSuperSpinReached;
            GameManager.Instance.OnBronzeSpinEvent -= OnBronzeSpin;
        }
    
        private void OnSpinButtonClicked()
        {
            if (!IsSpinAllowed()) return;
            Rotate();
            GameManager.Instance.UpdateGameState(GameState.Spin);
        }

        private void OnBronzeSpin(object sender, EventArgs e)
        {
            InitBronzeWheelImage();
        }

        private void OnSuperSpinReached(object sender, EventArgs e)
        {
            InitSuperWheelImage();
        }

        private void OnSilverSpinReached(object sender, EventArgs e)
        {
            InitSilverWheelImage();
        }

    }
}
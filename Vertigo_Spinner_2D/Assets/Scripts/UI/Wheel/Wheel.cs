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

        private void Rotate() //Burayı düzelt.
        {
            var angleArray = new[] { 360, 405, 450, 495, 540, 585, 630, 675, 720, 765, 810, 855, 900 };
            var randomAngle = angleArray[Random.Range(0,angleArray.Length)];
            Vector3 rotateVector = new Vector3(0, 0, randomAngle);
            var duration = 2.5f;
            _wheel.transform.DORotate(rotateVector, duration, RotateMode.WorldAxisAdd).SetEase(Ease.OutBack);
        }

        private bool IsSpinAllowed()
        {
            return GameManager.Instance.State == GameState.Start;
        }

        private void InitSilverWheelImage()
        {
            _wheelImage.sprite = Configuration.SpinnerSprites.SilverSpinSprite;
            _indicatorImage.sprite = Configuration.SpinnerSprites.SilverIndicatorSprite;
        }

        private void InitSuperWheelImage()
        {
            _wheelImage.sprite = Configuration.SpinnerSprites.SuperSpinSprite;
            _indicatorImage.sprite = Configuration.SpinnerSprites.SuperIndicatorSprite;
        }

        private void InitBronzeWheelImage()
        {
            _wheelImage.sprite = Configuration.SpinnerSprites.BronzeSpinSprite;
            _indicatorImage.sprite = Configuration.SpinnerSprites.BronzeIndicatorSprite;
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
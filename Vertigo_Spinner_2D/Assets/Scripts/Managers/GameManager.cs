using System;
using UnityEngine;
using VertigoDemo.Data;
using VertigoDemo.Helpers;
using Task = System.Threading.Tasks.Task;


namespace VertigoDemo.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public event EventHandler OnRewardDecidedEvent;
        public event EventHandler OnSuccessfulSpinEvent;
        public event EventHandler OnSpinCompleteEvent;
        public event EventHandler OnBronzeSpinEvent;
        public event EventHandler OnSilverSpinReachedEvent;
        public event EventHandler OnSuperSpinReachedEvent;
    
        [SerializeField] private GameState _state;
        public GameState State => _state;
    
        private bool _isExplosive;
        private int _successfulSpinCounter;
        public int SuccessfulSpinCounter => _successfulSpinCounter;

        private void Start()
        {
            _successfulSpinCounter = 0;
            AddEvent();
        }

        private void OnDisable()
        {
            RemoveEvent();
        }

        public void UpdateGameState(GameState newState)
        {
            _state = newState;
            switch (newState)
            {
                case GameState.Start:
                    HandleStart();
                    break;
                case GameState.Spin:
                    HandleSpin();
                    break;
                case GameState.Decision:
                    HandleDecision();
                    break;
                case GameState.Reward:
                    HandleReward();
                    break;
                case GameState.GameOver:
                    HandleGameOver();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private void HandleStart()
        {
            OnSpinCompleteEvent?.Invoke(this, EventArgs.Empty);
        }
    
        private async void  HandleSpin()
        {
            await Task.Delay(3000);
            UpdateGameState(GameState.Decision);
        }
    
        private async void HandleDecision()
        {
            await Task.Delay(1000);
            if (_isExplosive)
            {
                UpdateGameState(GameState.GameOver);
            }

            if (!_isExplosive)
            {
                UpdateGameState(GameState.Reward);
            }
        }
    
        private async void HandleGameOver()
        {
            _successfulSpinCounter = 0;
            OnRewardDecidedEvent?.Invoke(this, EventArgs.Empty);
            await Task.Delay(1000);
            UpdateGameState(GameState.Start);
        }

        private async void HandleReward()
        {
            _successfulSpinCounter++;
            HandleSpecialSpins();
            OnRewardDecidedEvent?.Invoke(this, EventArgs.Empty);
            OnSuccessfulSpinEvent?.Invoke(this, EventArgs.Empty);
            await Task.Delay(1000);
            UpdateGameState(GameState.Start);
        }

        private void HandleSpecialSpins()
        {
            if (SuccessfulSpinCounter % 5 == 0 && SuccessfulSpinCounter % 30 != 0 && SuccessfulSpinCounter !=0)
            {
                OnSilverSpinReachedEvent?.Invoke(this, EventArgs.Empty);
            }
            if (SuccessfulSpinCounter % 30 == 0 && SuccessfulSpinCounter !=0)
            {
                OnSuperSpinReachedEvent?.Invoke(this, EventArgs.Empty);
            }
            if((SuccessfulSpinCounter % 5 != 0 && SuccessfulSpinCounter % 30 != 0))
            {
                OnBronzeSpinEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        private void AddEvent()
        {
            RewardManager.Instance.OnRewardSelectedEvent += OnRewardSelected;
        }

        private void RemoveEvent()
        {
            RewardManager.Instance.OnRewardSelectedEvent -= OnRewardSelected;
        }

        private void OnRewardSelected(object sender, bool IsSelectedRewardExplosive)
        {
            _isExplosive = IsSelectedRewardExplosive;
        }
    }
}

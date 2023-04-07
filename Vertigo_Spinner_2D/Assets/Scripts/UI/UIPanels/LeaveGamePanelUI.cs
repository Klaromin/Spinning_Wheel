using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VertigoDemo.Data;
using VertigoDemo.Managers;

namespace VertigoDemo.UI.UIPanels
{
    public class LeaveGamePanelUI : MonoBehaviour
    {
        [SerializeField] private Button _leaveGameButton;

        private void Start()
        {
            AddEvents();
        }

        private void OnDisable()
        {
            RemoveEvents();
        }

        private void LeaveGame()
        {
            if (IsQuitAvailable())
            {
                SceneManager.LoadScene("MenuScene");
            }
        }

        private bool IsQuitAvailable()
        {
            return GameManager.Instance.State == GameState.Start;
        }
    
        private void AddEvents()
        {
            _leaveGameButton.onClick.AddListener(LeaveGame);
        }
    
        private void RemoveEvents()
        {
            _leaveGameButton.onClick.RemoveAllListeners();
        }
    }
}

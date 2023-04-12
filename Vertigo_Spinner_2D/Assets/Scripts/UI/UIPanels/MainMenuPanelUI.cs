using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace VertigoDemo.UI.UIPanels
{
    public class MainMenuPanelUI : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;

        private void Start()
        {
            AddEvents();
        }

        private void OnDisable()
        {
            RemoveEvents(); 
        }

        private void Play()
        {
            SceneManager.LoadScene("GameScene");
        }
    
        private void Quit()
        {
            Application.Quit();
        }

        private void AddEvents()
        {
            _playButton.onClick.AddListener(Play);
            _quitButton.onClick.AddListener(Quit);
        }

        private void RemoveEvents()
        {
            _playButton.onClick.RemoveAllListeners();
            _quitButton.onClick.RemoveAllListeners();
        }
    }
}

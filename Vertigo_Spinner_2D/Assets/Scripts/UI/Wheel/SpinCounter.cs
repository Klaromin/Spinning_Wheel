using System;
using TMPro;
using UnityEngine;
using VertigoDemo.Managers;

namespace VertigoDemo.UI.Wheel
{
    public class SpinCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _succesfullSpinCounter;

        private void Start()
        {
            AddEvents();
        }

        private void OnDisable()
        {
            RemoveEvents();
        }

        private void AddEvents()
        {
            GameManager.Instance.OnSpinCompleteEvent += OnSpinComplete;
        }
    
        private void RemoveEvents()
        {
            GameManager.Instance.OnSpinCompleteEvent -= OnSpinComplete;
        }

        private void OnSpinComplete(object sender, EventArgs e)
        {
            _succesfullSpinCounter.text = "Succesfull Spin Count: "  +GameManager.Instance.SuccessfulSpinCounter;
        }
    }
}

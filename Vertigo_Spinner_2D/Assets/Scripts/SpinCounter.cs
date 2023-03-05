using System;
using TMPro;
using UnityEngine;

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

    private void OnSpinComplete(object sender, EventArgs e)
    {
        _succesfullSpinCounter.text = "Succesfull Spin Count: "  +GameManager.Instance.SuccessfulSpinCounter;
    }
    private void AddEvents()
    {
        GameManager.Instance.OnSpinCompleteEvent += OnSpinComplete;
    }
    
    private void RemoveEvents()
    {
        GameManager.Instance.OnSpinCompleteEvent -= OnSpinComplete;
    }


}

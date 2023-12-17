using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class CurrencyBallance : MonoBehaviour {
    public TextMeshProUGUI textMeshPor;
    private void Start()
    {
        UpdateBalance();
        CurrencyController.onBalanceChanged += OnBalanceChanged;
        textMeshPor = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void UpdateBalance()
    {
        textMeshPor.SetText(CurrencyController.GetBalance().ToString());
    }

    private void OnBalanceChanged()
    {
        UpdateBalance();
    }

    private void OnDestroy()
    {
        CurrencyController.onBalanceChanged -= OnBalanceChanged;
    }
}

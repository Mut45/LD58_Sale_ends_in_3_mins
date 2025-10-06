using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }
    public float BalanceInCents { get; private set; }
    public Action<float> OnBalanceChanged; // when the balance is changed
    public Action OnDepleted; // when the balance is depleted

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void Init(int amount)
    {
        BalanceInCents = Math.Max(0, amount);
        OnBalanceChanged?.Invoke(BalanceInCents);
        if (BalanceInCents == 0)
        {
            OnDepleted?.Invoke();
        }
    }
    public bool Spend(float amount)
    {
        if (amount > BalanceInCents)
        {
            return false;
        }
        else
        {
            BalanceInCents -= amount;
            OnBalanceChanged?.Invoke(BalanceInCents);
            CurrencyDisplay.Instance.UpdateTextBalance();
            if (BalanceInCents <= 0) {
                BalanceInCents = 0;
                OnDepleted?.Invoke();
            }
            return true;
        }
    }

    
}

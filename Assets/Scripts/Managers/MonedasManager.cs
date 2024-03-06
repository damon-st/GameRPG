using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedasManager : Singleton<MonedasManager>
{
    [SerializeField] private int coinsTest;

    public int CointsTotals { get; set; }

    private string KEY_COIN = "MYJUEGO_COIND";


    private void Start()
    {
        //remove in produciton
        LoadCoins();
    }

    private void LoadCoins()
    {
        CointsTotals = PlayerPrefs.GetInt(KEY_COIN, coinsTest);
    }


    public void AddCoins(int amount)
    {
        CointsTotals += amount;
        SaveCoints();
    }

    public void RemoveCoins(int amount)
    {
        if (amount > CointsTotals) return;
        CointsTotals -= amount;
        SaveCoints();
    }

    private void SaveCoints()
    {
        PlayerPrefs.SetInt(KEY_COIN, CointsTotals);
        PlayerPrefs.Save();
    }
}

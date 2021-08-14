using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneLogic : MonoBehaviour
{
    [SerializeField] private CoinsGenerator _coinsGenerator;
    [SerializeField] private LevelUI _levelUI;

    private int _collectedCoins;
    private List<Coin> _coins;

    private void Start()
    {
        _coins = _coinsGenerator.Generate();
        _levelUI.SetCoinsText(_collectedCoins, _coins.Count);
        _coins.ForEach(coin => coin.Collected += OnCoinCollected);
        FindObjectOfType<UnknownScript>().Triggered += () => Win(true);
    }

    private void OnCoinCollected()
    {
        _collectedCoins++;
        _levelUI.SetCoinsText(_collectedCoins, _coins.Count);
        if (_collectedCoins == _coins.Count)
        {
            Win(false);
        }
    }

    private void Win(bool trueEnd)
    {
        _levelUI.ShowWinScreen(trueEnd);
    }
}
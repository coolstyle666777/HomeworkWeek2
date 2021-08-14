using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private int _coinsCount;
    [SerializeField] private float _minOffset;

    public List<Coin> Generate()
    {
        List<Coin> coins = new List<Coin>();
        for (int i = 0; i < _coinsCount; i++)
        {
            GameObject coin = Instantiate(_coinPrefab,
                new Vector3(Random.Range(-45f, 45f), 0.5f, Random.Range(-45f, 45f)),
                Quaternion.identity);
            if (Mathf.Abs(coin.transform.position.x) < _minOffset)
            {
                coin.transform.Translate(new Vector3(coin.transform.position.x +
                                                     (coin.transform.position.x > 0 ? 1 : -1) * _minOffset, 0, 0));
            }

            if (Mathf.Abs(coin.transform.position.z) < _minOffset)
            {
                coin.transform.Translate(new Vector3(0, 0, coin.transform.position.z +
                                                           (coin.transform.position.z > 0 ? 1 : -1) * _minOffset));
            }

            coins.Add(coin.GetComponentInChildren<Coin>());
        }

        return coins;
    }
}
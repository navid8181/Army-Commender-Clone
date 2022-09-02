using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OptimizeCoinChest : DistributionBase
{

    [SerializeField] private float yOffset = 0.07f;

    [SerializeField] public float moveSpeed = 2.0f;

    public float minSpeed = 2.0f, maxSpeed = 10;

    [SerializeField] public float coineLenght = 2;
    [SerializeField] public AnimationCurve animationCurve;

    public JoyStick joyStick;

    private List<int> goldCoinIndexes, ironCoinIndexes;


    private void Awake()
    {
        goldCoinIndexes = new List<int>();
        ironCoinIndexes = new List<int>();
    }
    public override void ExeCuteDistribute(int i)
    {
        if (IsUpdatingIndex) return;
        CoinDistribut(GetDistributables(), i);
    }


    public void SetIndexes(Coin coin)
    {
        CoinType coinType = coin.coinType;

        if (coinType == CoinType.goldCoin)
            goldCoinIndexes.Add(coin.DistributIndex);
        else
            ironCoinIndexes.Add(coin.DistributIndex);
    }


    public IDistributable RemoveGoldCoin()
    {
        if (goldCoinIndexes.Count <= 0) return null;

        int goldIndex = goldCoinIndexes[goldCoinIndexes.Count - 1];

        goldCoinIndexes.RemoveAt(goldCoinIndexes.Count - 1);

        IDistributable distributable = GetDistributables()[goldIndex];
        RemoveDistribut(distributable);

        return distributable;
    }

    public IDistributable RemoveIronCoin()
    {
        if (ironCoinIndexes.Count <= 0) return null;

        int ironIndex = ironCoinIndexes[ironCoinIndexes.Count - 1];

        ironCoinIndexes.RemoveAt(ironCoinIndexes.Count - 1);

        IDistributable distributable = GetDistributables()[ironIndex];
        RemoveDistribut(distributable);

        return distributable;
    }

    public void CoinDistribut(IDistributable[] objectDistribut, int index)
    {



        coineLenght = (objectDistribut.Length / 20.0f) + 0.1f;
        coineLenght = Mathf.Clamp01(coineLenght);

        // coineLenght *= 0.5f;

        float velocity = joyStick.getInput().magnitude;

        if (velocity >= 1) velocity = 1;

        moveSpeed = Mathf.Lerp(maxSpeed, minSpeed, velocity);


        Vector3 dire = joyStick.getInput();

        GameObject coin = ((Coin)objectDistribut[index]).gameObject;



        Vector3 currentCoinPos = coin.transform.position;
        currentCoinPos.y = transform.position.y + yOffset * index;



        float rad = ((index) / (float)objectDistribut.Length);





        currentCoinPos.x = Mathf.Lerp(currentCoinPos.x, transform.position.x + dire.x * coineLenght * animationCurve.Evaluate(rad), Time.deltaTime * moveSpeed);
        currentCoinPos.z = Mathf.Lerp(currentCoinPos.z, transform.position.z + dire.y * coineLenght * animationCurve.Evaluate(rad), Time.deltaTime * moveSpeed);

        objectDistribut[index].SetTraget(currentCoinPos);


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OptimizeCoinChest : DistributionBase
{

    [SerializeField] private float yOffset = 0.07f;

    [SerializeField] public float moveSpeed = 2.0f;

    public float minSpeed = 2.0f,maxSpeed = 10;

    [SerializeField] public float coineLenght = 2;
    [SerializeField] public AnimationCurve animationCurve;

    public JoyStick joyStick;


    public override void ExeCuteDistribute(int i)
    {
        if (IsUpdatingIndex) return;
        CoinDistribut(GetDistributables(), i);
    }



    public void CoinDistribut(IDistributable[] objectDistribut, int index)
    {

        coineLenght = (objectDistribut.Length / 20.0f) + 0.1f;
        coineLenght = Mathf.Clamp01(coineLenght);

       // coineLenght *= 0.5f;

        float velocity = joyStick.getInput().magnitude;

        if (velocity >= 1) velocity = 1;

        moveSpeed = Mathf.Lerp(maxSpeed,minSpeed,velocity);


        Vector3 dire = joyStick.getInput();

        GameObject coin = ((Coin)objectDistribut[index]).gameObject;



        Vector3 currentCoinPos = coin.transform.position;
        currentCoinPos.y = transform.position.y + yOffset * index;



        float rad = ((index) / (float)objectDistribut.Length);


    


        currentCoinPos.x = Mathf.Lerp(currentCoinPos.x, transform.position.x + dire.x * coineLenght * animationCurve.Evaluate(rad), Time.deltaTime * moveSpeed);
        currentCoinPos.z = Mathf.Lerp(currentCoinPos.z, transform.position.z + dire.y * coineLenght * animationCurve.Evaluate(rad) , Time.deltaTime * moveSpeed);

        coin.transform.position = currentCoinPos;
    }

}

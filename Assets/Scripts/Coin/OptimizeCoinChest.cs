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




    private void Awake()
    {



    }




    public override void ExeCuteDistribute(int i)
    {
        if (IsUpdatingIndex) return;
        CoinDistribut(GetDistributables(), i);
    }





    public IDistributable RemoveGoldCoin()
    {
        IDistributable distributable = null;
        for (int i = 0; i < GetDistributables().Length; i++)
        {

            if (((Coin)GetDistributables()[i]).coinType == CoinType.goldCoin)
            {
                distributable = GetDistributables()[i];
                RemoveDistribut(distributable);
                break;
            }


        }

        return distributable;
    }

    public IDistributable RemoveIronCoin()
    {

        IDistributable distributable = null;
        for (int i = 0; i < GetDistributables().Length; i++)
        {

            if (((Coin)GetDistributables()[i]).coinType == CoinType.ironCoin)
            {
                distributable = GetDistributables()[i];
                RemoveDistribut(distributable);
                break;
            }

         
        }



        return distributable;
    }

    public void CoinDistribut(IDistributable[] objectDistribut, int index)
    {



        //coineLenght = (objectDistribut.Length / 20.0f) + 0.1f;
        // coineLenght = Mathf.Clamp01(coineLenght);



        // coineLenght *= 0.5f;

        float velocity = joyStick.getRawInput().magnitude;

        if (velocity >= 1) velocity = 1;

        //  moveSpeed = maxSpeed * velocity;

        //  moveSpeed = Mathf.Lerp(maxSpeed, minSpeed, velocity);


        Vector3 dire = joyStick.getInput();

        GameObject coin = ((Coin)objectDistribut[index]).gameObject;



        Vector3 currentCoinPos = coin.transform.position;
        currentCoinPos.y = transform.position.y + yOffset * index;



        float rad = ((index) / (float)objectDistribut.Length);





        currentCoinPos.x = transform.position.x + joyStick.getInput().x * coineLenght * animationCurve.Evaluate(rad) * Time.fixedDeltaTime * maxSpeed;
        currentCoinPos.z = transform.position.z + joyStick.getInput().y * coineLenght * animationCurve.Evaluate(rad) * Time.fixedDeltaTime * maxSpeed;

        objectDistribut[index].SetTraget(currentCoinPos);


    }

}

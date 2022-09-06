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


    private Vector3 targetToFolow;

    public Vector3 offset;
    Vector3 offsetDireMove;
    private void Awake()
    {
        Debug.Log(transform.parent);


    }


    private void Update()
    {
        targetToFolow = transform.parent.TransformPoint(transform.localPosition) + offsetDireMove;

        // if (offset .magnitude > 0)
        // {
        Transform playerPos = transform.root;

        Vector3 playerfwd = playerPos.forward;

        playerfwd.y = 0;

        Vector3 playerrgh = playerPos.right;

        playerrgh.y = 0;

        offsetDireMove = playerfwd * offset.z + playerrgh * offset.x;

        //offsetDireMove.Normalize();

        offset = Vector3.Lerp(offset, Vector3.zero, moveSpeed * Time.deltaTime * 0.1f);
        // }


    }

    public override void ExeCuteDistribute(int i)
    {


        if (IsUpdatingIndex) return;
        CoinDistribut(GetDistributables(), i);
    }





    public IDistributable RemoveGoldCoin()
    {
        IDistributable distributable = null;
        for (int i = GetDistributables().Length - 1; i >= 0; i--)
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
        for (int i = GetDistributables().Length - 1; i >= 0; i--)
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
        currentCoinPos.y = targetToFolow.y + yOffset * index;



        float rad = ((index) / (float)objectDistribut.Length);





        //currentCoinPos.x = transform.position.x + joyStick.getInput().x * coineLenght * animationCurve.Evaluate(rad) * Time.fixedDeltaTime * maxSpeed;
        // currentCoinPos.z = transform.position.z + joyStick.getInput().y * coineLenght * animationCurve.Evaluate(rad) * Time.fixedDeltaTime * maxSpeed;

        currentCoinPos.x = targetToFolow.x;
        currentCoinPos.z = targetToFolow.z;

        objectDistribut[index].SetTraget(currentCoinPos);


    }

}

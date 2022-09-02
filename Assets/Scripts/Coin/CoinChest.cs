using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinChest : MonoBehaviour
{
    [SerializeField] private float yOffset = 1;

    public List<Coin> coins;


    public AnimationCurve coinMove;

    public AnimationCurve TcoinMove;



    public Vector3 CoinDirection;

    [Range(0.0f, 2.0f)] public float xDamp = 0;
    [Range(0.0f, 2.0f)] public float zDamp = 0;

    [Range(0, 1)] public float lenght = 1;


    public float speed = 2;

    public float idleSpeed = 0.6f;

    [SerializeField] private float maxSpeed = 60, minSpeed = 10;


    float time = 0;


    float xSleep, zSleep;
    public float lenghtSlips = 0.5f;







    private void Awake()
    {
        coins = new List<Coin>();

    }


    public JoyStick jj;

    public void AddCoin(Coin instanceCoin)
    {


        // Instantiate(coinPrefab, transform.position, coinPrefab.transform.rotation) as GameObject;

        if (instanceCoin == null) return;

       // coinPrefab.transform.position = transform.position;

        coins.Add(instanceCoin);

    }

    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {


        lenght = (coins.Count / 20.0f) + 0.1f;
        lenght = Mathf.Clamp01(lenght);

        lenght *= 0.5f;


        if (jj.getRawInput().magnitude >= 0.01f)
        {

            CoinDirection.x = jj.getInput().x * lenght;

            CoinDirection.z = jj.getInput().y * lenght;

            xSleep = -CoinDirection.x * lenghtSlips;

            zSleep = -CoinDirection.z * lenghtSlips;


            time = 1;
        }
        else
        {

            if (time > 0) time -= Time.deltaTime * idleSpeed;

            if (time < 0) time = 0;

            CoinDirection.x = Mathf.Lerp(CoinDirection.x, Mathf.Lerp(0, xSleep, TcoinMove.Evaluate(1 - time)), 1 - time);
            CoinDirection.z = Mathf.Lerp(CoinDirection.z, Mathf.Lerp(0, zSleep, TcoinMove.Evaluate(1 - time)), 1 - time);
        }



       
        float velocity = (CoinDirection.magnitude > 1) ? CoinDirection.normalized.magnitude : CoinDirection.magnitude;
        for (int i = coins.Count - 1; i >= 0; i--)
        {

            Coin coin = coins[i];

            Vector3 currentCoinPos = coin.transform.position;
            currentCoinPos.y = transform.position.y + yOffset * i;


            // print("velocirty ; " + velocity);
            speed = Mathf.Lerp(maxSpeed, minSpeed, velocity);



            currentCoinPos.x = Mathf.Lerp(currentCoinPos.x,  transform.position.x + CoinDirection.x * coinMove.Evaluate(((i) / (float)(coins.Count))), speed * Time.deltaTime);

            currentCoinPos.z = Mathf.Lerp(currentCoinPos.z, transform.position.z + CoinDirection.z * coinMove.Evaluate(((i) / (float)(coins.Count))), speed * Time.deltaTime);


            coin.transform.position = currentCoinPos;
        }
    }
}

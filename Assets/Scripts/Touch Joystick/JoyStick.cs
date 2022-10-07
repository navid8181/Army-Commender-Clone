using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [SerializeField] private Canvas canvas;
    [SerializeField] Image imageBackGround;
    private RectTransform rectTransform;

    [SerializeField] private float radius = 60;


     private Vector2 input,rawInput;

    private Vector2 startPos;


    private Vector2 deltaPos,rawDeltaPos;


    public RectTransform RectTransform { get => rectTransform; set => rectTransform = value; }
    public Canvas Canvas { get => canvas; }

    private void Start()
    {
     
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {

        deltaPos = (Vector2)transform.position - startPos;
        rawDeltaPos = deltaPos;



        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;


        Vector2 rectPosTemp = rectTransform.transform.position;

        rectPosTemp.x = Mathf.Clamp(rectPosTemp.x, startPos.x - radius, startPos.x + radius);
        rectPosTemp.y = Mathf.Clamp(rectPosTemp.y, startPos.y - radius, startPos.y + radius);

        rectTransform.transform.position = rectPosTemp;


        input = deltaPos / (Vector2.one * radius);
        rawInput = rawDeltaPos / (Vector2.one * radius);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //deltaPos = Vector2.Lerp(deltaPos, Vector2.zero, 10 * Time.deltaTime);
        //  input = Vector2.Lerp(input, Vector2.zero, 10 * Time.deltaTime);

       // deltaPos = Vector2.zero;

       // input = Vector2.zero;

        StartCoroutine(fade());


        transform.position = startPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = transform.position;
        imageBackGround.transform.position = startPos;
    }

    IEnumerator fade()
    {
        while (deltaPos != Vector2.zero || input != Vector2.zero)
        {
            deltaPos = Vector2.Lerp(deltaPos, Vector2.zero, 2 * Time.deltaTime);
            input = Vector2.Lerp(input, Vector2.zero, 2 * Time.deltaTime);

            rawInput = Vector2.zero;
            rawDeltaPos = Vector2.zero;
            yield return null;
        }

    }

    public Vector2 getInput() => input;
    public Vector2 getRawInput() => rawInput;
    public Vector2 getNormalMagnitude() => input;


}

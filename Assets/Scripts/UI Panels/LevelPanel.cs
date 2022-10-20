using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelPanel : BasePanel
{
    public float duration = 3;

    private Vector3 lastPosition = Vector3.one;

    Vector3 currentPosition;


    private void Awake()
    {

        lastPosition = transform.localPosition;
        transform.DOLocalMoveX(1000, duration);

    }
    public override void OnEnter()
    {
        transform.DOLocalMoveX(lastPosition.x, duration);
    }

    public override void OnExit()
    {
        transform.DOLocalMoveX( 1000, duration);
  
    }

    public override void OnPause()
    {
        transform.DOLocalMoveX(1000, duration);
    }

    public override void OnResume()
    {
        gameObject.SetActive(true);
    }

    public override bool DisableCondition()
    {
        return transform.localPosition.x >= 1000;
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanel : BasePanel
{

    public float duration = 3;

    private Vector3 lastScale = Vector3.one;

    public StatusBar statusBar;

    private void Awake()
    {
        lastScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }
    public override void OnEnter()
    {
        transform.DOScale(lastScale, duration);
    }

    public override void OnExit()
    {
        transform.DOScale(Vector3.zero, duration);
    }

    public override void OnPause()
    {
        transform.DOScale(Vector3.zero, duration);
    }

    public override void OnResume()
    {

    }

    public override bool DisableCondition()
    {
        return transform.localScale.sqrMagnitude <= 0.01f * 0.01f;
    }
}

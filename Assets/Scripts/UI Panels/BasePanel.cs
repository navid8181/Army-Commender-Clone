using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{

    public bool DontdisableLastPanel;

    public PanelType  panelName;

    public abstract void OnEnter();

    public abstract void OnExit();

    public abstract void OnResume();

    public abstract void OnPause();



    public abstract bool DisableCondition();


    public virtual void Update()
    {

      bool isDisable =  DisableCondition() ? false : true;


        this.gameObject.SetActive(isDisable);


    }

}

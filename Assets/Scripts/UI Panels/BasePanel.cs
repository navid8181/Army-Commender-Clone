using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{

    public PanelType  panelName;

    public abstract void OnEnter();

    public abstract void OnExit();

    public abstract void OnResume();

    public abstract void OnPause();

}

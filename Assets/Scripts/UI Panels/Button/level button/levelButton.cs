using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class levelButton : MonoBehaviour
{
    private Button button;



    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

    }

    public void AddListener(UnityAction call)
    {
        button.onClick.AddListener(call);
    }


}

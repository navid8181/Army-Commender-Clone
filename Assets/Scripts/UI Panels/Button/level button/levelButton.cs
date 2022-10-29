using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class levelButton : MonoBehaviour
{
    private Button button;


    public TextMeshProUGUI GetButtonTextMesh()=> button.GetComponentInChildren <TextMeshProUGUI>();
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

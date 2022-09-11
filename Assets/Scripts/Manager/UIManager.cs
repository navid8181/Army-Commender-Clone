using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public Button cataputAttackButton;
    public void EnableCataputAttackButton()=> cataputAttackButton.gameObject.SetActive(true);
    public void DisableCataputAttackButton() => cataputAttackButton.gameObject.SetActive(false);
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextoAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText;

    public void EstablishText(float amount,Color color)
    {
        damageText.text = amount.ToString();
        damageText.color = color;
    }
}

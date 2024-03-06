using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemigoBarraVida : MonoBehaviour
{
    [SerializeField] private Image barraLife;


    private float healthActual;
    private float healthMax;

    // Update is called once per frame
    void Update()
    {
        barraLife.fillAmount=Mathf.Lerp(barraLife.fillAmount,healthActual/healthMax,10f*Time.deltaTime);
    }

    public void ModifyHeath(float pHealthActual, float pHealthMAX)
    {

        healthActual = pHealthActual;
        healthMax = pHealthMAX;
    }
}

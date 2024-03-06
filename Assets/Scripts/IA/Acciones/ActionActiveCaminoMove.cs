using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/ActivarCaminoMovimiento")]
public class ActionActiveCaminoMove : IAAccion
{
    public override void Execute(IAController controller)
    {
        if (controller.EnemigoMovimiento == null) return;
        controller.EnemigoMovimiento.enabled = true;
    }
}

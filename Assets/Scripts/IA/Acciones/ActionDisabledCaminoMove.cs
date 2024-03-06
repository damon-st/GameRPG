using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/DesactivarCaminoMovimiento")]
public class ActionDisabledCaminoMove : IAAccion
{
    public override void Execute(IAController controller)
    {
        if (controller.EnemigoMovimiento == null) return;
        controller.EnemigoMovimiento.enabled = false;
    }
}

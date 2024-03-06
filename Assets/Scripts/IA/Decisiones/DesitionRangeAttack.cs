using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Decisiones/PersonajeEnRangoDeAtaque")]
public class DesitionRangeAttack : IADecesion
{
    public override bool Decidir(IAController controller)
    {
        return InRangeAttack(controller);
    }

    private bool InRangeAttack(IAController controller)
    {
        if (controller.CharacterRef == null) return false;

        float distance = (controller.CharacterRef.position - controller.transform.position).sqrMagnitude;
        
        if(distance< Mathf.Pow(controller.RangeAttackDeterminado, 2))
        {
            return true;
        }
        return false;
    }
}

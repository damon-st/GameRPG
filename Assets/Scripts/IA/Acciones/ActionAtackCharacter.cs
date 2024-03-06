using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/AtacarPersonaje")]
public class ActionAtackCharacter : IAAccion
{
    public override void Execute(IAController controller)
    {
        Attack(controller);
    }

    private void Attack(IAController controller)
    {
        if (controller.CharacterRef == null) return;
        if (controller.CharacterRef.GetComponent<CharacterLife>().Defeated) return;
        if (!controller.IsTimeToAttack()) return;
        if (controller.CharacterInRangeAttack(controller.RangeAttackDeterminado))
        {
            // Attack
            if(controller.TypeAttack == TypesAttack.Embestida)
            {
                controller.AttackEmbestida(controller.Damage);
            }
            else
            {
                controller.AttackMelee(controller.Damage);
            }
            controller.UpdateTimewBetweenAttacks();
        }
    }
}

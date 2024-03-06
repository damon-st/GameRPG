using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/PocionVida")]
public class ItemPocionVida : InventarioItem
{
    [Header("Pocion Info")]
    public float HPRestauration;


    public override bool UseItem()
    {
        if (Inventario.Instance.Character.CharacterLife.CanBeCured)
        {
            Inventario.Instance.Character.CharacterLife.ResetHealth(HPRestauration);
            return true;
        }
        return false;
    }

    public override string DescriptionItemCrafting()
    {
        return $"Restura {HPRestauration} de Salud";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/PocionMana")]
public class ItemPocionMana : InventarioItem
{
    [Header("Pocion Info")]
    public float MPRestauration;

    public override bool UseItem()
    {
        if (Inventario.Instance.Character.CharacterMana.CanBeRestart)
        {
            Inventario.Instance.Character.CharacterMana.RestartManaAmout(MPRestauration);
            return true;
        }
        return false;
    }
}

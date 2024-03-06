using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Arma")]
public class ItemArma : InventarioItem
{
    [Header("Arma")]
    public Arma Weapon;

    public override bool ToEquipItem()
    {
        if(ContenedorArma.Instance.WeaponToEquip!=null)return false;
        ContenedorArma.Instance.EquipToWeapon(this);
        return true;
    }

    public override bool RemoveItem()
    {
        if(ContenedorArma.Instance.WeaponToEquip==null)return false;
        ContenedorArma.Instance.RemoveWeapon();
        return true;
    }

    public override bool UseItem()
    {
        return false;
    }

    public override string DescriptionItemCrafting()
    {
        return $"- Change Crititco: {Weapon.ChanceCritical}%\n"+
            $"- Change Bloqueo: {Weapon.ChanceBloking}%";
    }
}

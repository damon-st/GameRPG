using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContenedorArma : Singleton<ContenedorArma>
{
    [SerializeField] private Image weaponIcon;
    [SerializeField] private Image weaponSkillIcon;

    public ItemArma WeaponToEquip { get; set; }

    public void EquipToWeapon(ItemArma itemWepaon)
    {
        WeaponToEquip   = itemWepaon;
        weaponIcon.sprite = itemWepaon.Weapon.Weapon;
        weaponIcon.gameObject.SetActive(true);

        if(itemWepaon.Weapon.Type == TypeWaepon.Magic)
        {
            weaponSkillIcon.sprite = itemWepaon.Weapon.IconSkill;
            weaponSkillIcon.gameObject.SetActive(true);
        }

        Inventario.Instance.Character.CharacterAttack.ToEquipWeapoin(itemWepaon);
    }

    public void RemoveWeapon()
    {
        weaponIcon.gameObject.SetActive(false);
        weaponSkillIcon.gameObject.SetActive(false);
        WeaponToEquip = null;
        Inventario.Instance.Character.CharacterAttack.RemoveWeapoin();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterAttack : MonoBehaviour
{
    public static Action<float,EnemigoVida> EventEnemyDamage;

    [Header("Stats")]
    [SerializeField] private CharacterStats stats;

    [Header("Pooler")]
    [SerializeField] private ObjectPooler pooler;

    [Header("Ataque")]
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private Transform[] positionsShoting;

    public EnemigoInteraction EnemyObjetivo { get; private set; }
    public Arma WeapoinEquip { get; private set; }
    public bool Attacking { get; set; }

    private CharacterMana _characterMana;
    private int indexDirectionShoting;
    private float timeForNextAttack;

    private void Awake()
    {
        _characterMana = GetComponent<CharacterMana>();
    }

    private void Update()
    {
        GetDirectionShoting();
        if (Time.time > timeForNextAttack)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (WeapoinEquip == null || EnemyObjetivo==null) return;
                UseWeapoint();
                timeForNextAttack = Time.time + timeBetweenAttacks;
                StartCoroutine(IEEstableshidConditionAttack());
            }
        }
    }

    public float GetDamage()
    {
        float amount = stats.Damage;
        if (Random.value < stats.PorcentageCritic / 100)
        {
            amount *= 2;
        }
        return amount;
    }

    private IEnumerator IEEstableshidConditionAttack()
    {
        Attacking = true;
        yield return new WaitForSeconds(0.3f);
        Attacking = false;
    }

    private void UseWeapoint()
    {
        if (WeapoinEquip.Type == TypeWaepon.Magic)
        {
            if (_characterMana.ManaActual < WeapoinEquip.ManaRequired) return;
            GameObject newProyectil = pooler.GetInstance();
            newProyectil.transform.localPosition = positionsShoting[indexDirectionShoting].position;

            Proyectil proyectil = newProyectil.GetComponent<Proyectil>();
            proyectil.InitializedProyectil(this);

            newProyectil.SetActive(true);
            _characterMana.UseMana(WeapoinEquip.ManaRequired);

            AudioManager.Instance.PlayAudioShooting();
        }
        else
        {
            EnemigoVida enemyLife = EnemyObjetivo.GetComponent<EnemigoVida>();
            float damage = GetDamage();
            enemyLife.ReceiverDamage(damage);
            EventEnemyDamage?.Invoke(damage,enemyLife);
            AudioManager.Instance.PlayAudioMeleePush();
        }
     
    }


    public void ToEquipWeapoin(ItemArma weapointForEquip)
    {
        WeapoinEquip = weapointForEquip.Weapon;
        if(WeapoinEquip.Type == TypeWaepon.Magic)
        {
            pooler.CreatePooler(WeapoinEquip.ProyectilPrefab.gameObject);
        }

        stats.AddBonusForWeapoin(WeapoinEquip);
    }

    public void RemoveWeapoin()
    {
        if (WeapoinEquip == null) return;
        if(WeapoinEquip.Type == TypeWaepon.Magic)
        {
            pooler.DestroyPooler();
        }
        stats.RemoveBonusForWeapoin(WeapoinEquip);
        WeapoinEquip = null;
    }

    private void GetDirectionShoting()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.x > 0.1f)
        {
            indexDirectionShoting = 1;
        }else if(input.x < 0f)
        {
            indexDirectionShoting = 3;
        }else if (input.y > 0.1f)
        {
            indexDirectionShoting = 0;
        }else if (input.y < 0f)
        {
            indexDirectionShoting = 2;
        }
    }

    private void EnemyRangeSelected(EnemigoInteraction enemySelected) 
    {
        if (WeapoinEquip == null) return;
        if (WeapoinEquip.Type != TypeWaepon.Magic) return;
        if (EnemyObjetivo == enemySelected) return;

        EnemyObjetivo = enemySelected;
        EnemyObjetivo.ShowEnemySelected(true,TypeDetecction.Range);
    }
    private void EnemyNoSelected()
    {
        if (EnemyObjetivo == null) return;
        EnemyObjetivo.ShowEnemySelected(false, TypeDetecction.Range);
        EnemyObjetivo = null;
    }

    private void EnemyMeleeDetected(EnemigoInteraction enemyMeleeDetected)
    {
        if (WeapoinEquip == null) return;
        if (WeapoinEquip.Type != TypeWaepon.Melee) return;
        EnemyObjetivo = enemyMeleeDetected;
        EnemyObjetivo.ShowEnemySelected(true, TypeDetecction.Melee);

    }

    private void EnemyMeleeLost()
    {
        if (WeapoinEquip == null) return;
        if (EnemyObjetivo == null) return;
        if (WeapoinEquip.Type != TypeWaepon.Melee) return;
        EnemyObjetivo.ShowEnemySelected(false, TypeDetecction.Melee);
        EnemyObjetivo = null;
    }

    private void OnEnable()
    {
        SelectionManager.EventEnemySelected += EnemyRangeSelected;
        SelectionManager.EventObjectNoSelected += EnemyNoSelected;
        CharacterDetector.EventEnemyDetected += EnemyMeleeDetected;
        CharacterDetector.EventEnemyLost += EnemyMeleeLost;
    }

    private void OnDisable()
    {
        SelectionManager.EventEnemySelected -= EnemyRangeSelected;
        SelectionManager.EventObjectNoSelected -= EnemyNoSelected;
        CharacterDetector.EventEnemyDetected -= EnemyMeleeDetected;
        CharacterDetector.EventEnemyLost -= EnemyMeleeLost;

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum TypesAttack
{
    Melee,
    Embestida
}

public class IAController : MonoBehaviour
{

    public static Action<float> EventDamageDone;

    [Header("Stats")]
    [SerializeField] private CharacterStats stats;

    [Header("Estados")]
    [SerializeField] private IAEstado stateInitial;
    [SerializeField] private IAEstado stateDefault;

    [Header("Config")]
    [SerializeField] private float rangeDetenction;
    [SerializeField] private float rangeAttack;
    [SerializeField] private float rangeEmbestida;
    [SerializeField] private float velocityMove;
    [SerializeField] private float velocityEmbestida;
    [SerializeField] private LayerMask characterLayerMask;

    [Header("Ataque")]
    [SerializeField] private float damage;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private TypesAttack typeAttack;


    [Header("Debug")]
    [SerializeField] private bool showDetection;
    [SerializeField] private bool showRangeAttack;
    [SerializeField] private bool showRangeEmbestida;

    private float timeForNextAttack;
    private BoxCollider2D boxCollider2D;


    public Transform CharacterRef {  get;  set; }
    public IAEstado StateActual { get; set; }
    public EnemigoMovimiento EnemigoMovimiento { get; set; }
    public float RangeDetection => rangeDetenction;
    public float VelocityMove => velocityMove;
    public TypesAttack TypeAttack => typeAttack;
    public float Damage => damage;
    public LayerMask CharacterLayerMask => characterLayerMask;
    public float RangeAttackDeterminado => typeAttack == TypesAttack.Embestida ? rangeEmbestida : rangeAttack;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        StateActual = stateInitial;
        EnemigoMovimiento= GetComponent<EnemigoMovimiento>();
    }

    private void Update()
    {
        StateActual.ExecuteState(this);
    }

    public void ChangeState(IAEstado state)
    {
        if (state != stateDefault) 
        {
            StateActual = state;
        }
    }

    private IEnumerator IEEmbestida(float amount)
    {
        Vector3 characterPosition = CharacterRef.position;
        Vector3 positionInitial = transform.position;
        Vector3 directionToCharacter = (characterPosition - positionInitial).normalized;
        Vector3 positionAttack = characterPosition - directionToCharacter * 0.5f;
        boxCollider2D.enabled = false;
        
        float transitionAttack = 0f;
        while (transitionAttack<=1f)
        {
            transitionAttack += Time.deltaTime * velocityMove;
            float interpolation = (-Mathf.Pow(transitionAttack, 2) + transitionAttack) * 4f;
            transform.position = Vector3.Lerp(positionInitial, positionAttack, interpolation);
            yield return null;
        }
        if(CharacterRef != null)
        {
            ApplyDamageToCharacter(amount);
        }
        boxCollider2D.enabled = true;
    }

    public void AttackEmbestida(float amount)
    {
        StartCoroutine(IEEmbestida(amount));
    }


    public void AttackMelee(float ammout)
    {
        if (CharacterRef != null)
        {
            ApplyDamageToCharacter(ammout);
        }
    }

    public void ApplyDamageToCharacter(float amount)
    {
        if(Random.value < stats.PorcentageBlocking / 100)
        {
            return;
        }
        float damageForCarryOut = Mathf.Max(amount - stats.Defending, 1f);
        CharacterRef.GetComponent<CharacterLife>().ReceiverDamage(damageForCarryOut);
        EventDamageDone?.Invoke(damageForCarryOut);
    }

    public bool CharacterInRangeAttack(float range) 
    {
        float distanceToCharacter = (CharacterRef.position - transform.position).sqrMagnitude;
        if(distanceToCharacter < Mathf.Pow(range,2)) 
        {
            return true;
        }
        return false;
    }

    public bool IsTimeToAttack()
    {
        if(Time.time> timeForNextAttack)
        {
            return true;
        }
        return false;
    }

    public void UpdateTimewBetweenAttacks()
    {
        timeForNextAttack= Time.time + timeBetweenAttacks;
    }

    private void OnDrawGizmos()
    {
        if (showDetection)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, rangeDetenction);
        }

        if (showRangeAttack)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, rangeAttack);
        }

        if (showRangeEmbestida)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, rangeEmbestida);
        }


    }

}

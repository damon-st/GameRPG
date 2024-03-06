using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private string layerIdle;
    [SerializeField] private string layerWalking;
    [SerializeField] private string layerAttack;

    private Animator _animator;
    private CharacterMove _characterMove;
    private CharacterAttack _characterAttack;

    private readonly int directionX = Animator.StringToHash("X");
    private readonly int directionY = Animator.StringToHash("Y");
    private readonly int derrotado = Animator.StringToHash("Derrotado");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterMove = GetComponent<CharacterMove>();
        _characterAttack = GetComponent<CharacterAttack>();
    }

    // Update is called once per frame
    void Update()
    {
       UpdatedLayers();
        if (!_characterMove.IsMoved)
        {
            return;
        }
        _animator.SetFloat(directionX, _characterMove.DirectionMove.x);
        _animator.SetFloat(directionY, _characterMove.DirectionMove.y);
    }

    private void ActiveLayer(string nameLayer) {

        for (int i = 0; i < _animator.layerCount; i++)
        {
            _animator.SetLayerWeight(i, 0);
        }

        _animator.SetLayerWeight(_animator.GetLayerIndex(nameLayer), 1);
    }

    private void UpdatedLayers()
    {
        if (_characterAttack.Attacking)
        {
            ActiveLayer(layerAttack);
        }
        else if (_characterMove.IsMoved)
        {
            ActiveLayer(layerWalking);
        }
        else
        {
            ActiveLayer(layerIdle);
        }
    }

    public void RevivirCharacter()
    {
        ActiveLayer(layerIdle);
        _animator.SetBool(derrotado, false);
    }

    private void CharacterDefeatedReponse()
    {
        if (_animator.GetLayerWeight(_animator.GetLayerIndex(layerIdle)) == 1)
        {
            _animator.SetBool(derrotado, true);
        }
        else
        {
            ActiveLayer(layerIdle);
            _animator.SetBool(derrotado, true);
        }
    }

    private void OnEnable()
    {
        CharacterLife.EventCharacterDead += CharacterDefeatedReponse;
    }


    private void OnDisable()
    {
        CharacterLife.EventCharacterDead -= CharacterDefeatedReponse;

    }
}

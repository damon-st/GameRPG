using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField] private CharacterStats stats;

    public CharacterAttack CharacterAttack { get; private set; }
    public CharacterExperience CharacterExperience {  get; private set; }

    public CharacterLife CharacterLife { get; private set; }
    public CharacterAnimations CharacterAnimations { get; private set; }
    public CharacterMana CharacterMana { get; private set; }

    private void Awake()
    {
        CharacterAttack = GetComponent<CharacterAttack>();
        CharacterLife = GetComponent<CharacterLife>();
        CharacterAnimations = GetComponent<CharacterAnimations>();
        CharacterMana = GetComponent<CharacterMana>();
        CharacterExperience = GetComponent<CharacterExperience>();
    }

    public void RestarCharacter()
    {
        CharacterLife.RestartCharacter();
        CharacterAnimations.RevivirCharacter();
        CharacterMana.RestartMana();
    }


    private void AttributeResponse(TypeAttributes type)
    {
        if(stats.PointsAvalibles <= 0)
        {
            return;
        }

        switch (type)
        {
            case TypeAttributes.Force:
                stats.Force++;
                stats.AddBoundForAttributeForce();
                break;
            case TypeAttributes.Intelligence:
                stats.Intelligence++;
                stats.AddBoundForAttribueIntelligence();
                break;
            case TypeAttributes.Skill:
                stats.Skill++;
                stats.AddBoundForAttributeSkill();
                break;
        }
        stats.PointsAvalibles -= 1;
    }


    private void OnEnable()
    {
        AttributoButtom.EventAddAttribute += AttributeResponse;
    }

    private void OnDisable()
    {
        AttributoButtom.EventAddAttribute -= AttributeResponse;
    }


}

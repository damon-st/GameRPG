using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="IA/Acciones/SeguirPersonaje")]
public class ActionFollowCharacter : IAAccion
{
    public override void Execute(IAController controller)
    {
        FollowCharacter(controller);
    }

    private void FollowCharacter(IAController controller) 
    {
        if (controller.CharacterRef == null) return;
        if (controller.CharacterRef.GetComponent<CharacterLife>().Defeated) return;
        Vector3 directionToCharacter = controller.CharacterRef.position - controller.transform.position;
        Vector3 directon = directionToCharacter.normalized;
        float distance = directionToCharacter.magnitude;
        if(distance >= 1.30f)
        {
            controller.transform.Translate(directon * controller.VelocityMove * Time.deltaTime);
        }

    }
}

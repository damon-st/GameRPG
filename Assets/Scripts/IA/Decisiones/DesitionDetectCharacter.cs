using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="IA/Decisiones/DetectarPersonaje")]
public class DesitionDetectCharacter : IADecesion
{
    public override bool Decidir(IAController controller)
    {
        return DetectCharacter(controller);
    }

    private bool DetectCharacter(IAController controller)
    {
        Collider2D characterDetect = Physics2D.OverlapCircle(controller.transform.position, controller.RangeDetection, controller.CharacterLayerMask);

        if (characterDetect != null)
        {
            controller.CharacterRef = characterDetect.transform;
            return true;
        }
        controller.CharacterRef = null;
        return false;
    }
}

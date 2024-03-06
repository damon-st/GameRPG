using UnityEngine;

[CreateAssetMenu(menuName ="IA/Estado")]
public class IAEstado : ScriptableObject
{
    public IAAccion[] Acctions;
    public IATrancision[] Transitions;


    public void ExecuteState(IAController controller)
    {
        ExecuteActions(controller);
        ExecuteTransitions(controller);
    }


    private void ExecuteActions(IAController controller)
    {
        if(Acctions==null || Acctions.Length<=0)return;
        for (int i = 0; i < Acctions.Length; i++)
        {
            Acctions[i].Execute(controller);
        }
    }

    private void ExecuteTransitions(IAController controller)
    {
        if(Transitions==null || Transitions.Length<=0) return;
        for (int i = 0; i < Transitions.Length; i++)
        {
            var transition = Transitions[i];
            bool desitionValue = transition.Decision.Decidir(controller);
            if(desitionValue) 
            {
                controller.ChangeState(transition.StateTrue);
            }
            else
            {
                controller.ChangeState(transition.StateFalse);
            }
        }
    }
}

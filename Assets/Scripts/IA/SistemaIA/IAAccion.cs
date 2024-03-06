using UnityEngine;

public abstract class IAAccion : ScriptableObject
{
    public abstract void Execute(IAController controller);

}

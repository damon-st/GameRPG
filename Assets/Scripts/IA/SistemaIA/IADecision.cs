using UnityEngine;

public abstract class IADecesion : ScriptableObject
{
    public abstract bool Decidir(IAController controller);
}

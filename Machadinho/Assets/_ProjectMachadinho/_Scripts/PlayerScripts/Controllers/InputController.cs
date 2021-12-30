using UnityEngine;

namespace Machadinho
{
    public abstract class InputController : ScriptableObject
    {
        public abstract float RetrieveMoveInput();
        public abstract bool RetrieveJumpInput();
        public abstract bool RetrieveFireInput();
        public abstract bool RetrieveFireUpInput();

    }
}

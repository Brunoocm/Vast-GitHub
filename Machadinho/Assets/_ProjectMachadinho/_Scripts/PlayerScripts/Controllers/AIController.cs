using UnityEngine;

namespace Machadinho
{
    [CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
    public class AIController : InputController
    {
        public override bool RetrieveJumpInput()
        {
            return true;
        } 
        public override bool RetrieveFireInput()
        {
            return true;
        } 
        public override bool RetrieveFireUpInput()
        {
            return true;
        }

        public override float RetrieveMoveInput()
        {
            return 1f;
        }
    }
}

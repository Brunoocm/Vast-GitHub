using UnityEngine;

namespace Machadinho
{
    [CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
    public class PlayerController : InputController
    {
        public override bool RetrieveJumpInput()
        {
            return Input.GetButtonDown("Jump");
        } 
        public override bool RetrieveFireInput()
        {
            return Input.GetButtonDown("Fire1");
        }
        public override bool RetrieveFireUpInput()
        {
            return Input.GetButtonUp("Fire1");
        }
        public override float RetrieveMoveInput()
        {
            return Input.GetAxisRaw("Horizontal");
        }   
       
    }
}

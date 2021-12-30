using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Machadinho
{
    public class InputHandler : MonoBehaviour
    {
        Vector2 aim;
        public enum eInputState
        {
            MouseKeyboard,
            Controler
        };
        public eInputState currentState = eInputState.MouseKeyboard;

        private void Awake()
        {
        }

        private void Update()
        {
            print(currentState);
           
        }

        void OnGUI()
        {
            switch (currentState)
            {
                case eInputState.MouseKeyboard:
                    if (isControlerInput())
                    {
                        currentState = eInputState.Controler;
                    }
                    break;
                case eInputState.Controler:
                    if (isMouseKeyboard())
                    {
                        currentState = eInputState.MouseKeyboard;
                    }
                    break;
            }
        }


        public eInputState GetInputState()
        {
            return currentState;
        }



        private bool isMouseKeyboard()
        {
            if (Event.current.isKey ||
                Event.current.isMouse)
            {
                return true;
            }
            if (Input.GetAxis("Mouse X") != 0.0f ||
                Input.GetAxis("Mouse Y") != 0.0f)
            {
                return true;
            }
            return false;
        }

        private bool isControlerInput()
        {

            if (Input.GetKey(KeyCode.Joystick1Button0) ||
               Input.GetKey(KeyCode.Joystick1Button1) ||
               Input.GetKey(KeyCode.Joystick1Button2) ||
               Input.GetKey(KeyCode.Joystick1Button3) ||
               Input.GetKey(KeyCode.Joystick1Button4) ||
               Input.GetKey(KeyCode.Joystick1Button5) ||
               Input.GetKey(KeyCode.Joystick1Button6) ||
               Input.GetKey(KeyCode.Joystick1Button7) ||
               Input.GetKey(KeyCode.Joystick1Button8) ||
               Input.GetKey(KeyCode.Joystick1Button9) ||
               Input.GetKey(KeyCode.Joystick1Button10) ||
               Input.GetKey(KeyCode.Joystick1Button11) ||
               Input.GetKey(KeyCode.Joystick1Button12) ||
               Input.GetKey(KeyCode.Joystick1Button13) ||
               Input.GetKey(KeyCode.Joystick1Button14) ||
               Input.GetKey(KeyCode.Joystick1Button15) ||
               Input.GetKey(KeyCode.Joystick1Button16) ||
               Input.GetKey(KeyCode.Joystick1Button17) ||
               Input.GetKey(KeyCode.Joystick1Button18) ||
               Input.GetKey(KeyCode.Joystick1Button19))
            {
                return true;
            }

            // joystick axis
            if (Input.GetAxis("HorizontalJoystick") != 0.0f ||
               Input.GetAxis("VerticalJoystick") != 0.0f ||
               Input.GetAxis("HorizontalRightJoystick") != 0.0f ||
               Input.GetAxis("VerticalRightJoystick") != 0.0f)


            {
                return true;
            }

            return false;
        }
    }
}

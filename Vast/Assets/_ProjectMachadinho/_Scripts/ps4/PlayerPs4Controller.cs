using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Machadinho
{
    public class PlayerPs4Controller : MonoBehaviour
    {
        public GameObject crossHair;

        [HideInInspector] public float angleValue;

        private Controllers controls;
        private Skills skills;
        private Move move;
        private InputHandler inputHandler;

        private Vector2 right = new Vector2(1, 0);
        private Vector2 left = new Vector2(-1, 0);
        private Vector2 aim;
        private void Awake()
        {
            controls = new Controllers();
            skills = GetComponent<Skills>();
            move = GetComponent<Move>();
            inputHandler = GetComponent<InputHandler>();

            //controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
            //controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
        }

        private void Update()
        {

            if (inputHandler.currentState == InputHandler.eInputState.Controler)
            {
                AimInput();
                if (!skills.shooting) Flip();
            }
            else
            {
                crossHair.SetActive(false);
            }

        }

        void AimInput()
        {
            aim = controls.Gameplay.Aim.ReadValue<Vector2>();
            var angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;

            if (angle != 0)
            {
                crossHair.SetActive(true);
                crossHair.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                if (skills.shooting)
                {
                    skills.Shoot(aim);
                }
            }
            else
            {
                crossHair.SetActive(false);

                if (skills.shooting)
                {
                    if(move.facingRight) skills.Shoot(right);
                    else skills.Shoot(left);
                }
            }
        }

        void Flip()
        {
            Vector3 scale = crossHair.transform.localScale;
            if (Input.GetAxis("Horizontal") < 0)
            {
                scale.x = -1;
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                scale.x = 1;
            }
            crossHair.transform.localScale = scale;
        }

        private void OnEnable()
        {
            crossHair.SetActive(true);
            controls.Gameplay.Enable();
        }

        private void OnDisable()
        {
            crossHair.SetActive(false);
            controls.Gameplay.Disable();
        }
    }
}
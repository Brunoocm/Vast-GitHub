using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Machadinho
{
    public class Skills : MonoBehaviour
    {
        public GameObject prefab;
        public Transform pos;
        [SerializeField, Range(0f, 25f)] private float force = 3f;
        public bool shooting;
        public bool shootingUp;

        private Controller controller;
        private PlayerPs4Controller playerPs4Controller;
        private InputHandler inputHandler;

        private Vector2 aim;
        private Rigidbody2D rb;

        void Awake()
        {
            controller = GetComponent<Controller>();
            playerPs4Controller = GetComponent<PlayerPs4Controller>();
            inputHandler = GetComponent<InputHandler>();
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            GameObject machado = GameObject.FindGameObjectWithTag("Machado");
            if (machado == null) shooting |= controller.input.RetrieveFireInput();
            if (machado == null) shootingUp |= controller.input.RetrieveFireUpInput();
            AimMouse();
        }

        void AimMouse()
        {

            if (inputHandler.currentState == InputHandler.eInputState.MouseKeyboard)
            {
                aim = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

                if (shooting)
                {
                    Shoot(aim);
                  
                }
            }
        }

        public void Shoot(Vector2 direction)
        {         
           
            rb.bodyType = RigidbodyType2D.Static;
            if (shootingUp)
            {
                GameObject newBullet = Instantiate(prefab, pos.transform.position, Quaternion.identity);

                newBullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * force;

                rb.bodyType = RigidbodyType2D.Dynamic;

                shootingUp = false;
                shooting = false;
            }
        }
    }
}

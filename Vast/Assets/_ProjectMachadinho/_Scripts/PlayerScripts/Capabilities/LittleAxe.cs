using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Machadinho
{
    public class LittleAxe : MonoBehaviour
    {
        public float cdGrab;
        public bool canGrab;
        public bool hit;

        private Rigidbody2D rb;
        private GameObject player;
        Move move;
        Jump jump;

        Vector2 lastVelocity;

        private void Awake()
        {
            Physics2D.IgnoreLayerCollision(3, 6, true);
          
        }
        void Start()
        {
            canGrab = false;

            player = GameObject.FindGameObjectWithTag("Player");
            move = player.GetComponent<Move>();
            jump = player.GetComponent<Jump>();
            rb = GetComponent<Rigidbody2D>();

            Destroy(gameObject, 2f);
        }

        void Update()
        {

            //if (!hit)
            //{
            //    //float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            //    //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //}
            //else
            //{
            //    //rb.velocity = Vector2.zero;
            //    //rb.isKinematic = true;
            //    //rb.constraints = RigidbodyConstraints2D.FreezeAll;
            //}
        }
        private void FixedUpdate()
        {
            lastVelocity = rb.velocity;

        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            rb.velocity = Vector2.Reflect(lastVelocity, other.contacts[0].normal);

            hit = true;

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player") && hit)
            {
                move.ResetDash();
                jump.ResetJump();
                canGrab = true;
                Destroy(gameObject);

            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player") && hit)
            {
                canGrab = false;
            }
        }
    }
}
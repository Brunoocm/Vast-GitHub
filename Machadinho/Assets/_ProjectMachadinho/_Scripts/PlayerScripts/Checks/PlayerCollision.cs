using UnityEngine;

namespace Machadinho
{
    public class PlayerCollision : MonoBehaviour
    {
        Move move => GetComponent<Move>();
        Jump jump => GetComponent<Jump>();

        private void OnTriggerEnter2D(Collider2D other)
        {

        }
        private void OnTriggerStay2D(Collider2D other)
        {

        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            
        }
    }
}

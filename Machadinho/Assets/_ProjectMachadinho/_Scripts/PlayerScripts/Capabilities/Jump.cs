using UnityEngine;

namespace Machadinho
{
    [RequireComponent(typeof(Controller))]
    public class Jump : MonoBehaviour
    {
        [SerializeField, Range(0f, 10f)] private float jumpHeight = 3f;
        [SerializeField, Range(0, 5)] private int maxAirJumps = 0;
        [SerializeField, Range(0, 5)] private int maxDash = 0;
        [SerializeField, Range(0f, 5f)] private float downwardMovementMultiplier = 3f;
        [SerializeField, Range(0f, 5f)] private float upwardMovementMultiplier = 1.7f;

        private Controller controller;
        private Skills skills;
        private Rigidbody2D body;
        private Ground ground;
        private Vector2 velocity;

        private int jumpPhase;
        private int m_maxAirJumps;
        private float defaultGravityScale;

        private bool desiredJump;
        private bool onGround;


        // Start is called before the first frame update
        void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            ground = GetComponent<Ground>();
            controller = GetComponent<Controller>();
            skills = GetComponent<Skills>();

            defaultGravityScale = 1f;
            m_maxAirJumps = maxAirJumps;
        }

        // Update is called once per frame
        void Update()
        {
            if(!skills.shooting) desiredJump |= controller.input.RetrieveJumpInput();
        }

        private void FixedUpdate()
        {
            onGround = ground.GetOnGround();
            velocity = body.velocity;

            if (onGround)
            {
                jumpPhase = 0;
            }

            if (desiredJump)
            {
                desiredJump = false;
                JumpAction();
            }

            if (body.velocity.y > 0)
            {
                body.gravityScale = upwardMovementMultiplier;
            }
            else if (body.velocity.y < 0)
            {
                body.gravityScale = downwardMovementMultiplier;
            }
            else if(body.velocity.y == 0)
            {
                body.gravityScale = defaultGravityScale;
            }

            body.velocity = velocity;
        }
        private void JumpAction()
        {
            if (onGround || jumpPhase < maxAirJumps)
            {
                jumpPhase += 1;
                float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
                if (velocity.y > 0f)
                {
                    jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
                }
                else if (velocity.y < 0f)
                {
                    jumpSpeed += Mathf.Abs(body.velocity.y);
                }
                velocity.y += jumpSpeed;
            }
        }

        public void ResetJump()
        {
            jumpPhase = 0;
        }
    }
}


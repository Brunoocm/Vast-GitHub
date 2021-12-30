using UnityEngine;

namespace Machadinho
{
    [RequireComponent(typeof(Controller))]
    public class Move : MonoBehaviour
    {
        [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
        [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
        [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;

        PlayerAnimation playerAnimation => GetComponent<PlayerAnimation>();

        private GameObject AxeObj;
        private Controller controller;
        private Skills skills;
        private Rigidbody2D rb;
        private Ground ground;

        private Vector2 direction;
        private Vector2 desiredVelocity;
        private Vector2 velocity;
        private Vector2 aim;
        private Vector2 dashDir;
        private Vector2Int v2i;

        private float maxSpeedChange;
        private float acceleration;
        private bool onGround;
        [HideInInspector] public bool facingRight = true;

        [Header("Dash")]
        public int dashCount = 1;
        public float dashSpeed;
        public float startDashTime;

        private float dashTime;
        private int dirDash;
        private int m_dashCount;

        private float horizontalMove;
        private float moveAnim;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            ground = GetComponent<Ground>();
            controller = GetComponent<Controller>();
            skills = GetComponent<Skills>();

            dashTime = startDashTime;
            m_dashCount = dashCount;
        }

        private void Update()
        {
            if (!skills.shooting)
            {
                Flip();
                direction.x = controller.input.RetrieveMoveInput();
            }
            desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);

        }

        private void FixedUpdate()
        {
            onGround = ground.GetOnGround();
            velocity = rb.velocity;

            acceleration = onGround ? maxAcceleration : maxAirAcceleration;
            maxSpeedChange = acceleration * Time.deltaTime;
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

            rb.velocity = velocity;

            dashDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            moveAnim = Input.GetAxis("Horizontal");
            playerAnimation.anim.SetFloat("Speed", Mathf.Abs(moveAnim));

            if (!skills.shooting)
            {
                Dash();
            }

        }

        void Flip()
        {
            Vector3 charecterScale = transform.localScale;
            if (Input.GetAxis("Horizontal") < 0)
            {
                charecterScale.x = -1;
                facingRight = false;
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                charecterScale.x = 1;
                facingRight = true;
            }
            transform.localScale = charecterScale;
        }

        void TowardsAxe()
        {
            if (Input.GetAxisRaw("Dash") > 0 && dashCount > 0)
            {
                rb.velocity = Vector2.zero;
                AxeObj = GameObject.FindGameObjectWithTag("Machado");
                AxeObj.GetComponent<LittleAxe>().hit = true;

                Vector2 target = new Vector2(AxeObj.transform.position.x, AxeObj.transform.position.y);

                transform.position = Vector2.MoveTowards(transform.position, target, 20 * Time.deltaTime);
            }


        }

        void Dash()
        {


            horizontalMove = Input.GetAxis("Horizontal") + Input.GetAxis("Vertical");

            if (dirDash == 0)
            {
                if (Input.GetAxisRaw("Dash") > 0 && dashCount > 0)
                {
                    dashCount--;
                    dirDash = 1;

                    if (horizontalMove == 0)
                    {
                        if (facingRight)
                        {
                            rb.velocity = Vector2Int.right * (int)dashSpeed;

                        }
                        else
                        {
                            rb.velocity = Vector2Int.left * (int)dashSpeed;
                        }
                    }
                    else
                    {
                        v2i = Vector2Int.RoundToInt(dashDir);
                    }
                }
            }
            else
            {
                if (dashTime <= 0)
                {
                    dirDash = 0;
                    dashTime = startDashTime;
                    rb.velocity = Vector2.zero;
                }
                else
                {
                    dashTime -= Time.deltaTime;

                    if (horizontalMove != 0) rb.velocity = v2i * (int)dashSpeed;
                }
            }

            if (onGround)
            {
                dashCount = m_dashCount;
            }
        }


        public void ResetDash()
        {
            dashCount = m_dashCount;
        }
    }
}

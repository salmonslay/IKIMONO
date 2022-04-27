using UnityEngine;

namespace IKIMONO.Minigame.Jump
{
    public class JumpPlayer : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Vector3 _screenBounds;
        [SerializeField] private Transform _groundCheck;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _screenBounds = Camera.main!.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        }

        private void Update()
        {
            Vector2 move = new Vector2(Input.GetAxis("Horizontal"), 0);
            transform.Translate(Time.deltaTime * 5 * move);

            if (Input.GetKeyDown(KeyCode.Space))
                Jump();

            if (IsGrounded())
            {
                Jump();
            }

            // Teleport the player to the other side of the screen if they go off screen
            Vector3 pos = transform.position;
            if (pos.x > _screenBounds.x)
                transform.position = new Vector3(-_screenBounds.x, pos.y, pos.z);
            else if (pos.x < -_screenBounds.x)
                transform.position = new Vector3(_screenBounds.x, pos.y, pos.z);
        }

        public bool IsGrounded()
        {
            if (_rigidbody2D.velocity.y > 0)
            {
                return false;
            }

            Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, 0.2f);
            foreach (Collider2D collider in colliders)
            {
                if (!collider.CompareTag("Player") && !collider.isTrigger)
                    return true;
            }
            return false;
        }

        private void Jump()
        {
            // This technically doesn't require a grounded check, but it might be good to have? Eh let's skip that for now
            _rigidbody2D.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        }
    }
}
using UnityEngine;

namespace IKIMONO.Minigame.Jump
{
    public class JumpPlayer : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Vector3 _screenBounds;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _screenBounds = Camera.main!.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        }

        private void Update()
        {
            // TODO Tova: Use device tilting instead of keyboard input
            Vector2 move = new Vector2(Input.GetAxis("Horizontal"), 0);
            transform.Translate(move * Time.deltaTime * 5);
            
            // TODO Tova: Jump automatically when the player is on the ground
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
            
            // Teleport the player to the other side of the screen if they go off screen
            Vector3 pos = transform.position;
            if (pos.x > _screenBounds.x)
                transform.position = new Vector3(-_screenBounds.x, pos.y, pos.z);
            else if (pos.x < -_screenBounds.x)
                transform.position = new Vector3(_screenBounds.x, pos.y, pos.z);
        }
        
        private void Jump()
        {
            // This technically doesn't require a grounded check, but it might be good to have? Eh let's skip that for now
            _rigidbody2D.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        }
    }
}
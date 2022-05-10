using IKIMONO.Pet;
using UnityEngine;
using UnityEngine.UI;

namespace IKIMONO.UI
{
    public class IkimonoSponge : MonoBehaviour
    {
        private Image _sponge;
        [SerializeField] private GameObject _waterFx;
        [SerializeField] private GameObject _bubblesFx;
        private bool _isActive = false;
        
        #region Touch properties
        private Vector2 _fingerDownPos;
        private Vector2 _fingerUpPos;
        private const float SwipeThreshold = 50f; // Min swipe distance
        #endregion
        
        private void Awake()
        {

            
            _sponge = GetComponent<Image>();
            Set(false);
        }

        private void Update()
        {
            if (_isActive && Input.touchCount> 0)
            {
                // Set sponge position
                Touch touch = Input.GetTouch(0);
                Vector3 screenPos = touch.position;
                transform.position = screenPos;
                
                // Calculate swipe
                switch (touch.phase)
                {
                    // Record initial touch position to later calculate swipe distance and direction
                    case TouchPhase.Began:
                    {
                        _fingerUpPos = touch.position;
                        _fingerDownPos = touch.position;
                        break;
                    }
                    
                    // Detects Swipe while finger is still moving on screen
                    case TouchPhase.Moved:
                    {
                        _fingerDownPos = touch.position;
                        DetectSwipe();
                        break;
                    }
                    // Detects swipe after finger is released from screen
                    case TouchPhase.Ended:
                    {
                        _fingerDownPos = touch.position;
                        DetectSwipe(); 
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Toggle the cleaning mode. If the sponge is active, it will be disabled and vice versa.
        /// </summary>
        public void Toggle()
        {
            Set(!_isActive);
        }
        
        /// <summary>
        /// Set the cleaning mode to active or inactive
        /// </summary>
        /// <param name="active">Whether or not the cleaning mode should be enabled</param>
        public void Set(bool active)
        {
            _isActive = active;
            _sponge.enabled = _isActive;
            
        }
        
        // TODO: Replace? 
        // Likely not needed, but this is from another of my projects so it's a bit overly complicated for this. - Fabian
        private void DetectSwipe()
        {
            float verticalMoveValue = Mathf.Abs(_fingerDownPos.y - _fingerUpPos.y);
            float horizontalMoveValue = Mathf.Abs(_fingerDownPos.x - _fingerUpPos.x);
            if (verticalMoveValue > SwipeThreshold && verticalMoveValue > horizontalMoveValue)
            {
                if (_fingerDownPos.y - _fingerUpPos.y > 0)
                {
                    // Swipe up
                    Clean();
                }
                else if (_fingerDownPos.y - _fingerUpPos.y < 0)
                {
                    // Swipe down
                    Clean();
                }

                _fingerUpPos = _fingerDownPos;
            }
            else if (horizontalMoveValue > SwipeThreshold && horizontalMoveValue > verticalMoveValue)
            {
                if (_fingerDownPos.x - _fingerUpPos.x > 0)
                {
                    // Swipe right
                    Clean();
                }
                else if (_fingerDownPos.x - _fingerUpPos.x < 0)
                {
                    // Swipe left
                    Clean();
                }

                _fingerUpPos = _fingerDownPos;
            }
        }

        public static void Clean()
        {
            

            
            AudioManager.Instance.RandomizeSound("Bubbles");
            // @PhilipAudio: Play the cleaning sound here, and maybe some bubbles?
            // This is ran multiple times per swipe, so it will require a cooldown.
            Player.Instance.Pet.Hygiene.Increase(0.05f);
        }
    }
}
using UnityEngine;

namespace IKIMONO.Minigame.Jump
{
    public class SmoothCamera : MonoBehaviour
    {
        [Tooltip("The target we are following")]
        [SerializeField]  private Transform _target;
        
        [Tooltip("The Y position offset")]
        [SerializeField] private float _offset = 2f;
        
        [Tooltip("The speed of the camera")]
        [SerializeField] private float _speedModifier = 2f;
        
        private void FixedUpdate()
        {
            Vector3 targetPosition = _target.position;
            targetPosition.y += _offset;
            
            float speed = _speedModifier * Time.deltaTime;

            Vector3 currentPosition = transform.position;
            
            float y = Mathf.Lerp(currentPosition.y, targetPosition.y, speed);
            transform.position = new Vector3(currentPosition.x, y, currentPosition.z);
        }
    }
}
using UnityEngine;

namespace IKIMONO.UI
{
    public class UICoin : MonoBehaviour
    {
        private Vector3 _endPos;


        private void Start()
        {
            AudioManager.Instance.RandomizeSound("Swoosh");
        }
        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPos, Time.deltaTime * 100);

            if (transform.position == _endPos)
            {
                AudioManager.Instance.RandomizeSound("Coin");
            }


            
            if (transform.position == _endPos)
            {
                Pet.Player.Instance.AddCoins(1);
                         
                Destroy(gameObject);
            }
        }
        
        public void SetTarget(Vector3 target)
        {
            _endPos = target;
        }
    }
}
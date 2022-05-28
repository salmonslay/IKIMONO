using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunAndMoon : MonoBehaviour
{

    [SerializeField] private Vector2 _parallaxEffectMultiplier;
    [SerializeField] private Sprite _firstSprite;
    [SerializeField] private Sprite _secondSprite;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private float _fadeOutStartHeight;
    [SerializeField] private float _fadeTime;


    private Transform _cameraTransform;
    private Vector3 _lastCameraPosition;

    private Vector3 _deltaMovement;

    void Start()
    {
        _cameraTransform = Camera.main.transform;
        _lastCameraPosition = _cameraTransform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite sprite = _spriteRenderer.sprite;
    }

    private bool _hasSwitched = false;
    void LateUpdate()
    {
        _deltaMovement = _cameraTransform.position - _lastCameraPosition;
        MoveSprite();

        if (transform.position.x > 5)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y);

            if (!_hasSwitched)
            {
                transform.localScale *= 2;
                _spriteRenderer.sprite = _secondSprite;
                _hasSwitched = true;
            }
        }
    }

    private void MoveSprite()
    {
        transform.position += new Vector3(_deltaMovement.y * _parallaxEffectMultiplier.x, _deltaMovement.y * _parallaxEffectMultiplier.y);
        _lastCameraPosition = _cameraTransform.position;
    }

}

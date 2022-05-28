using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBackgrundScroll : MonoBehaviour
{

    [SerializeField] private Vector2 _parallaxEffectMultiplier;
    [SerializeField] private Sprite _firstSprite;
    [SerializeField] private Sprite _secondSprite;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private float _fadeOutStartHeight;
    [SerializeField] private float _fadeTime;


    private Transform _cameraTransform;
    private Vector3 _lastCameraPosition;
    private float _textureUnitSizeY;
    private Vector3 _deltaMovement;

    [SerializeField] private SpriteRenderer _darknessSpriteRenderer;


    void Start()
    {
        _cameraTransform = Camera.main.transform;
        _lastCameraPosition = _cameraTransform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite sprite = _spriteRenderer.sprite;
        Texture2D texture = sprite.texture;
        _textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    private bool _hasSwitched = false;
    void LateUpdate()
    {
        _deltaMovement = _cameraTransform.position - _lastCameraPosition;
        ScrollBackground();
        if (_cameraTransform.position.y > _fadeOutStartHeight && _spriteRenderer.color.a > 0 && !_hasSwitched)
        {
            FadeOutSprite();
            if (_darknessSpriteRenderer.color.a < .66)
                FadeInDarkness();

        }
        if (_spriteRenderer.color.a <= 0 && !_hasSwitched)
        {
            _spriteRenderer.sprite = _secondSprite;
            _parallaxEffectMultiplier = new Vector2(_parallaxEffectMultiplier.x, _parallaxEffectMultiplier.y * 1.9f);
            _hasSwitched = true;
        }
        if (_spriteRenderer.color.a < 1 && _hasSwitched)
        {
            FadeInSprite();
        }
    }

    private void ScrollBackground()
    {
        transform.position += new Vector3(_deltaMovement.y * _parallaxEffectMultiplier.x, _deltaMovement.y * _parallaxEffectMultiplier.y);
        _lastCameraPosition = _cameraTransform.position;

        if (Mathf.Abs(_cameraTransform.position.y - transform.position.y) >= _textureUnitSizeY)
        {
            float offsetPositionY = (_cameraTransform.position.y - transform.position.y) % _textureUnitSizeY;
            transform.position = new Vector3(transform.position.x, _cameraTransform.position.y + offsetPositionY);
        }
    }

    private void FadeOutSprite()
    {
        Color color = _spriteRenderer.color;
        float fade = _spriteRenderer.color.a - _deltaMovement.y / _fadeTime;
        _spriteRenderer.color = new Color(color.r, color.g, color.b, fade);
    }

    private void FadeInSprite()
    {
        Color color = _spriteRenderer.color;
        float fade = _spriteRenderer.color.a + _deltaMovement.y / _fadeTime / 2;
        _spriteRenderer.color = new Color(color.r, color.g, color.b, fade);
    }

    private void FadeInDarkness()
    {
        Color color = _darknessSpriteRenderer.color;
        float fade = _darknessSpriteRenderer.color.a + _deltaMovement.y / _fadeTime;
        _darknessSpriteRenderer.color = new Color(color.r, color.g, color.b, fade);
    }



}

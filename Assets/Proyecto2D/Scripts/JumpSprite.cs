using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSprite: MonoBehaviour
{
    public Texture2D t_jumpStartTexture;
    public Texture2D t_jumpAirTexture;
    public Texture2D t_jumpDownTexture;

    public void updateJumpAnimation(int _direction, float _velocityY, Material _material)
    {
        if ((_velocityY >= -2.0f) && (_velocityY <= 2.0f))
        {
            _material.mainTexture = t_jumpAirTexture;
        }
        else if (_velocityY > 2.0f)
        {
            _material.mainTexture = t_jumpStartTexture;
        }
        else
        {
            _material.mainTexture = t_jumpDownTexture;
        }
        _material.mainTextureScale = new Vector2(_direction * 1, 1);
        _material.mainTextureOffset = new Vector2(_direction * 1, 1);

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManagerWalk : MonoBehaviour
{
    public Texture2D spriteTexture;
    public int in_framePerSec;
    public int in_gridX;
    public int in_gridY;

    private float f_timePercent;
    private float f_nextTime;
    private float f_gridX;
    private float f_gridY;

    private int in_curFrame;

    // Start is called before the first frame update
    public void SpriteManagerWalkStart()
    {
        f_timePercent = 1.0f / in_framePerSec;
        f_nextTime = f_timePercent;
        f_gridX = 1.0f / in_gridX;
        f_gridY = 1.0f / in_gridY;
        in_curFrame = 1;
    }

    public void updateAnimation(int _direction, Material _material)
    {

        _material.mainTexture = spriteTexture;

        if (Time.time > f_nextTime)
        {
            f_nextTime = Time.time + f_timePercent;
            in_curFrame++;
            if (in_curFrame > in_framePerSec)
            {
                in_curFrame = 1;
            }
        }

        _material.mainTextureScale = new Vector2(_direction * f_gridX, f_gridY);
        int in_col = 0;
        if (in_gridY > 1)
        {
            in_col = (int)Mathf.Ceil(in_curFrame / in_gridX);
        }
        if (_direction == 1)
        {
            _material.mainTextureOffset = new Vector2(((in_curFrame) % in_gridX) * f_gridX, in_col * f_gridY);
        }
        else
        {
            _material.mainTextureOffset = new Vector2((in_gridX + (in_curFrame) % in_gridX) * f_gridX, in_col * f_gridY);
        }
    }

    public void resetFrame()
    {
        in_curFrame = 1;
    }

}

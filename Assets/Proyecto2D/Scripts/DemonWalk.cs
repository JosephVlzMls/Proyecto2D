using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonWalk : MonoBehaviour {

    MeshRenderer renderer;
    Rigidbody rigidbody;

    public Texture2D spriteTexture;
    public int in_framePerSec;
    public int in_gridX;
    public int in_gridY;
    public int speed;

    private float f_timePercent;
    private float f_nextTime;
    private float f_gridX;
    private float f_gridY;
    
    private int direction;
    private int in_curFrame;

    void Start() {
        direction = 1;
        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<MeshRenderer>();
        SpriteManagerWalkStart();
    }

    void Update() {
        updateAnimation();
    }

    public void SpriteManagerWalkStart() {
        f_timePercent = 1.0f / in_framePerSec;
        f_nextTime = f_timePercent;
        f_gridX = 1.0f / in_gridX;
        f_gridY = 1.0f / in_gridY;
        in_curFrame = 1;
    }

    public void updateAnimation() {
        rigidbody.velocity = new Vector3((direction * speed), rigidbody.velocity.y, 0);
        renderer.material.mainTexture = spriteTexture;
        if(Time.time > f_nextTime) {
            f_nextTime = Time.time + f_timePercent;
            in_curFrame++;
            if (in_curFrame > in_framePerSec)
            {
                in_curFrame = 1;
            }
        }
        renderer.material.mainTextureScale = new Vector2(direction * f_gridX, f_gridY);
        int in_col = 0;
        if(in_gridY > 1) {
            in_col = (int) Mathf.Ceil(in_curFrame / in_gridX);
        }
        if(direction == 1) {
            renderer.material.mainTextureOffset = new Vector2(((in_curFrame) % in_gridX) * f_gridX, in_col * f_gridY);
        }
        else {
            renderer.material.mainTextureOffset = new Vector2((in_gridX + (in_curFrame) % in_gridX) * f_gridX, in_col * f_gridY);
        }
    }

    void OnCollisionEnter(Collision collision) {
        direction *= -1;
    }

    public void resetFrame() {
        in_curFrame = 1;
    }

}

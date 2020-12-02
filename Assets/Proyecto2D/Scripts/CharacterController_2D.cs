using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController_2D : MonoBehaviour
{
    public float f_speed = 5.0f;
    public Texture2D doorOpenTexture;
    public Texture2D doorCloseTecture;
    public AudioClip doorOpenSound;
    public AudioClip getKeySound;
    public AudioClip jumpSound;
    public AudioClip getItemSound;
    public LayerMask layerMask;

    public GameObject canvas;
    public GameObject restartButton;

    private int in_direction;
    private bool b_isJumping;
    private float f_height;
    private float f_lastY;
    private bool b_hasKey;
    private int rockHands;

    Rigidbody rigidbody;
    MeshRenderer renderer;
    AudioSource audio;
    Collider collider;

    SpriteManagerIdle loopSpriteIdle;
    SpriteManagerWalk loopSpriteWalk;
    JumpSprite jumpSprite;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<MeshRenderer>();
        audio = GetComponent<AudioSource>();
        collider = GetComponent<Collider>();
        loopSpriteIdle = GetComponent<SpriteManagerIdle>();
        loopSpriteWalk = GetComponent<SpriteManagerWalk>();
        jumpSprite = GetComponent<JumpSprite>();

        restartButton = Instantiate(canvas) as GameObject;
        restartButton.SetActive(false);

        //Start with no Key
        b_hasKey = false;
        rockHands = 3;
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        f_height = mesh.bounds.size.y * transform.localScale.y;
        f_lastY = transform.position.y;
        b_isJumping = false;
        in_direction = 1;

        loopSpriteIdle.SpriteManagerIdleStart();
        loopSpriteWalk.SpriteManagerWalkStart();
      
        Camera.main.transform.position = new Vector3(transform.position.x,
                    transform.position.y, Camera.main.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!b_isJumping)
        {
            if (Input.GetButton("Horizontal"))
            {
                // Walking
                in_direction = Input.GetAxis("Horizontal") < 0 ? -1 : 1;
                rigidbody.velocity = new Vector3((in_direction * f_speed), rigidbody.velocity.y, 0);
                loopSpriteIdle.resetFrame();
                loopSpriteWalk.updateAnimation(in_direction, renderer.material);
            }
            else
            {
                loopSpriteWalk.resetFrame();
                loopSpriteIdle.updateAnimation(in_direction, renderer.material);
            }
            //Jump
            if (Input.GetButton("Jump"))
            {
                b_isJumping = true;
                audio.volume = 0.3f;
                audio.PlayOneShot(jumpSound);
                loopSpriteIdle.resetFrame();
                loopSpriteWalk.resetFrame();
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, -Physics.gravity.y, 0);
            }
        }
        else
        {
            jumpSprite.updateJumpAnimation(in_direction, rigidbody.velocity.y, renderer.material);
        }
    }

    private void LateUpdate()
    {
        RaycastHit hit;
        Vector3 v3_hit = transform.TransformDirection(-Vector3.up) * (f_height * 0.5f);
        Vector3 v3_right = new Vector3(transform.position.x + (collider.bounds.size.x * 0.45f), transform.position.y, transform.position.z);
        Vector3 v3_left = new Vector3(transform.position.x - (collider.bounds.size.x * 0.45f), transform.position.y, transform.position.z);

        if (Physics.Raycast(transform.position, v3_hit, out hit, 2.5f, layerMask.value))
        {
            b_isJumping = false;
        }
        else if (Physics.Raycast(v3_right, v3_hit, out hit, 2.5f, layerMask.value))
        {
            if (b_isJumping)
            {
                b_isJumping = false;
            }
        }
        else if (Physics.Raycast(v3_left, v3_hit, out hit, 2.5f, layerMask.value))
        {
            if (b_isJumping)
            {
                b_isJumping = false;
            }
        }
        else
        {
            if (!b_isJumping)
            {
                if (Mathf.Floor(transform.position.y) == f_lastY)
                {
                    b_isJumping = false;
                }
                else
                {
                    b_isJumping = true;
                }
            }
        }
        f_lastY = Mathf.Floor(transform.position.y);
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }

    public IEnumerator OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Item")
        {
            rockHands--;
            audio.PlayOneShot(getItemSound);
            Destroy(hit.gameObject);
        }
        if (hit.gameObject.tag == "Key")
        {
            if (!b_hasKey)
            {
                audio.volume = 1.0f;
                audio.PlayOneShot(getKeySound);
                b_hasKey = true;
                Destroy(hit.gameObject);
            }
        }
        if (hit.gameObject.tag == "Door")
        {
            if (b_hasKey && rockHands == 0)
            {
                audio.volume = 1.0f;
                audio.PlayOneShot(doorOpenSound);
                hit.GetComponent<Renderer>().material.mainTexture = doorOpenTexture;
                yield return new WaitForSeconds(1);
                Destroy(gameObject);
                hit.GetComponent<Renderer>().material.mainTexture = doorCloseTecture;
                restartButton.SetActive(true);
            }
        }
    }

}



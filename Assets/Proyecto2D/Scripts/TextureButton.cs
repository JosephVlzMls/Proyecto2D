using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextureButton : MonoBehaviour
{
    public Texture2D normalTexture; //restartButtonOut.png
    public Texture2D rollOverTexture; //restartButtonOver.png
    public AudioClip clickSound; //button_click.aiff

    public GameObject key; //key prefab
    public GameObject Player; //player prefab

    private GameObject canvasIU;

    public void OnMouseEnter()
    {  //Mouse Roll over function
//        GetComponent<GUITexture>().texture = rollOverTexture;
    }

    public void OnMouseExit()
    { //Mouse Roll out function
//        GetComponent<GUITexture>().texture = normalTexture;
    }

    public void OnMouseUp()
    { // IEnumeratorMouse up function

        //GetComponent<AudioSource>().PlayOneShot(clickSound);
        //yield return new WaitForSeconds(1.0f); //Wait for 0.5f secs. until do the next function
        //Create a new Player at the start position by cloning from our prefab
        Instantiate(Player, new Vector3(Player.transform.position.x, Player.transform.position.y, 0.0f), Player.transform.rotation);
        //Create a new key at the start position by cloning from our prefab
        Instantiate(key, new Vector3(key.transform.position.x, key.transform.position.y, 0.0f), key.transform.rotation);
        canvasIU = GameObject.FindWithTag("RestartButton");
        canvasIU.SetActive(false);

    }
}

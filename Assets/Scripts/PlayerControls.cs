using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    // variable for player movement 
    public int speed = 1;
    // variable for speechbubbles 
    public GameObject npcBlue_speechBubble;
    public GameObject npcOrange_speechBubble;
    public GameObject npcBlue_dialogue;
    public GameObject npcOrange_dialogue1;
    public GameObject npcOrange_dialogue2;
    // varibale for collecting the key
    bool haveKey = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //// player movement, WASD controller ////
        // innitilize the position we want the player to be at 
        Vector3 newPos = transform.position;

        // set the target position 
        if (Input.GetKey(KeyCode.W))
        {
            newPos.y += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            newPos.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newPos.y -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newPos.x += speed * Time.deltaTime;
        }

        // setting the new position 
        transform.position = newPos;
    }

    private void OnTriggerEnter2D(Collider2D other) //check once upon entering collision
    {
        // if the player collides with the key, they collect it 
        if (other.gameObject.name == "Key")
        {
            haveKey = true;
            Destroy(other.gameObject);
          // if the player collides with the exit bush, the they get transported out of the game (goes to end screen)
        } else if (other.gameObject.name == "ExitBush")
        {
            SceneManager.LoadScene(1);
        }
    }

    private void OnTriggerStay2D(Collider2D other) //checks once per frame for the collision
    {
        // if the player collides with the NPC or gets within their range, activate the speech bubble 
        if (other.gameObject.name == "oTalkZone_blue")
        {
            Debug.Log("Entered NPC zone");
            // if player presses space within the zone, the speech bubble appears 
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("pressed space");
                npcBlue_speechBubble.SetActive(true);
                npcBlue_dialogue.SetActive(true);
            }
        }

        // if the player collides with the NPC or gets within their range, activate the speech bubble 
        if (other.gameObject.name == "oTalkZone_orange")
        {
            Debug.Log("Entered NPC zone");
            // if player presses space within the zone, the speech bubble appears 
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("pressed space");
                npcOrange_speechBubble.SetActive(true);
                if (haveKey == false)
                {
                    npcOrange_dialogue1.SetActive(true);
                } else if (haveKey == true)
                {
                    npcOrange_dialogue2.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) //checks once exiting the collision 
    {
        // if the player collides with the NPC or gets within their range, activate the speach bubble 
        if (other.gameObject.name == "oTalkZone_blue")
        {
            npcBlue_speechBubble.SetActive(false);
            npcBlue_dialogue.SetActive(false);
        }
        // if the player collides with the NPC or gets within their range, activate the speach bubble 
        if (other.gameObject.name == "oTalkZone_orange")
        {
            npcOrange_speechBubble.SetActive(false);
            npcOrange_dialogue1.SetActive(false);
            npcOrange_dialogue2.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // if the player has the key and collides with the door then open the door 
        if (other.gameObject.name == "Door" && haveKey == true)
        {
            Destroy(other.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    Player playerScript;
    public string powerUpName;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D hitObject)
    {
        if (hitObject.tag == "Player")
        {
            playerScript.setPowerUpState(true, powerUpName);
            Destroy(gameObject);
        }

        if (hitObject.tag == "Ground")
        {
            Destroy(gameObject);
        }

        if (hitObject.tag == "Gamabunta" && powerUpName == "scroll")
        {
            Destroy(gameObject);
        }
    }
}

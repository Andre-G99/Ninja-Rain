using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAfterDelay : MonoBehaviour
{

    AudioSource source;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
      source = GetComponent<AudioSource>();
      anim = GetComponent<Animator>();
      Kill();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Kill()
    {
      //kills object after delay of five seconds
      Invoke("playJumpAnimation", 5);
      source.Play();
      Invoke("playSound", 6.5f);
      Destroy (gameObject, 5f);
    }

    public void playJumpAnimation()
    {
        anim.SetBool("jump", true);
    }

    public void playSound()
    {
      source.Play();
    }
}

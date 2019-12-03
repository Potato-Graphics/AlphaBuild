using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    public AudioSource source;
    public AudioClip hover;
    public AudioClip click;


    // Start is called before the first frame update
    public void OnHover()
    {
        source.PlayOneShot(hover);
    }

    public void Onclick()
    {
        source.PlayOneShot(click);
    }
}

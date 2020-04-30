using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip PlayerFire, PlayerChargedFire, PlayerJump, PlayerDash, PlayerCollect, PlayerDamage, PlayerPump;
    static AudioSource audioGame;


    // Start is called before the first frame update
    void Start()
    {
        PlayerFire = Resources.Load<AudioClip>("playerfire");
        PlayerChargedFire = Resources.Load<AudioClip>("playerchargefire");
        PlayerJump = Resources.Load<AudioClip>("playerjump");
        PlayerDash = Resources.Load<AudioClip>("playerdash");
        PlayerCollect = Resources.Load<AudioClip>("playercollect");
        PlayerDamage = Resources.Load<AudioClip>("playerdamage");
        PlayerPump = Resources.Load<AudioClip>("playerpump");

        audioGame = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "playerfire":
                audioGame.PlayOneShot(PlayerFire);
                break;
         case "playerchargedfire":
                audioGame.PlayOneShot(PlayerChargedFire);
                break;
         case "playerjump":
                audioGame.PlayOneShot(PlayerJump);
                break;
         case "playerDash":
                audioGame.PlayOneShot(PlayerDash);
                break;
         case "playercollect":
                audioGame.PlayOneShot(PlayerCollect);
                break;
            case "playerDamage":
                audioGame.PlayOneShot(PlayerDamage);
                break;
            case 
            "playerpump":
                audioGame.PlayOneShot(PlayerPump);
                break;
        }
    }


}

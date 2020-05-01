using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip PlayerFire, PlayerJump, PlayerDash, PlayerCollect, PlayerDamage, PlayerPump, BubblePop, PlaneDive, FrogCroak, BallBounce, ChargerPatrol, ChargerExplosion;
    static AudioSource audioGame;


    // Start is called before the first frame update
    void Start()
    {
        PlayerFire = Resources.Load<AudioClip>("playerfire");
        PlayerJump = Resources.Load<AudioClip>("playerjump");
        PlayerDash = Resources.Load<AudioClip>("playerdash");
        PlayerCollect = Resources.Load<AudioClip>("playercollect");
        PlayerDamage = Resources.Load<AudioClip>("playerdamage");
        PlayerPump = Resources.Load<AudioClip>("playerpump");
        BubblePop = Resources.Load<AudioClip>("bubblepop");
        PlaneDive = Resources.Load<AudioClip>("planedive");
        FrogCroak = Resources.Load<AudioClip>("frogcroak");
        ChargerPatrol = Resources.Load<AudioClip>("chargerpatrol");
        ChargerExplosion = Resources.Load<AudioClip>("chargerexplosion");


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
         case "playerjump":
                audioGame.PlayOneShot(PlayerJump);
                break;
         case "playerdash":
                audioGame.PlayOneShot(PlayerDash);
                break;
         case "playercollect":
                audioGame.PlayOneShot(PlayerCollect);
                break;
            case "playerdamage":
                audioGame.PlayOneShot(PlayerDamage);
                break;
            case
            "playerpump":
                audioGame.PlayOneShot(PlayerPump);
                break;
            case
            "bubblepop":
                audioGame.PlayOneShot(BubblePop);
                break;
            case
            "planedive":
                audioGame.PlayOneShot(PlaneDive);
                break;
            case
            "frogcroak":
                audioGame.PlayOneShot(FrogCroak);
                break;
            case
            "ballbounce":
                audioGame.PlayOneShot(BallBounce);
                break;
            case
            "chargerpatrol":
                audioGame.PlayOneShot(ChargerPatrol);
                break;
            case
            "chargerexplosion":
                audioGame.PlayOneShot(ChargerExplosion);
                break;
        }
    }


}

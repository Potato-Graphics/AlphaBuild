using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip PlayerFire, PlayerJump, PlayerDash, PlayerCollect, PlayerDamage, PlayerPump, BubblePop, PlaneDive, FrogCroak, BallBounce, ChargerPatrol, ChargerExplosion;
    static AudioSource playerfire, playerjump, playerdash, playercollect, playerdamage, playerpump, bubblepop, planedive, frogcroak, chargerpatrol, chargerexplosion, ballbounce;
   


    // Start is called before the first frame update
    void Start()
    {
        


        playerfire = GetComponent<AudioSource>();
        playerjump = GetComponent<AudioSource>();
        playerdash = GetComponent<AudioSource>();
        playerdamage = GetComponent<AudioSource>();
        playercollect = GetComponent<AudioSource>();
        bubblepop = GetComponent<AudioSource>();
        planedive = GetComponent<AudioSource>();
        frogcroak = GetComponent<AudioSource>();
        chargerpatrol = GetComponent<AudioSource>();
        playerpump = GetComponent<AudioSource>();
        chargerexplosion = GetComponent<AudioSource>();
        


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
                playerfire.Play();
                break; 
         case "playerjump":
                playerjump.Play();
                break;
         case "playerdash":
                playerdash.Play();
                break;
         case "playercollect":
                playercollect.Play();
                break;
            case "playerdamage":
                playerdamage.Play();
                break;
            case
            "playerpump":
                playerpump.Play();
                break;
            case
            "bubblepop":
                bubblepop.Play();
                break;
            case
            "planedive":
                planedive.Play();
                break;
            case
            "frogcroak":
                frogcroak.Play();
                break;
            case
            "ballbounce":
                ballbounce.Play();
                break;
            case
            "chargerpatrol":
                chargerpatrol.Play();
                break;
            case
            "chargerexplosion":
                chargerexplosion.Play();
                break;
        }
    }


}

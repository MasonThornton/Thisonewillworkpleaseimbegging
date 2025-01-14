using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public float Score = 0;
        public float currentScore;
   public AudioSource AudioGod;
    public AudioClip JumpAudio;
    public AudioClip CoinAudio;
    public AudioClip DashAudio;
    public AudioClip ThudAudio;
    public AudioClip BreakAudio;
    public AudioClip ItemAudio;
    public AudioClip DoorAudio;
    public AudioClip MusicAudio;
    public AudioClip LandAudio;

    public bool HasPlunger;
    public bool HasDash;
  
    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        AudioGod = GetComponent<AudioSource>();
 
        DontDestroyOnLoad(gameObject);
        if (PlayerController.dead == true)
        {
            currentScore = Score;
         
        }

   

    }

    public void CoinSound()
    {

        AudioGod.pitch = Random.Range(0.6f, 1.1f);
        AudioGod.PlayOneShot(CoinAudio,1);
      
    }

    public void JumpSound()
    {

        AudioGod.pitch = Random.Range(0.5f, 1f);
        AudioGod.PlayOneShot(JumpAudio, 1);
    }

    public void DashSound()
    {

        AudioGod.pitch = Random.Range(0.9f, 1.5f);
        AudioGod.PlayOneShot(DashAudio, 0.5F);
    }

    public void ItemSound()
    {

        AudioGod.pitch = Random.Range(0.9f, 1.5f);
        AudioGod.PlayOneShot(ItemAudio, 0.25F);
    }
    public void ThudSound()
    {

        AudioGod.pitch = Random.Range(0.7f, 1.5f);
        AudioGod.PlayOneShot(ThudAudio, 1F);
    }
    public void BreakSound()
    {

        AudioGod.pitch = Random.Range(0.7f, 1.5f);
        AudioGod.PlayOneShot(BreakAudio, 1F);
    }
    public void DoorSound()
    {

        AudioGod.pitch = Random.Range(0.7f, 1.5f);
        AudioGod.PlayOneShot(DoorAudio, 1F);
    }

 

    public void LandSound()
    {

        AudioGod.pitch = Random.Range(0.7f, 1.5f);
        AudioGod.PlayOneShot(LandAudio, 10F);
    }
}

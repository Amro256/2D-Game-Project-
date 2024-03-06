using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum SoundFXCat {FootStepConcrete, FootStepWood, Jump, Death, HitGround, HitCeiling, Flag, Squish, OpenDoor, PickupKey, PickupCoin, ExtaLive}
    public GameObject audioObject;
    public AudioClip[] footsteps; //This code and the ones below allow me to assign an audio clip to the manager in the inspector 
    public AudioClip[] ladderSteps;
    public AudioClip[] jumpAudio;
    public AudioClip[] deathAudio;
    public AudioClip[] groundedAudio;
    public AudioClip[] ceilingedAudio;
    public AudioClip[] flagRaiseAudio;
    public AudioClip[] squishAudio;
    public AudioClip[] openDoorAudio;
    public AudioClip[] keyClips;
    public AudioClip[] coinClips;
    public AudioClip[] extaliveClip;

    public void AudioTrigger(SoundFXCat audioType, Vector3 audioPosition, float volume)
    {
        GameObject newAudio = GameObject.Instantiate(audioObject, audioPosition, Quaternion.identity);
        ControlAudio ca = newAudio.GetComponent<ControlAudio>();
        switch (audioType)
        {
            case(SoundFXCat.Death):
            ca.myClip = deathAudio[Random.Range(0, deathAudio.Length)];
            break;
            case(SoundFXCat.Flag):
            ca.myClip = flagRaiseAudio[Random.Range(0, flagRaiseAudio.Length)];
            break;
            case(SoundFXCat.FootStepConcrete):
            ca.myClip = footsteps[Random.Range(0, footsteps.Length)];
            break;
            case(SoundFXCat.FootStepWood):
            ca.myClip = ladderSteps[Random.Range(0, ladderSteps.Length)];
            break;
            case(SoundFXCat.HitCeiling):
            ca.myClip = ceilingedAudio[Random.Range(0, ceilingedAudio.Length)];
            break;
            case(SoundFXCat.HitGround):
            ca.myClip = groundedAudio[Random.Range(0, groundedAudio.Length)];
            break;
            case(SoundFXCat.Jump):
            ca.myClip = jumpAudio[Random.Range(0, jumpAudio.Length)];
            break;
            case(SoundFXCat.OpenDoor):
            ca.myClip = openDoorAudio[Random.Range(0, openDoorAudio.Length)];
            break;
            case(SoundFXCat.PickupCoin):
            ca.myClip = coinClips[Random.Range(0, coinClips.Length)];
            break;
            case(SoundFXCat.PickupKey):
            ca.myClip = keyClips[Random.Range(0, keyClips.Length)];
            break;
            case(SoundFXCat.Squish):
            ca.myClip = squishAudio[Random.Range(0, squishAudio.Length)];
            break;
            case(SoundFXCat.ExtaLive):
            ca.myClip = extaliveClip[Random.Range(0, extaliveClip.Length)];
            break;
        }


        ca.volume  = volume; 
        ca.StartAudio();
    

    }

}

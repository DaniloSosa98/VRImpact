using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetPresbycusis : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetVolume(float level){
        mixer.SetFloat("MusicVol", Mathf.Log10((float)0.5 - level) * 20);
    }
}

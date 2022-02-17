using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class SetTinnitusVolume : MonoBehaviour
{
	public AudioMixer mixer;
	public AudioSource audioSource;
	
	private void Start()
	{
		// initialize the audio track so it doesn't blip on first play
		SetVolume(0.01f);
		SetVolume(0);
	}
	
	public void SetVolume(float level) {
		if (level > 0) // the audio is non-zero
		{
			mixer.SetFloat("TinnitusVol", Mathf.Log10(level) * 20);
			if(!audioSource.isPlaying) // start it if it's not playing
				audioSource.Play();
		}
		// else if the audio level should be 0, and it's currently playing...
		else if(audioSource.isPlaying)
			audioSource.Stop();
	}
}

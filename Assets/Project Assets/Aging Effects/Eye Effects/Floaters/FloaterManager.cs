using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the configuration data and oversight of the floaters on the screen.
///
/// Note that the way this works is that floaters are spawned randomly on the screen, with a random clamped
/// velocity and rotation, then faded in. Upon reaching the edge of the screen, they are faded out, destroyed,
/// and a new one is added on some other edge. While there is a moderate amount of setup involved in creating the floaters,
/// it's nothing worth really considering optimizing it reuse the same floaters
/// </summary>
public class FloaterManager : MonoBehaviour
{
	// the different floater textures available to pick from. Will be selected randomly.
	public Sprite[] floaterTextures;
	// the EyeFloater component on the floater prefab, for easy access to deconstruction later without GetComponents.
	public EyeFloater floaterPrefab;
	
	// how many floaters to display on screen, per level of intensity.
	public int floaterCountPerLevel;
	// speed at which the floaters travel across your vision, as a range.
	public float minSpeed, maxSpeed;
	// speed at which the floaters will rotate as they move. Square bias toward the slower end.
	public float minAngSpeed, maxAngSpeed;
	
	// the amount of time, in seconds, for the floater to fade in or out.
	public float fadeTime;
	
	private Stack<EyeFloater> currentFloaters;
	
	private void Start()
	{
		currentFloaters = new Stack<EyeFloater>();
	}
	
	// checks the floater count against the desired count based on the intensity level,
	// and adjusts it as needed by fading floaters in and out.
	public void UpdateFloaterCount(float newIntensity)
	{
		int effectIntensity = (int) newIntensity;
		int targetAmount = effectIntensity * floaterCountPerLevel;
		if (targetAmount == currentFloaters.Count)
			return;
		
		while (currentFloaters.Count > targetAmount)
			currentFloaters.Pop().DestroyFloater();
		
		while (currentFloaters.Count < targetAmount)
		{
			// add a floater
			currentFloaters.Push(Instantiate(floaterPrefab, transform));
		}
	}
}

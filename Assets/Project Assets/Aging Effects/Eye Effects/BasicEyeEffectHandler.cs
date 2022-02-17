using System;
using UnityEngine;

public class BasicEyeEffectHandler : MonoBehaviour
{
	public GameObject[] effectObjects;
	
	public enum EffectType
	{
		Cataract, Degeneration
	}
	
	public void ToggleEffect(string effectName)
	{
		if (!Enum.TryParse(effectName, out EffectType effect))
			return;
		
		GameObject obj = effectObjects[(int) effect];
		obj.SetActive(!obj.activeSelf);
	}
}

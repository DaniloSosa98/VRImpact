using UnityEngine;

public class AnimationBasedEffectSlider : MonoBehaviour
{
	private static readonly int TargetLevelId = Animator.StringToHash("Target Level");
	
	public Animator animator;
	
	public void UpdateTargetIntensity(float target)
	{
		animator.SetInteger(TargetLevelId, (int) target);
	}
}

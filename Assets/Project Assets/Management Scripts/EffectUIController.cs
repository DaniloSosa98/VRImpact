using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;

public class EffectUIController : MonoBehaviour, XRActionMap.ICustomGameActionsActions
{
	// expects an entry for every EffectType
	// this allows the buttons to be shaded properly according to the active effect
	public Image[] effectButtons;
	
	public GameObject[] uiPanels;
	
	private static readonly int ShowMenuId = Animator.StringToHash("Show Menu");
	private static readonly int ShowTextId = Animator.StringToHash("Show Text");
	
	// for showing/hiding the ui
	public Animator uiAnimatior;
	// the message to fade in for a bit when the menu is closed
	public Animator menuNoticeAnimator;
	
	private enum EffectType
	{
		Vision, Hearing, Muscular
	}
	
	public void SetEffectType(string effectName)
	{
		EffectType effect = (EffectType) Enum.Parse(typeof(EffectType), effectName);
		
		// select the proper button, visually
		for (int i = 0; i < effectButtons.Length; i++)
			effectButtons[i].color = i == (int) effect ? Color.gray : Color.white;
		// set the correct ui panel to be visible
		for(int i = 0; i < uiPanels.Length; i++)
			uiPanels[i].SetActive(i == (int) effect);
	}
	
	private void Start()
	{
		SetEffectType("Vision");
	}
	
	public void ToggleMenu(bool showMenu)
	{
		uiAnimatior.SetBool(ShowMenuId, showMenu);
		menuNoticeAnimator.SetBool(ShowTextId, !showMenu);
	}
	
	private void OnEnable()
	{
		Debug.Log("setting up action map");
		XRActionMap actionMap = new XRActionMap();
		actionMap.CustomGameActions.SetCallbacks(this);
		actionMap.CustomGameActions.Enable();
	}
	
	public void OnMenuPress(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			// if we used the menu button for the first hide, then don't even bother with the notice
			if(menuNoticeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Starting State"))
				menuNoticeAnimator.enabled = false;
			// toggle the menu
			ToggleMenu(!uiAnimatior.GetBool(ShowMenuId));
		}
	}
}

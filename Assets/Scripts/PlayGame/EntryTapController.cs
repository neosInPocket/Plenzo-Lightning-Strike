using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class EntryTapController : MonoBehaviour
{
	private Action tapAction;

	public EntryTapController(Action TapAction)
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();

		tapAction = TapAction;
		Touch.onFingerDown += OnFingerDown;
	}

	private void OnFingerDown(Finger finger)
	{
		tapAction();
		Touch.onFingerDown -= OnFingerDown;
	}

	~EntryTapController()
	{
		Touch.onFingerDown -= OnFingerDown;
	}
}

using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class GamePlayShow : MonoBehaviour
{
	[SerializeField] private string[] textsStrings;
	[SerializeField] private TMP_Text currentText;
	[SerializeField] private Transform pointer;
	[SerializeField] private float pointerAnimationFrequency;
	[SerializeField] private float pointerMagnitude;
	[SerializeField] private Transform arrowPos;
	[SerializeField] private Transform firstPortal;
	[SerializeField] private Transform secondPortal;
	[SerializeField] private Transform ball;
	public Action<int> GamePlayShowEnd { get; set; }
	private Action nextAction;

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}
	public void ShowGameplay()
	{
		gameObject.SetActive(true);

		Touch.onFingerDown += OnNextValue;

		currentText.text = textsStrings[0];
		nextAction = WelcomeToGameValue;
	}

	private void OnNextValue(Finger playerFinger)
	{
		nextAction();
	}

	private void WelcomeToGameValue()
	{
		nextAction = GamePlay1;
		var currentEuler = pointer.transform.eulerAngles;
		currentEuler.z = 270f;
		pointer.transform.eulerAngles = currentEuler;
		pointer.transform.position = ball.transform.position;
		pointer.gameObject.SetActive(true);
		StartCoroutine(PointerAnimation(270f));

		currentText.text = textsStrings[1];
	}

	private void GamePlay1()
	{
		StopAllCoroutines();

		var currentEuler = pointer.transform.eulerAngles;
		currentEuler.z = 180f;
		pointer.transform.eulerAngles = currentEuler;
		pointer.transform.position = firstPortal.transform.position;
		StartCoroutine(PointerAnimation(180f));

		nextAction = Gameplay2;

		currentText.text = textsStrings[2];
		pointer.gameObject.SetActive(true);

	}

	private void Gameplay2()
	{
		nextAction = Gameplay3;

		StopAllCoroutines();
		pointer.transform.position = secondPortal.transform.position;
		StartCoroutine(PointerAnimation(180f));

		currentText.text = textsStrings[3];
	}

	private void Gameplay3()
	{
		nextAction = Gameplay4;

		StopAllCoroutines();
		pointer.gameObject.SetActive(false);
		currentText.text = textsStrings[4];
	}

	private void Gameplay4()
	{
		nextAction = Gameplay5;

		currentText.text = textsStrings[5];
	}

	private void Gameplay5()
	{
		nextAction = Gameplay6;

		currentText.text = textsStrings[6];
	}

	private void Gameplay6()
	{
		nextAction = Gameplay7;

		currentText.text = textsStrings[7];
	}

	private void Gameplay7()
	{
		nextAction = LastGamePlay;

		currentText.text = textsStrings[8];
	}

	private void LastGamePlay()
	{
		GamePlayShowEnd?.Invoke(132);
		gameObject.SetActive(false);
		Touch.onFingerDown -= OnNextValue;
	}

	private IEnumerator PointerAnimation(float degAngle)
	{
		var angle = Mathf.Deg2Rad * degAngle;
		Vector2 startPointerPos = pointer.transform.position;
		Vector2 currentPointerPos = pointer.transform.position;
		Vector2 stepVector = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		float time = Time.time;
		float value = 0;
		while (true)
		{
			value = pointerMagnitude * Mathf.Sin(time * pointerAnimationFrequency) - pointerMagnitude;
			time += Time.deltaTime;

			pointer.transform.position = startPointerPos + value * stepVector;
			yield return null;
		}

	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnNextValue;
	}
}

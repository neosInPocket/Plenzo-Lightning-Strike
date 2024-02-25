using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Random = UnityEngine.Random;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PortalsController : MonoBehaviour
{
	[SerializeField] private List<Portal> portals;
	[SerializeField] private PortalBall portalBall;
	[SerializeField] private float firstPortalYSpawnValue;
	[SerializeField] private float secondPortalYSpawnValue;
	[SerializeField] private Vector2 xyScreenThresholds;
	private Action OnDeath;

	public void Initialize()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();

		portalBall.SubscribeOnPortal(OnPlayerEnteredPortal);
		portalBall.Initialize();

		float ratio = (float)Screen.height / (float)Screen.width;
		var ortho = Camera.main.orthographicSize;
		var screenSize = new Vector2(ortho / ratio, ortho);

		portals[0].Show(true, new Vector2(0, 2 * screenSize.y * firstPortalYSpawnValue - screenSize.y));
		portals[1].Show(true, new Vector2(0, 2 * screenSize.y * secondPortalYSpawnValue - screenSize.y));
	}

	public void SubscribeOnDeath(Action onDeath)
	{
		OnDeath = onDeath;
	}

	public void SetEnabled(bool value)
	{
		if (value)
		{
			Touch.onFingerDown += OnPlayerFingerHandler;

		}
		else
		{
			Touch.onFingerDown -= OnPlayerFingerHandler;
		}
	}

	public void StartAction()
	{
		ShootBall(portals[0].transform.position - portalBall.transform.position);
	}

	private void OnPlayerFingerHandler(Finger finger)
	{
		if (portals == null) return;
		var freePortal = portals.FirstOrDefault(x => !x.gameObject.activeSelf);

		if (freePortal != null)
		{
			float ratio = (float)Screen.height / (float)Screen.width;
			var ortho = Camera.main.orthographicSize;
			var screenSize = new Vector2(ortho / ratio, ortho);

			var xFactor = finger.screenPosition.x / Screen.width;
			var yFactor = finger.screenPosition.y / Screen.height;

			Vector2 spawnPosition = new Vector2(2 * screenSize.x * xFactor - screenSize.x, 2 * screenSize.y * yFactor - screenSize.y);
			freePortal.Show(true, spawnPosition);
		}
		else
		{
			return;
		}
	}

	private void OnPlayerEnteredPortal(Portal onPortal)
	{
		var randomNumber = Random.Range(0, 2 * Mathf.PI);
		var randomX = Mathf.Cos(randomNumber);
		var randomY = Mathf.Sin(randomNumber);
		var otherPortal = portals.FirstOrDefault(x => x != onPortal && x.gameObject.activeSelf);

		if (otherPortal != null)
		{
			portalBall.transform.position = otherPortal.transform.position;
			ShootBall(-onPortal.transform.position + otherPortal.transform.position);
			onPortal.OnPass();
			otherPortal.OnPass();
		}
		else
		{
			OnDeath();
		}

		DisableAllPortals();
	}

	public void ShootBall(Vector2 direction)
	{
		portalBall.SetSpeedNormalized(direction);
	}

	private void DisableAllPortals()
	{
		foreach (var portal in portals)
		{
			portal.Show(false);
		}
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnPlayerFingerHandler;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class PortalsController : MonoBehaviour
{
	[SerializeField] private List<Portal> portals;
	[SerializeField] private PortalBall portalBall;
	[SerializeField] private float firstPortalYSpawnValue;

	private Portal currentPortal;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();

		portals[0].Show(false);

		float ratio = (float)Screen.height / (float)Screen.width;
		var ortho = Camera.main.orthographicSize;
		var screenSize = new Vector2(ortho / ratio, ortho);

		portals[1].Show(true, new Vector2(0, 2 * screenSize.y * firstPortalYSpawnValue - screenSize.y));
	}

	public void ShootBall()
	{

	}
}

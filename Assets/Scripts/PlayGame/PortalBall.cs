using System;
using System.Collections;
using UnityEngine;

public class PortalBall : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Rigidbody2D rigidbody2;
	[SerializeField] private GameObject deathEffect;
	[SerializeField] private float spawnYValue;
	[SerializeField] private float[] levelSpeeds;
	private float speed;

	private Action OnCrash;
	private Action<Portal> OnPortal;

	public void Initialize()
	{
		speed = levelSpeeds[DataCaptureController.Capture.speedUpgradeLevel];

		float ratio = (float)Screen.height / (float)Screen.width;
		var ortho = Camera.main.orthographicSize;
		var screenSize = new Vector2(ortho / ratio, ortho);

		transform.position = new Vector2(0, 2 * screenSize.y * spawnYValue - screenSize.y);
	}

	public void SubscribeCrash(Action onCrash)
	{
		OnCrash += onCrash;
	}

	public void SubscribeOnPortal(Action<Portal> onPortal)
	{
		OnPortal += onPortal;
	}

	public void ClearAllSubscribers()
	{
		OnPortal = null;
		OnCrash = null;
	}

	public void SetSpeedNormalized(Vector2 speed)
	{
		rigidbody2.velocity = speed.normalized * this.speed;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<Portal>(out Portal portal))
		{
			OnPortal?.Invoke(portal);
			return;
		}

		if (collider.TryGetComponent<InvisibleEdge>(out InvisibleEdge edge))
		{
			Lose();
			return;
		}

		if (collider.TryGetComponent<Meteor>(out Meteor meteor))
		{
			Lose();
			return;
		}
	}

	private void Lose()
	{
		spriteRenderer.enabled = false;
		rigidbody2.velocity = Vector2.zero;
		rigidbody2.angularVelocity = 0;
		rigidbody2.constraints = RigidbodyConstraints2D.FreezeAll;
		deathEffect.SetActive(true);

		OnCrash?.Invoke();
	}
}

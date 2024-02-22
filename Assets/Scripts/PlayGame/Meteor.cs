using System.Collections;
using UnityEngine;

public class Meteor : MonoBehaviour
{
	[SerializeField] private CircleCollider2D circleCollder;
	[SerializeField] private Vector2 speedsRange;
	[SerializeField] private Vector2 rotationSpeedsRange;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private GameObject explosionEffect;
	private float currentFallSpeed;
	private float currentRotationSpeed;
	private Vector3 currentEuler;
	public bool Avaliable { get; set; }
	public float Radius => circleCollder.radius;

	public void Enable()
	{
		rb.constraints = RigidbodyConstraints2D.None;
		currentFallSpeed = Random.Range(speedsRange.x, speedsRange.y);
		currentRotationSpeed = Random.Range(rotationSpeedsRange.x, rotationSpeedsRange.y);

		rb.velocity = Vector2.down * currentFallSpeed;
	}

	private void Update()
	{
		currentEuler.z += currentRotationSpeed * Time.deltaTime;
		transform.eulerAngles = currentEuler;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<InvisibleEdge>(out InvisibleEdge edge))
		{
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			StartCoroutine(ExplosionCoroutine());
		}
	}

	private IEnumerator ExplosionCoroutine()
	{
		Avaliable = false;
		explosionEffect.SetActive(true);
		yield return new WaitForSeconds(1);
		explosionEffect.SetActive(false);
		Avaliable = true;
		gameObject.SetActive(false);
	}

	public void Disable()
	{
		gameObject.SetActive(false);
	}
}

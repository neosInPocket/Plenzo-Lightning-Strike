using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
	[SerializeField] private GameObject passEffect;

	public void Show(bool value, Vector2 position = new Vector2())
	{
		transform.position = position;
		gameObject.SetActive(value);

	}

	public void OnPass()
	{
		StartCoroutine(onPass());
	}

	private IEnumerator onPass()
	{
		var effect = Instantiate(passEffect, transform.position, Quaternion.identity, null);
		effect.gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);
		Destroy(effect.gameObject);
		gameObject.SetActive(false);
	}
}

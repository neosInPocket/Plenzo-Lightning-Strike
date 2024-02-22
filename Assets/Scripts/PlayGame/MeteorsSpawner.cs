using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeteorsSpawner : MonoBehaviour
{
	[SerializeField] private Meteor[] prefabs;
	[SerializeField] private int initialCount;
	[SerializeField] private Vector2 spawnDelays;
	[SerializeField] private float spawnXDelta;
	private List<Meteor> currentMeteors;

	public bool Enabled { get; set; }
	private bool spawnProcess;
	private Vector2 screenSize;

	private void Start()
	{
		float ratio = (float)Screen.height / (float)Screen.width;
		var ortho = Camera.main.orthographicSize;
		screenSize = new Vector2(ortho / ratio, ortho);

		for (int i = 0; i < initialCount; i++)
		{
			var meteor = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);
			currentMeteors.Add(meteor);
			meteor.Disable();
		}
	}

	public void Enable(bool value)
	{
		Enabled = value;

		if (!value)
		{
			StopAllCoroutines();
		}
	}

	public void SpawnOne()
	{
		Meteor avaliable = currentMeteors.FirstOrDefault(x => x.Avaliable);

		if (avaliable == null)
		{
			avaliable = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);
			currentMeteors.Add(avaliable);
			avaliable.Enable();
		}

		float randomXPos = Random.Range(-screenSize.x + spawnXDelta + avaliable.Radius, screenSize.x - spawnXDelta - avaliable.Radius);
		float yPos = screenSize.y + avaliable.Radius + spawnXDelta;
		avaliable.transform.position = new Vector2(randomXPos, yPos);
	}

	private void Update()
	{
		if (!Enabled) return;
		if (spawnProcess) return;
		spawnProcess = true;

		StartCoroutine(SpawnProcess());
	}

	private IEnumerator SpawnProcess()
	{
		SpawnOne();
		yield return new WaitForSeconds(Random.Range(spawnDelays.x, spawnDelays.y));

		spawnProcess = false;
	}
}

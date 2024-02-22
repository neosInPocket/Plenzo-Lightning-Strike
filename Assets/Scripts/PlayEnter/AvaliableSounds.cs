using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AvaliableSounds : MonoBehaviour
{
	[SerializeField] private AudioSource source;

	private void Awake()
	{
		var capturing = FindObjectsByType<AvaliableSounds>(sortMode: FindObjectsSortMode.None);

		try
		{
			var dontDestroy = capturing.First(x => x.gameObject.scene.name == "DontDestroyOnLoad");

			if (dontDestroy != this)
			{
				Destroy(gameObject);
				return;
			}
		}
		catch
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start()
	{
		SetVolumeEnabled(DataCaptureController.Capture.volumeAvaliable);
	}

	public void SetVolumeEnabled(bool valueEnabled)
	{
		source.enabled = valueEnabled;
	}
}

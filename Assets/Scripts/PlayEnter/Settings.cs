using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
	[SerializeField] private Image musicButton;
	[SerializeField] private Image effectsButton;

	private AvaliableSounds activeSound;

	private void Start()
	{
		activeSound = FindFirstObjectByType<AvaliableSounds>();

		musicButton.color = DataCaptureController.Capture.volumeAvaliable ? Color.white : Color.red;
		effectsButton.color = DataCaptureController.Capture.effectsAvaliable ? Color.white : Color.red;
	}

	public void EnableMusic()
	{
		bool enabled = musicButton.color == Color.white;
		musicButton.color = enabled ? Color.red : Color.white;

		activeSound.SetVolumeEnabled(!enabled);

		DataCaptureController.Capture.volumeAvaliable = !enabled;
		DataCaptureController.Save();
	}

	public void EnableEffects()
	{
		bool enabled = effectsButton.color == Color.white;
		effectsButton.color = enabled ? Color.red : Color.white;

		DataCaptureController.Capture.effectsAvaliable = !enabled;
		DataCaptureController.Save();
	}
}

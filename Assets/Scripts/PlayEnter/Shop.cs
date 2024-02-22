using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
	[SerializeField] private Image fillImage0;
	[SerializeField] private Image fillImage1;
	[SerializeField] private Button button0;
	[SerializeField] private Button button1;
	[SerializeField] private TMP_Text normalText0;
	[SerializeField] private TMP_Text normalText1;
	[SerializeField] private CurrentCoins[] allCurrentCoins;
	[SerializeField] private int cost0;
	[SerializeField] private int cost1;

	private void Start()
	{
		Restore();
	}

	private void Restore()
	{
		for (int i = 0; i < allCurrentCoins.Length; i++)
		{
			allCurrentCoins[i].UpdateData();
		}

		if (DataCaptureController.Capture.speedUpgradeLevel >= 10)
		{
			normalText0.color = Color.white;
			button0.interactable = false;
			normalText0.text = "PURCHASED";
		}
		else
		{
			if (DataCaptureController.Capture.goldCoins >= cost0)
			{
				normalText0.color = Color.white;
				button0.interactable = true;
				normalText0.text = "PURCHASE";
			}
			else
			{
				normalText0.color = Color.red;
				button0.interactable = false;
				normalText0.text = "NO COINS";
			}
		}

		if (DataCaptureController.Capture.upgrade1 >= 10)
		{
			normalText1.color = Color.white;
			button1.interactable = false;
			normalText1.text = "PURCHASED";
		}
		else
		{
			if (DataCaptureController.Capture.goldCoins >= cost1)
			{
				normalText1.color = Color.white;
				button1.interactable = true;
				normalText1.text = "PURCHASE";
			}
			else
			{
				normalText1.color = Color.red;
				button1.interactable = false;
				normalText1.text = "NO COINS";
			}
		}

		fillImage0.fillAmount = (float)DataCaptureController.Capture.speedUpgradeLevel / 10f;
		fillImage1.fillAmount = (float)DataCaptureController.Capture.upgrade1 / 10f;
	}

	public void Purchase0()
	{
		DataCaptureController.Capture.speedUpgradeLevel++;
		DataCaptureController.Capture.goldCoins -= cost0;
		DataCaptureController.Save();

		Restore();
	}

	public void Purchase1()
	{
		DataCaptureController.Capture.upgrade1++;
		DataCaptureController.Capture.goldCoins -= cost1;
		DataCaptureController.Save();

		Restore();
	}
}

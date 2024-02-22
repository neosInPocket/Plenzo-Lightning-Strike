using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Image currentProgressFillImage;
	[SerializeField] private TMP_Text currentProgressText;
	[SerializeField] private GamePlayShow gamePlayShow;
	[SerializeField] private ResultCapture resultCapture;
	[SerializeField] private PortalsController portalsController;
	[SerializeField] private MeteorsSpawner meteorsSpawner;
	[SerializeField] private PortalBall portalBall;
	private int rewardCoin;
	private int maximum;
	private int current;
	private int portalCount;

	private void Start()
	{


		// if (DataCaptureController.Capture.tutorialAvaliable)
		// {
		// 	DataCaptureController.Capture.tutorialAvaliable = false;
		// 	DataCaptureController.Save();

		// 	gamePlayShow.GamePlayShowEnd += OnGamePlayShowEnd;
		// 	gamePlayShow.ShowGameplay();
		// }
		// else
		// {
		// 	OnGamePlayShowEnd(0);
		// }

		// GetLevelStatistics();
		// RefreshBars();

		meteorsSpawner.Enable(true);
	}

	private void OnGamePlayShowEnd(int currentIndex)
	{

	}

	private void OnEdgeHit()
	{
		EndLoseGame();
	}

	private void EndLoseGame()
	{
		resultCapture.GetResultWindow(portalCount, false, 0);
	}

	private void EndWinGame()
	{
		resultCapture.GetResultWindow(portalCount, true, rewardCoin);

		DataCaptureController.Capture.goldCoins += rewardCoin;
		DataCaptureController.Capture.level += 1;
		DataCaptureController.Save();
	}

	private void OnPortal(Portal portal)
	{
		if (current >= maximum)
		{
			EndWinGame();
		}

		RefreshBars();
	}

	private void RefreshBars()
	{
		currentProgressFillImage.fillAmount = (float)current / (float)maximum;
		currentProgressText.text = current.ToString() + "/" + maximum.ToString();
	}

	private void GetLevelStatistics()
	{
		var x = (float)DataCaptureController.Capture.level;
		maximum = 3 * (int)Mathf.Sqrt(x);
		rewardCoin = 1;
	}
}

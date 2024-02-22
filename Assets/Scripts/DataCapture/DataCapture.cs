using System;

[Serializable]
public class DataCapture
{
	public int level;
	public int goldCoins;
	public int upgrade0;
	public int upgrade1;
	public bool volumeAvaliable;
	public bool effectsAvaliable;
	public bool tutorialAvaliable;

	public DataCapture DefaultData()
	{
		var capture = new DataCapture();
		capture.level = 1;
		capture.goldCoins = 1003;
		capture.upgrade0 = 0;
		capture.upgrade1 = 0;
		capture.volumeAvaliable = true;
		capture.effectsAvaliable = true;
		capture.tutorialAvaliable = true;
		return capture;
	}
}

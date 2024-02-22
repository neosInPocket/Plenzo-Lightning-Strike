using System.IO;
using UnityEngine;

public class DataCaptureController : MonoBehaviour
{
	[SerializeField] private bool removePresets;
	private static string captureFilePath => Application.persistentDataPath + "/CaptureFile.json";
	public static DataCapture Capture;

	private void Awake()
	{
		if (!removePresets)
		{
			LoadExistingData();
		}
		else
		{
			LoadDefaults();
		}
	}

	public static void Save()
	{
		if (File.Exists(captureFilePath))
		{
			WriteFile();

		}
		else
		{
			NewCapture();
		}
	}

	private void LoadExistingData()
	{
		if (File.Exists(captureFilePath))
		{
			LoadFile();

		}
		else
		{
			NewCapture();
		}
	}

	private void LoadDefaults()
	{
		NewCapture();
	}

	private static void NewCapture()
	{
		Capture = new DataCapture().DefaultData();
		File.WriteAllText(captureFilePath, JsonUtility.ToJson(Capture, prettyPrint: true));
	}

	private static void WriteFile()
	{
		File.WriteAllText(captureFilePath, JsonUtility.ToJson(Capture, prettyPrint: true));
	}

	private static void LoadFile()
	{
		string jsonFile = File.ReadAllText(captureFilePath);
		Capture = JsonUtility.FromJson<DataCapture>(jsonFile);
	}
}

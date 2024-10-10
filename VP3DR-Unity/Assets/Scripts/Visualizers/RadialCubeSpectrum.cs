using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RadialCubeSpectrum : MonoBehaviour
{
	public SpectrumScriptable spectrumDisplayData;
	[Range(64, 8192)]
	public int sampleSize = 512;
	public float radius = 20f;
	public float maxScale = 20f;
	public SpectrumAnalyzer spectrumAnalyzer;
	[Header("Debug Variables")]
	[ReadOnly]
	[SerializeField]
	private float rotationAngle = 0f;
	[ReadOnly]
	[SerializeField]
	private List<GameObject> displayObjects = new List<GameObject>();
	private void Start()
	{
		Initialize();
	}
#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Handles.color = Color.green;
		Handles.DrawWireDisc(transform.position, transform.up, radius);
	}
#endif
	private void Initialize()
	{
		if (spectrumAnalyzer == null)
		{
			spectrumAnalyzer = GetComponent<SpectrumAnalyzer>();
		}
		// Break on not being able to initialize
		if (!CheckNeeded())
		{
			return;
		}
		// Check for existing displayObjects and clear them
		if (displayObjects.Count > 0)
		{
			// Destroy all objects in the list and clear the list
			foreach (GameObject displayObject in displayObjects)
			{
				Destroy(displayObject);
			}
			displayObjects.Clear();
		}
		// Set rotation angle
		rotationAngle = (360f / sampleSize);
		// Create new objects for displaying
		for (int i = 0; i < sampleSize; i++)
		{
			// Create object and prepare for translations
			GameObject newDisplayedObject = Instantiate(spectrumDisplayData.displayPrefab);
			newDisplayedObject.transform.SetParent(transform, false);
			newDisplayedObject.name += $" {i}";
			// Rotate parent transform for easy object placement
			transform.eulerAngles = new Vector3(0, -rotationAngle * i, 0);
			newDisplayedObject.transform.position = Vector3.forward * radius;
			// Add to the objects list
			displayObjects.Add(newDisplayedObject);
		}
	}
	private void Update()
	{
		// Break on not having all that's needed
		if (!CheckNeeded() || displayObjects.Count <= 0)
		{
			return;
		}
		// Keep a reference of the current Analyzed FFT array this cycle
		float[] spectrumValues = spectrumAnalyzer.GetSpectrum(sampleSize);
		// Check for a change in displayObject count and spectrum count
		if (displayObjects.Count != spectrumValues.Length)
		{
			Initialize();
		}
		// Scale all objects in the direction given in the spectrumData
		for (int i = 0; i < spectrumValues.Length; i++)
		{
			Vector3 scaledAmount = spectrumDisplayData.amplitudeEffectDirection * (spectrumValues[i] * maxScale);
			displayObjects[i].transform.localScale = spectrumDisplayData.displayPrefab.transform.localScale + scaledAmount;
		}
	}
	private bool CheckNeeded()
	{
		try
		{
			return spectrumAnalyzer != null && spectrumDisplayData != null && spectrumDisplayData.displayPrefab != null;
		}
		catch {
			return false;
		}
	}
}

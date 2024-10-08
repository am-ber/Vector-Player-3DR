using System;
using UnityEngine;
using UnityEngine.Audio;

public class SpectrumAnalyzer : MonoBehaviour
{
	public bool UseMicrophone = true;
	[SerializeField]
	private AudioMixerGroup mixerGroup = null;
	[ReadOnly]
	[SerializeField]
	private string currentInput;
	[SerializeField]
	private AudioSource audioSource;
	[SerializeField]
	private FFTWindow currentWindow = FFTWindow.Hamming;
	private void Start()
	{
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.outputAudioMixerGroup = mixerGroup;
		// Set to the default device to start with.
		SetInput(Microphone.devices[0]);
	}
	private void Update()
	{
		float[] spectrum = GetSpectrum();
		for (int i = 1; i < spectrum.Length - 1; i++)
		{
			Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
			Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
			Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
			Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
		}
	}
	public void SetInput(string input)
	{
		try
		{
			currentInput = input;
			audioSource.clip = Microphone.Start(currentInput, true, 10, AudioSettings.outputSampleRate);
			audioSource.Play();
		}
		catch (Exception e)
		{
			Debug.LogError($"Error while setting the input:\t{e.Message}\n{e.StackTrace}");
		}
	}
	public float[] GetSpectrum(int fftSize = 256)
	{
		if ((fftSize == 0) || ((fftSize & (fftSize - 1)) != 0))
		{
			return null;
		}
		float[] result = new float[fftSize];
		audioSource.GetSpectrumData(result, 0, currentWindow);
		return result;
	}
}

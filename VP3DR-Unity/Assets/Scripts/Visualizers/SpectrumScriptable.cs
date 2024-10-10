using UnityEngine;

[CreateAssetMenu(fileName = "Spectrum", menuName = "Scriptable Objects/Standard Spectrum")]
public class SpectrumScriptable : ScriptableObject
{
	public GameObject displayPrefab;
	public Vector3 amplitudeEffectDirection = Vector3.up;
}

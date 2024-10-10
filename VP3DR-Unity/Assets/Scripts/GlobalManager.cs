using UnityEngine;

public class GlobalManager : MonoBehaviour
{

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}

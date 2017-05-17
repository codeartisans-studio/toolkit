using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Toolkit.Settings
{
	public class AdditiveScenesSetting : MonoBehaviour
	{
		public string[] scenes;

		void Awake ()
		{
			for (int i = 0; i < scenes.Length; i++) {
				SceneManager.LoadScene (scenes [i], LoadSceneMode.Additive);
			}
		}
	}
}

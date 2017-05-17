using UnityEngine;
using System.Collections;

namespace Toolkit.Sprites
{
	public class SpriteFading : MonoBehaviour
	{
		public float fadeSpeed = 2f;
		public bool changeChildren = true;

		private float alpha = 1f;
		private int fadeDir = 1;

		void Awake ()
		{
			SpriteRenderer sr = GetComponentInChildren<SpriteRenderer> ();
			alpha = sr.color.a;
		}

		public float Alpha {
			get {
				return alpha;
			}
			set {
				value = Mathf.Clamp01 (value);
			
				if (alpha != value) {
					alpha = value;

					if (changeChildren) {
						foreach (SpriteRenderer s in GetComponentsInChildren <SpriteRenderer> ()) {
							s.color = new Color (s.color.r, s.color.g, s.color.b, alpha);
							s.enabled = (alpha != 0);
						}
					} else {
						SpriteRenderer s = GetComponent<SpriteRenderer> ();
						s.color = new Color (s.color.r, s.color.g, s.color.b, alpha);
						s.enabled = (alpha != 0);
					}
				}
			}
		}

		// Update is called once per frame
		void Update ()
		{
			Alpha += fadeDir * fadeSpeed * Time.deltaTime;
		}

		public void ToggleFade ()
		{
			fadeDir *= -1;
		}

		public void FadeIn ()
		{
			fadeDir = 1;
		}

		public void FadeOut ()
		{
			fadeDir = -1;
		}

		public void Show ()
		{
			Alpha = 1;
			fadeDir = 1;
		}

		public void Hide ()
		{
			Alpha = 0;
			fadeDir = -1;
		}
	}
}

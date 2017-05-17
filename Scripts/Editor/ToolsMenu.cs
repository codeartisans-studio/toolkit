using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Toolkit.Editor
{
	public class ToolsMenu
	{
		[MenuItem ("Tools/PlayerPrefs Tools/Delete All")]
		private static void DeleteAll ()
		{
			PlayerPrefs.DeleteAll ();
		}

		[MenuItem ("Tools/Transform Tools/Align With Ground %t")]
		static void AlignWithGround ()
		{
			Transform[] transforms = Selection.transforms;
			foreach (Transform myTransform in transforms) {
				RaycastHit hit;
				if (Physics.Raycast (myTransform.position, -Vector3.up, out hit)) {
					Vector3 targetPosition = hit.point;
					if (myTransform.gameObject.GetComponent<MeshFilter> () != null) {
						Bounds bounds = myTransform.gameObject.GetComponent<MeshFilter> ().sharedMesh.bounds;
						targetPosition.y += bounds.extents.y;
					}
					myTransform.position = targetPosition;
					Vector3 targetRotation = new Vector3 (hit.normal.x, myTransform.eulerAngles.y, hit.normal.z);
					myTransform.eulerAngles = targetRotation;

					EditorSceneManager.MarkSceneDirty (EditorSceneManager.GetActiveScene ());
				}
			}
		}
	}
}

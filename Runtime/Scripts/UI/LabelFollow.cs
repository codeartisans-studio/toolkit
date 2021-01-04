using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Toolkit
{
    [AddComponentMenu("Toolkit/UI/Label Follow")]
    public class LabelFollow : MonoBehaviour
    {
        public Transform target;
        public Text text;
        public Vector3 offset = new Vector3(0, 5, 0);

        private Camera mainCamera;

        // Start is called before the first frame update
        void Start()
        {
            mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 targetPosition = mainCamera.WorldToScreenPoint(target.position) + offset;
            Vector3 labelPosition = targetPosition;
            labelPosition.x = Mathf.Clamp(targetPosition.x, 0, Screen.width);
            labelPosition.y = Mathf.Clamp(targetPosition.y, 0, Screen.height);
            transform.position = labelPosition;
            transform.rotation = Quaternion.FromToRotation(Vector3.down, targetPosition - labelPosition);
            text.transform.rotation = Quaternion.identity;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Toolkit
{
    [AddComponentMenu("Toolkit/UI/LabelFollow")]
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
            Vector3 position = mainCamera.WorldToScreenPoint(target.position) + offset;
            transform.position = new Vector3(Mathf.Clamp(position.x, 0, Screen.width), Mathf.Clamp(position.y, 0, Screen.height), position.z);
            transform.rotation = Quaternion.FromToRotation(Vector3.down, position - transform.position);
            text.transform.rotation = Quaternion.identity;
        }
    }
}

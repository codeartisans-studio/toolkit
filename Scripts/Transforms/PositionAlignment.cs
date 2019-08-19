using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolkit.Cameras;

namespace Toolkit
{
    [AddComponentMenu("Toolkit/Transforms/PositionAlignment")]
    public class PositionAlignment : MonoBehaviour
    {
        public SpriteAlignment alignment = SpriteAlignment.Center;

        // Use this for initialization
        void Start()
        {
            Camera camera = Camera.main;

            Vector2 baseResolution = camera.GetComponent<CameraAdjustment>().baseResolution;
            Vector2 offset = new Vector2(camera.orthographicSize * camera.aspect - baseResolution.x / 2, camera.orthographicSize - baseResolution.y / 2);
            Vector2 anchor;

            switch (alignment)
            {
                case SpriteAlignment.Center:
                    anchor = Vector2.zero;
                    break;
                case SpriteAlignment.TopLeft:
                    anchor = new Vector2(-1, 1);
                    break;
                case SpriteAlignment.TopCenter:
                    anchor = Vector2.up;
                    break;
                case SpriteAlignment.TopRight:
                    anchor = new Vector2(1, 1);
                    break;
                case SpriteAlignment.LeftCenter:
                    anchor = Vector2.left;
                    break;
                case SpriteAlignment.RightCenter:
                    anchor = Vector2.right;
                    break;
                case SpriteAlignment.BottomLeft:
                    anchor = new Vector2(-1, -1);
                    break;
                case SpriteAlignment.BottomCenter:
                    anchor = Vector2.down;
                    break;
                case SpriteAlignment.BottomRight:
                    anchor = new Vector2(1, -1);
                    break;
                default:
                    Debug.AssertFormat(false, transform, "Unsupported SpriteAlignment: {0}", alignment);
                    return;
            }

            transform.position += new Vector3(offset.x * anchor.x, offset.y * anchor.y, 0);
        }
    }
}

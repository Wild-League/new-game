using UnityEngine;

namespace Project.Scripts.UI
{
    public class ScreenFitter : MonoBehaviour
    {
        private Camera _camera;

        void Awake()
        {
            _camera = GetComponent<Camera>();
            int res;

#if UNITY_EDITOR
            UnityEditor.PlayModeWindow.GetRenderingResolution(out uint width, out uint height);
            res = (int)width / (int)height;
#else
            res = Screen.currentResolution.width / Screen.currentResolution.height;
#endif

            _camera.orthographicSize = res == 16 / 9 ? 4.5f : 5f;
        }
    }
}
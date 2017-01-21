using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    [CustomEditor(typeof(MenuScreen))]
    public class EditorScreenTesting : Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var screen = (MenuScreen)target;
            if (GUILayout.Button("Slide Out right"))
            {
                screen.SlideOut(SlideDirection.right);
            }
            if (GUILayout.Button("Slide Out left"))
            {
                screen.SlideOut(SlideDirection.left);
            }
            if (GUILayout.Button("Slide Out top"))
            {
                screen.SlideOut(SlideDirection.top);
            }
            if (GUILayout.Button("Slide In"))
            {
                screen.SlideIn();
            }
        }
    }
}
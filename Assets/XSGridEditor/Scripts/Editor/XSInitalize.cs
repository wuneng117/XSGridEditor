using UnityEditor;
using UnityEngine;

namespace XSSLG
{
    public class XSInitalize
    {
        protected XSInitalize() {}

        [InitializeOnLoadMethod]
        static void Initialize()
        {
            UnityEditor.SceneManagement.EditorSceneManager.sceneOpened += OnEditorSceneManagerSceneOpened;
        }

        protected static void OnEditorSceneManagerSceneOpened(UnityEngine.SceneManagement.Scene scene, UnityEditor.SceneManagement.OpenSceneMode mode)
        {
            // 重置一些场景相关的界面
            
            
            if (scene.name == "Demo_1")
            {
                XSAssetPostprocessor.CheckLayer();
            }
        }
    }
}

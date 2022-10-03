using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    public class Tools: UnityEditor.Editor
    {
        [MenuItem("Tools/Clear PlayerPrefs")]
        private static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}

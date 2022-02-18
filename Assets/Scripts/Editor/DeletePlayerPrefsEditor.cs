 using UnityEngine;
 using UnityEditor;

 public class DeletePlayerPrefsEditor : EditorWindow
 {
     [MenuItem("Window/Delete PlayerPrefs (All)")]
     static void DeleteAllPlayerPrefs()
     {
         PlayerPrefs.DeleteAll();
     }
 }

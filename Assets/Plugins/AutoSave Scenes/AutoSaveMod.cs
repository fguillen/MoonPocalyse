using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using UnityEditorInternal;
using UnityEngine.UIElements;
using System.Reflection;
using System.Collections.Generic;

namespace EM.AutoSave.Editor
{




    class Root
    {
        internal static string WIKI_2 = "https://emem.store/wiki?AutoSave%20Free&%5B%20Getting%20Started&Usage";

        static internal bool USE_AUTOSAVE_MOD {
            get { return GET("USE_AUTOSAVE_MOD", true); }
            set {
                var r = USE_AUTOSAVE_MOD; SET("USE_AUTOSAVE_MOD", value);
                AutoSaveHelper.Subscribe();
            }
        }

        static internal int AS_SAVE_INTERVAL_IN_MIN { get { return Mathf.Clamp(GET("AS_SAVE_INTERVAL_IN_MIN", 5), 1, 60); } set { var r = AS_SAVE_INTERVAL_IN_MIN; SET("AS_SAVE_INTERVAL_IN_MIN", value); } }
        static internal bool AS_LOG { get { return GET("AS_LOG", false); } set { var r = AS_LOG; SET("AS_LOG", value); } }
        static internal int AS_SAVE_INTERVAL_IN_SEC {
            get { return (int)AS_SAVE_INTERVAL_IN_MIN * 60; }
            set { AS_SAVE_INTERVAL_IN_MIN = (value / 60); ; }
        }
        static internal int AS_FILES_COUNT { get { return GET("AS_FILES_COUNT", 10); } set { var r = AS_FILES_COUNT; SET("AS_FILES_COUNT", value); } }
        static internal string AS_LOCATION {
            get {
                var res = GET("AS_LOCATION", "AutoSave");
                if (string.IsNullOrEmpty(res)) return "AutoSave";
                return res;
            }
            set { var r = AS_LOCATION; SET("AS_LOCATION", value); }
        }
        static internal int AS_FILES_STYLE { get { return GET("AS_FILES_STYLE", 1); } set { var r = AS_FILES_STYLE; SET("AS_FILES_STYLE", value); } }


        internal const string PREFS_KEY = "EM.AutoSave.";

        internal static void SET(string m_registryKey, int value)
        {
            EditorPrefs.SetInt(PREFS_KEY + m_registryKey, value);
        }
        internal static void SET(string m_registryKey, bool value)
        {
            EditorPrefs.SetBool(PREFS_KEY + m_registryKey, value);
        }
        internal static int GET(string m_registryKey, int m_boolDefaultValue)
        {
            return EditorPrefs.GetInt(PREFS_KEY + m_registryKey, m_boolDefaultValue);
        }
        internal static bool GET(string m_registryKey, bool m_boolDefaultValue)
        {
            return EditorPrefs.GetBool(PREFS_KEY + m_registryKey, m_boolDefaultValue);
        }

        internal static void SET(string m_registryKey, string m_str)
        {
            EditorPrefs.SetString(PREFS_KEY + m_registryKey, m_str);
        }
        internal static string GET(string m_registryKey, string m_str)
        {
            return EditorPrefs.GetString(PREFS_KEY + m_registryKey, m_str);
        }




        const string MENU_PATH = "Tools/AutoSave Free/";
        [MenuItem(MENU_PATH + "Enable", true)]
        public static bool EnableV() { return !USE_AUTOSAVE_MOD; }
        [MenuItem(MENU_PATH + "Enable", false)]
        public static void Enable()
        {
            USE_AUTOSAVE_MOD = (!USE_AUTOSAVE_MOD);
        }
        [MenuItem(MENU_PATH + "Disable", true)]
        public static bool DisableV() { return USE_AUTOSAVE_MOD; }
        [MenuItem(MENU_PATH + "Disable", false)]
        public static void Disable()
        {
            USE_AUTOSAVE_MOD = (!USE_AUTOSAVE_MOD);
        }
        [MenuItem(MENU_PATH + "Settings", priority = 50)]
        public static void Settings()
        {
            AutoSaveMod.SELECT();
        }

    }


    internal class AutoSaveHelper
    {




        static float AS_LAST_SAVE_TIME_IN_SEC {
            get { return SessionState.GetFloat("EMX_AS_LAST_SAVE_TIME_IN_SEC", -1); }
            set { SessionState.SetFloat("EMX_AS_LAST_SAVE_TIME_IN_SEC", value); }
        }
        static float AS_PLAY_LAUNCH_TIME {
            get { return SessionState.GetFloat("EMX_AS_PLAY_LAUNCH_TIME", -1); }
            set { SessionState.SetFloat("EMX_AS_PLAY_LAUNCH_TIME", value); }
        }

        static float lastSave {
            get { return AS_LAST_SAVE_TIME_IN_SEC; }
            set {
                if (AS_LAST_SAVE_TIME_IN_SEC != (value))
                    AS_LAST_SAVE_TIME_IN_SEC = (value);
            }
        }
        static float EDITOR_TIMER {
            get { return (float)(EditorApplication.timeSinceStartup % 1000000); }
        }




        [InitializeOnLoadMethod]
        internal static void Subscribe()
        {
            if (!Root.USE_AUTOSAVE_MOD)
            {
                EditorApplication.update -= UpdateCS;
                return;
            }
            EditorApplication.update += UpdateCS;
        }

        const string FE = ".unity";
        static string ghtrhr()
        {
            return DateTime.Now.Minute.ToString("D2");
        }

        static string _dataPath = null;
        internal static string dataPath {
            get {
                return _dataPath ?? (_dataPath = System.IO.Directory.GetCurrentDirectory().Replace('\\', '/').TrimEnd('/') + "/Assets");
            }
        }

        static string autoSaveFileName(string source)
        {
            // get {
            if (!System.IO.Directory.Exists(dataPath + "/" + Root.AS_LOCATION))
            {
                System.IO.Directory.CreateDirectory(dataPath + "/" + Root.AS_LOCATION);
                AssetDatabase.Refresh();
            }
            //if (!AssetDatabase.IsValidFolder("Assets/" + AutoSaveFolder)) AssetDatabase.CreateFolder("Assets", AutoSaveFolder);

            var files = System.IO.Directory.GetFiles(dataPath + "/" + Root.AS_LOCATION).Select(f => f.Replace('\\', '/')).Where(f =>
                f.EndsWith(".unity", StringComparison.OrdinalIgnoreCase) && f.Substring(f.LastIndexOf('/') + 1).StartsWith("AutoSave", StringComparison.OrdinalIgnoreCase)).ToArray();

            var as_loc = dataPath + "/" + Root.AS_LOCATION + "/";
            var out_loc = "Assets/" + Root.AS_LOCATION + "/";

            if (Root.AS_FILES_STYLE == 0)
            {

                //var D = Mathf.Max( 2, Root.AS_FILES_COUNT.ToString().Length);
                var D = Root.AS_FILES_COUNT.ToString().Length;
                var D_string = Enumerable.Repeat(0, D).Select(s => s.ToString()).Aggregate((a, b) => a + b);

                if (files.Length == 0) return out_loc + "AutoSave_" + D_string + ".unity";


                DateTime? ta_max = null;
                string f_max = null;
                var _times = files.Select(f => {
                    var fa = f.Remove(f.LastIndexOf('.'));
                    DateTime ta = System.IO.File.GetLastWriteTime(f);
                    if (!ta_max.HasValue || ta_max < ta)
                    {
                        ta_max = ta;
                        f_max = f;
                    }
                    return new { f = fa, t = ta };
                }
                ).ToArray();

                Func<string, string> tryGet = (file) => {
                    var _ = file.LastIndexOf('_');
                    if (_ == -1 || _ == file.Length - 1) return null;
                    int count;
                    if (int.TryParse(file.Substring(_ + 1), out count)) //.TrimStart( '0' )
                    {
                        count = (count + 1) % Root.AS_FILES_COUNT;
                        return out_loc + "AutoSave_" + count.ToString("D" + D.ToString()) + ".unity";
                    }
                    return null;
                };

                // attempt 1
                if (f_max != null)
                {
                    f_max = f_max.Remove(f_max.LastIndexOf('.'));
                    var res1 = tryGet(f_max);
                    if (res1 != null) return res1;
                }


                // attempt 2
                var times = files.Select(f => new { f = f.Remove(f.LastIndexOf('.')), t = System.IO.File.GetLastWriteTime(f) }).OrderBy(w => w.t).ToList();
                for (int ind = times.Count - 1; ind >= 0; ind--)
                {
                    var res = tryGet(times[ind].f);
                    if (res != null) return res;
                }
                // files = files.Select( n => n.Remove( n.LastIndexOf( '.' ) ) ).ToArray();

                return out_loc + "AutoSave_" + D_string + ".unity";
            }
            else
            {

                var shs = "";
                var hihi = 0;
                while (File.Exists(as_loc + gggER(source) + sdfgs().Replace("RTTR", ghtrhr()) + shs + FE))
                {
                    hihi++;
                    shs = " (" + hihi.ToString() + ")";
                }
                var outNewFileName = out_loc + gggER(source) + sdfgs().Replace("RTTR", ghtrhr()) + shs + FE;
                var asNewFileName = as_loc + gggER(source) + sdfgs().Replace("RTTR", ghtrhr()) + shs + FE;

                if (files.Length >= Root.AS_FILES_COUNT)
                {
                    //var times = files.Select(f=>new {f = f.Remove( f.LastIndexOf( '.' ) ),t = System.IO.File.GetLastWriteTime(f) } ).ToArray();
                    //.OrderBy(w=>w.t).ToList();
                    DateTime? ta_max = null;
                    string f_max = null;
                    var _times = files.Select(f => {
                        // var fa = f.Remove( f.LastIndexOf( '.' ) );
                        var fa = f;
                        DateTime ta = System.IO.File.GetLastWriteTime(f);
                        if (!ta_max.HasValue || ta_max > ta)
                        {
                            ta_max = ta;
                            f_max = f;
                        }
                        return new { f = fa, t = ta };
                    }).ToArray();
                    if (f_max == null) throw new Exception("f_max == null");

                    var f_max_local = f_max.Replace('\\', '/').TrimEnd('/');

                    if (!f_max_local.StartsWith(dataPath, StringComparison.OrdinalIgnoreCase)) throw new Exception("f_max_local | " + dataPath + " | " + f_max_local);
                    f_max_local = "Assets" + f_max_local.Substring(dataPath.Length);


                    if (!string.IsNullOrEmpty(AssetDatabase.MoveAsset(f_max_local, outNewFileName)))
                    {
                        if (!AssetDatabase.DeleteAsset(f_max_local))
                        {
                            File.Delete(f_max);
                            if (File.Exists(f_max + ".meta")) File.Delete(f_max + ".meta");
                        }
                    }
                    else
                    {
                        AssetDatabase.ImportAsset(outNewFileName, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport);
                    }
                }

                return outNewFileName;

            }

        }
        static string gggER(string source)
        {
            return "AutoSave [" + source + "] ";
        }
        static string sdfgs()
        {
            var dt = DateTime.Now;
            var tyre = dt.Hour;
            return tyre.ToString("D2") + "hRTTRm";
        }

        static float speeder = 0;

        public static void UpdateCS()
        {
            if (!Root.USE_AUTOSAVE_MOD) return;

            if (Application.isPlaying)
            {
                if (AS_PLAY_LAUNCH_TIME == -1) AS_PLAY_LAUNCH_TIME = EDITOR_TIMER;
                return;
            }

            if (AS_PLAY_LAUNCH_TIME != -1)
            {
                lastSave += (float)(EDITOR_TIMER - AS_PLAY_LAUNCH_TIME);
                AS_PLAY_LAUNCH_TIME = -1;
            }

            if (Mathf.Abs(speeder - EDITOR_TIMER) < 4) return;
            speeder = EDITOR_TIMER;

            if (Mathf.Abs(lastSave - (float)EDITOR_TIMER) >= Root.AS_SAVE_INTERVAL_IN_SEC * 2)
            {
                lastSave = (float)EDITOR_TIMER;
                // resetSet();
            }

            //Debug.Log(lastSave + " : " +  (float)EDITOR_TIMER + "  : " +  Root.AS_SAVE_INTERVAL_IN_SEC);
            if (Mathf.Abs(lastSave - (float)EDITOR_TIMER) >= Root.AS_SAVE_INTERVAL_IN_SEC)
            {
                SaveScene();
            }
            //debug();
        }

        public static string GET_SCENE_NAME()
        {
            var s = EditorSceneManager.GetActiveScene();
            if (!s.IsValid()) return "";
            var scenename = s.name;
            if (string.IsNullOrEmpty(scenename)) scenename = "untitled";
            return scenename + ".unity";
        }
        public static void SaveScene()
        {

            var s = EditorSceneManager.GetActiveScene();
            if (!s.IsValid()) return;
            var scenename = GET_SCENE_NAME();
            var fn = autoSaveFileName(scenename);

            //var relativeSavePath = "Assets/" + Root.AS_LOCATION + "/";
            var interval = Root.AS_SAVE_INTERVAL_IN_SEC;
            if (EditorSceneManager.SaveScene(s, fn, true))
            {
                //AssetDatabase.ImportAsset( fn, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport );
            }
            else
            {
                interval = 10;
            }
            var dif = (float)EDITOR_TIMER - lastSave - interval;
            if (dif > 0)
            {
                int interator = 0;
                while (dif > 0)
                {
                    lastSave = (float)EDITOR_TIMER - dif;
                    dif = (float)EDITOR_TIMER - lastSave - interval;
                    interator++;
                    if (interator > 15)
                    {
                        lastSave = (float)EDITOR_TIMER;
                        break;
                    }
                }
            }
            else lastSave = (float)EDITOR_TIMER;

            if (Root.AS_LOG)
            {
                Debug.Log("Auto-Save Current Scene: " + fn
                    + '\n' +
                    lastSave + " : " + dif + " : " + interval + " : " + EDITOR_TIMER);
                if (interval == 10) Debug.LogWarning("Error save, second attempt");
            }
        }




    }

























    class AutoSaveMod : ScriptableObject
    {

        static string __FOLDER;
        static internal string FOLDER {
            get {
                if (__FOLDER != null) return __FOLDER;
                var o = ScriptableObject.CreateInstance(typeof(AutoSaveMod));
                if (!o) return null;
                var s = MonoScript.FromScriptableObject(o);
                if (!s) return null;
                var candidate = AssetDatabase.GetAssetPath(s);
                if (string.IsNullOrEmpty(candidate)) return null;

                try
                {
                    candidate = candidate.Replace('\\', '/');
                    candidate = candidate.Remove(candidate.LastIndexOf('/'));
                    candidate = candidate.Trim(new[] { '/' }) + '/';
                }
                catch
                {
                    return null;
                }
                //PluginInternalFolder = candidate;
                //_PluginExternalFolder = UNITY_SYSTEM_PATH + _PluginInternalFolder;

                return __FOLDER = candidate;
            }
        }


        static AutoSaveMod __GetInstance = null;
        internal static AutoSaveMod GetInstance()
        {
            if (__GetInstance) return __GetInstance;
            var oldId = SessionState.GetInt(Root.PREFS_KEY + "setid", -1);
            var load = InternalEditorUtility.GetObjectFromInstanceID(oldId) as AutoSaveMod;
            if (!load)
            {
                if (!Directory.Exists(FOLDER)) Directory.CreateDirectory(FOLDER);
                var path = FOLDER + "AutoSaveSettings.asset";
                load = AssetDatabase.LoadAssetAtPath<AutoSaveMod>(path);
                if (!load)
                {
                    load = ScriptableObject.CreateInstance(typeof(AutoSaveMod)) as AutoSaveMod;
                    SessionState.SetInt(Root.PREFS_KEY + "setid", load.GetInstanceID());
                    AssetDatabase.CreateAsset(load, path);
                    AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceSynchronousImport | ImportAssetOptions.ForceUpdate);
                }

            }
            __GetInstance = load;
            return load;
        }


        internal static void SELECT()
        {
            Selection.objects = new[] { GetInstance() };
        }

    }

    [CustomEditor(typeof(AutoSaveMod))]
    class SETGUI_Autosave : UnityEditor.Editor
    {

        internal static string set_text = "AutoSave (Background)";//
        internal static string set_key = "USE_AUTOSAVE_MOD";
        public override VisualElement CreateInspectorGUI()
        {
            return base.CreateInspectorGUI();
        }
        public override void OnInspectorGUI()
        {
            _GUI();
        }



        public static void _GUI()
        {
            Draw.RESET();


            var qweqwe = Draw.R2;
            Draw.simple_wiki(ref qweqwe, Root.WIKI_2);
            var nv = EditorGUI.ToggleLeft(qweqwe, set_text, Root.USE_AUTOSAVE_MOD);
            if (nv != Root.USE_AUTOSAVE_MOD)
            {
                Root.USE_AUTOSAVE_MOD = nv;
            }
            Draw.Sp(10);




            Root.AS_SAVE_INTERVAL_IN_MIN = Draw.FIELD(Draw.R, "Save Every (Minutes)", Root.AS_SAVE_INTERVAL_IN_MIN, 1, 60);
            Root.AS_FILES_COUNT = Draw.FIELD(Draw.R, "Maximum Files Version", Root.AS_FILES_COUNT, 1, 999);

            //Draw.HELP( w, "You already have files that exceed the maximum number of files, you must manually delete the files, otherwise the asset will continue to use them." );
            //if (D)
            //var r = Draw.R;
            //r.width /= 3;
            //var r2 = r;
            //r2.width *= 2;

            EditorGUI.TextField(Draw.R, "Assets/" + Root.AS_LOCATION);
            if (GUI.Button(Draw.R2, "Change Folder"))
            {
                var res = EditorUtility.OpenFolderPanel("Change Folder", AutoSaveHelper.dataPath + "/" + Root.AS_LOCATION, "");
                if (!string.IsNullOrEmpty(res))
                {
                    res = res.Replace('\\', '/').Trim('/');
                    if (ValidateFolder(res))
                    {
                        if (!res.StartsWith(AutoSaveHelper.dataPath, StringComparison.OrdinalIgnoreCase) || res.Length <= AutoSaveHelper.dataPath.Length)
                        {
                            EditorUtility.DisplayDialog("Failed", "Path should be in the project asset folder", "Ok");
                        }
                        else
                        {
                            Root.AS_LOCATION = res.Substring(AutoSaveHelper.dataPath.Length).Replace('\\', '/').Trim('/');
                            AssetDatabase.Refresh();
                        }
                    }
                }
            }



            GUI.Label(Draw.R, "File names style:");

            Root.AS_FILES_STYLE = GUI.Toolbar(Draw.R2, Root.AS_FILES_STYLE, new[] { "Simple counter", "Special names" });
            if (Root.AS_FILES_STYLE == 0)
            {
                GUI.Label(Draw.R, "Example: 'AutoSave_" + Enumerable.Repeat(0, Root.AS_FILES_COUNT.ToString().Length).Select(a => a.ToString()).Aggregate((a, b) => a + b) + ".unity'");
            }
            else
            {
                GUI.Label(Draw.R, "Example: 'AutoSave [" + AutoSaveHelper.GET_SCENE_NAME() + "] 14h56m.unity'");
            }

            Draw.Sp(10);
            Draw.HRx1();
            //Draw.TOG( "Log", "AS_LOG" ); 
            Root.AS_LOG = EditorGUI.ToggleLeft(Draw.R, "Log", Root.AS_LOG);
            if (Root.AS_LOG)
            {
                if (GUI.Button(Draw.R, "Debug Save Scene Now"))
                {
                    AutoSaveHelper.SaveScene();
                }
            }
            Draw.HELP("Autosave timer stops during the PlayMode, for example, you set saving in every 5 minutes, and when there will 2 minutes left before saving, and you click PlayMode, and you will play 15 minutes, it will be also remained 2 minutes");
            Draw.Sp(10);


        }

        static bool ValidateFolder(string p)
        {
            if (Directory.GetFiles(p).Length != 0)
            {
                return EditorUtility.DisplayDialog(
                    "Warning!",
                    "Folder:\n...-" + p + "\nContains files, so if you will continue, some files started with 'AutoSave*' and ended with '*.unity' may be overwritten.\n\nDo you want to continue?",
                    "Yes", "Cancel");
            }
            return true;
        }
    }








    class Draw
    {


        [NonSerialized]
        internal static int groupIndex = 0;
        [NonSerialized]
        internal static float padding = 20;

        //static EventType? _lastResetEvent = null;
        internal static int CurrentId;
        internal static void RESET()
        {
            currentViewWidth = (EditorGUIUtility.currentViewWidth - 16);
            CurrentId = EditorGUIUtility.GetControlID(FocusType.Passive);
            padding = 20;
            //Debug.Log(  );
            // if ( _lastResetEvent == Event.current.type ) return;
            //_lastResetEvent = Event.current.type;
            groupIndex = 0;
        }
        static float currentViewWidth;
        static Rect _getRerct(GUILayoutOption gUILayoutOption = null)
        {
            var res = gUILayoutOption != null ? EditorGUILayout.GetControlRect(gUILayoutOption) : EditorGUILayout.GetControlRect();
            res.x = 0;
            res.width = currentViewWidth;
            res.x += padding;
            res.width -= Math.Min(20, padding) + padding;
            return last = res;
        }
        static Rect PEEK_NEW_WIDHT()
        {
            Rect res = Rect.zero;
            res.x = 0;
            res.width = currentViewWidth;
            res.x += padding;
            res.width -= Math.Min(20, padding) + padding;
            return res;
        }
        static float CALC_PADDING { get { return padding + Math.Min(20, padding); } }
        static GUIContent ec = new GUIContent();
        internal static Rect R05 { get { return _getRerct(GUILayout.Height(Mathf.RoundToInt(EditorGUIUtility.singleLineHeight * 0.2f))); } }
        internal static Rect R15 { get { return _getRerct(GUILayout.Height(Mathf.RoundToInt(EditorGUIUtility.singleLineHeight * 1.5f))); } }
        internal static Rect R { get { return _getRerct(); } }
        internal static Rect R2 { get { return _getRerct(GUILayout.Height(EditorGUIUtility.singleLineHeight * 2)); } }
        internal static Rect RH(float h) { return _getRerct(GUILayout.Height(h)); }
        internal static Rect RH(float h, int shrink, int shrink2)
        {
            var r = _getRerct(GUILayout.Height(h));
            r.x += shrink;
            r.width -= shrink2;
            return r;
        }
        internal static Rect last;
        internal static Rect lastPlus {
            get {
                var r = last;
                r.x += 16;
                return r;
            }
        }

        static bool hover { get { return last.Contains(Event.current.mousePosition); } }
        static bool press { get { return Event.current.button == 0 && Event.current.isMouse; } }
        internal static Rect Sp(float sp)
        {
            //  GUILayout.Space( sp );
            return _getRerct(GUILayout.Height(sp));
            //GUILayout.Space( sp );
        }

        internal static void HRx4RED()
        {
            Sp(4);

            Draw.HRx2();
            //EditorGUI.DrawRect(Draw.R2, Color.red);
            var c = GUI.color;
            GUI.color = Color.red;
            Draw.HRx2();
            GUI.color = c;
            /*	var r = R05;
                if (Event.current.type == EventType.Repaint)
                    s("dragHandle").Draw(r, ec, false, false, false, false);*/
            //Sp(12);
            Draw.HRx2();
            /*   r = R05;
              if ( Event.current.type == EventType.Repaint )
                  s( "dragHandle" ).Draw( r, ec, false, false, false, false );*/
            Sp(4);
        }

        internal static void HRx1(float resize = 0)
        {
            Sp(4);
            var r = R05;
            r.width -= resize;
            if (Event.current.type == EventType.Repaint)
                s("dragHandle").Draw(r, ec, false, false, false, false);
            /*   r = R05;
              if ( Event.current.type == EventType.Repaint )
                  s( "dragHandle" ).Draw( r, ec, false, false, false, false );*/
            Sp(4);
        }
        internal static void HRx1(Rect r)
        {
            // r.y += r.height / 4;
            r.height /= 2;
            if (Event.current.type == EventType.Repaint)
                s("dragHandle").Draw(r, ec, false, false, false, false);
        }
        internal static void HRx05(Rect r)
        {
            r.y += (r.height - 3) / 2;
            r.height = 2f;
            if (Event.current.type == EventType.Repaint)
                s("preToolbar").Draw(r, ec, false, false, false, false);
        }
        internal static void HRx2()
        {
            Sp(4);
            var r = R05;
            if (Event.current.type == EventType.Repaint)
                s("dragHandle").Draw(r, ec, false, false, false, false);
            Sp(12);
            /*   r = R05;
              if ( Event.current.type == EventType.Repaint )
                  s( "dragHandle" ).Draw( r, ec, false, false, false, false );*/
            Sp(4);
        }
        internal static void EXPAND(string text)
        {
            GUI.Button(R, text, s("preDropDown"));
        }

        internal static Rect Grow(Rect p, int v)
        {
            v = -v;
            p.x += v;
            p.y += v;
            p.width -= v * 2;
            p.height -= v * 2;
            return p;
        }


        static Dictionary<string, GUIStyle> _styles = new Dictionary<string, GUIStyle>();
        static Type t;
        internal static GUIStyle s(string style)
        {
            if (_styles.ContainsKey(style)) return _styles[style];
            if (t == null)
            {
                t = typeof(EditorWindow).Assembly.GetType("UnityEditor.InspectorWindow+Styles");
                if (t == null)
                {
                    t = typeof(EditorWindow).Assembly.GetType("UnityEditor.PropertyEditor+Styles");
                    //if (t == null) throw new Exception("ASD");
                }
            }
            if (t == null)
                _styles.Add(style, EditorStyles.toggle);
            else
            {
                var l = new GUIStyle(t.GetField(style, ~BindingFlags.Instance).GetValue(null) as GUIStyle);
                if (style == "addComponentArea")
                {
                    l.fixedHeight = 0;
                    l.stretchHeight = true;
                    l.padding.left = 16 + 32;
                }
                else if (style == "addComponentButtonStyle")
                {
                    l = new GUIStyle(EditorStyles.toolbarButton);
                    l.fixedWidth = 0;
                    l.stretchWidth = true;
                    l.fixedHeight = 0;
                    l.stretchHeight = true;
                }
                else if (style == "preToolbar")
                {
                    l.fixedWidth = 0;
                    l.stretchWidth = true;
                    l.fixedHeight = 0;
                    l.stretchHeight = true;
                }
                else
                {
                    l.padding.left = 16;
                }
                l.fixedWidth = 0;
                l.stretchWidth = true;
                l.fixedHeight = 0;
                l.stretchHeight = true;
                l.alignment = TextAnchor.MiddleLeft;
                _styles.Add(style, l);
            }
            return _styles[style];
        }


        static Color oldc;
        internal static void HELP(string text, Color? c = null, bool drawTog = false)
        {
            //  EditorGUI.LabelField( R, text, s( "previewMiniLabel" ) );
            var _s = s("previewMiniLabel");
            _s.wordWrap = true;

            if (c.HasValue)
            {
                oldc = GUI.color;
                GUI.color *= c.Value;
            }
            if (drawTog) text = "· " + text;
            var ca = _s.normal.textColor;
            if (!EditorGUIUtility.isProSkin) _s.normal.textColor = new Color32(20, 20, 20, 255);
            EditorGUI.TextArea(CALC_R(_s, text), text, _s);
            _s.normal.textColor = ca;
            if (c.HasValue) GUI.color = oldc;
            // GUI.Label( CALC_R( _s, text ), text, _s );

        }
        static GUIContent _calcContent = new GUIContent();
        static Rect CALC_R(GUIStyle s, string t)
        {
            _calcContent.text = t;
            var h = s.CalcHeight(_calcContent, (EditorGUIUtility.currentViewWidth - 16) - CALC_PADDING);
            var r = _getRerct(GUILayout.Height(h));
            return r;
        }

        static GUIContent __simple_wiki = new GUIContent();
        const string WIKI_LINK = "\n - Open online documentation page in browser\nLink also will be copied to text os buffer";
        internal static void simple_wiki(ref Rect r, string WIKI)
        {

            var wiki_rect = r;
            if (WIKI != null)
            {
                var S = r.height * 4;

                if (Event.current.type != EventType.Repaint) r.width -= S;
                wiki_rect.x = wiki_rect.x + wiki_rect.width;
                if (Event.current.type == EventType.Repaint) wiki_rect.x -= S;
                wiki_rect.width = S;
                //Root.SetMouseTooltip( "[ " + text + " ]" + WIKI, wiki_rect );
            }
            if (WIKI != null)
            {
                __simple_wiki.text = "-->wiki?";
                __simple_wiki.tooltip = "[ " + WIKI + " ]" + WIKI_LINK;

                //var cont = CONT( ,  );
                //Root.SetMouseTooltip( cont.tooltip, wiki_rect );
                if (GUI.Button((Rect)wiki_rect, __simple_wiki, EditorStyles.toolbarButton))
                {
                    Application.OpenURL(WIKI);
                    EditorGUIUtility.systemCopyBuffer = WIKI;
                }
            }
        }
        internal static int FIELD(Rect rect, string postFix, int value, int min, int max, float labelOffset = 0)
        {

            var _R = rect;
            var _r = _R;
            _R.width /= 1.5f;
            GUI.Label(_R, postFix + ":");
            _R.x += _R.width;
            _R.width = _r.width - _R.width;
            rect = _R;

            var crop = rect;
            if (labelOffset == 0)
            {
                if (crop.width < 160 * 1.5f)
                {
                    crop.width /= 1.5f;
                }
                else
                {
                    crop.width -= 80;
                }
            }
            else crop.width = labelOffset;
            crop.x += crop.width;
            crop.width = rect.width - crop.width;
            value = EditorGUI.IntField(crop, value);

            var ac = GUI.color;
            GUI.color *= Color.clear;
            GUI.BeginClip(rect);
            rect.y = rect.x = 0;

            rect.x += rect.width;
            float viewRect;
            viewRect = EditorGUIUtility.labelWidth;
            rect.x -= viewRect;
            value = EditorGUI.IntField(rect, "ASDaaaaaaaaaaaaaaaaaaaaa", value);
            rect.x -= viewRect;
            value = EditorGUI.IntField(rect, "ASDaaaaaaaaaaaaaaaaaaaaa", value);
            rect.x -= viewRect;
            value = EditorGUI.IntField(rect, "ASDaaaaaaaaaaaaaaaaaaaaa", value);
            GUI.EndClip();
            GUI.color = ac;

            value = Mathf.Clamp(value, min, max);

            return value;
        }

    }




}

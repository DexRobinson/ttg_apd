using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
//using System;
using System.IO;

public class FindReference : EditorWindow 
{
    private string[] allAssetPaths;                                         // holds all the project files asset paths
    private List<ProjectAsset> projectAssets = new List<ProjectAsset>();    // used to hold all the assets in the project
    private Object[] sceneAssets;                                           // used to hold all the object in the current scene
    private List<Object> dependiences = new List<Object>();                 // a list of all the dependencies in the scene(scripts, materials, textures, etc)

    private string[] allScenesNames;                                        // all the scenes in the project
    private bool[] scenesToRun;                                             // check boxs for the scenes to use when running the test


    private bool runSceneCheck;                                             // run the scene check to setup the system
    private bool runReferenceCheck;                                         // run the reference check per scene for each object
    private bool runScan;


    private Vector2 scrollView;                                             // scroll view for the assets
    private int progressValue;                                              // keep track of all the scenes loaded

    private bool hideUsedObject;                                            // used to hide/unhide assets that are being used(for a cleaner interface)

    [MenuItem("Window/Find Reference")]
    static void Init()
    {
        FindReference window = (FindReference)EditorWindow.GetWindow(typeof(FindReference));
        window.Show();
    }


    void OnInspectorUpdate()
    {
        Repaint();
    }


    void OnGUI()
    {
        hideUsedObject = GUILayout.Toggle(hideUsedObject, "Hide Used Assets");

        if (GUILayout.Button("Get All Assets"))
        {
            // reset everything so we don't carry over anything
            allAssetPaths = null;
            projectAssets.Clear();
            sceneAssets = null;
            allScenesNames = null;
            scenesToRun = null;
            dependiences.Clear();
            progressValue = 0;
            runScan = false;


            // get all the scenes in the projects
            allScenesNames = GetAllScenes();
            scenesToRun = new bool[allScenesNames.Length];
            for (int x = 0; x < scenesToRun.Length; x++)
            {
                scenesToRun[x] = true; 
            }

            // get all the assets that are in the project
            allAssetPaths = GetAllAssets();
            for (int i = 0; i < allAssetPaths.Length; i++)
            {
                if (GetObject(allAssetPaths[i]) != null)
                    projectAssets.Add(new ProjectAsset(GetObject(allAssetPaths[i])));
            }

            // flag to be able to scan the scenes once all the assets are loaded
            runSceneCheck = true;
        }

        if (runSceneCheck)
        {
            GUILayout.Label("All Scenes");
            for (int i = 0; i < allScenesNames.Length; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                scenesToRun[i] = GUILayout.Toggle(scenesToRun[i], allScenesNames[i]);
                GUILayout.EndHorizontal();
            }

            
            if (GUILayout.Button("Scan"))
            {
                runScan = true;
            }
            
            if (runScan)
            {
                // load all the dependencies in the resources folder
                //sceneAssets = Resources.LoadAll<GameObject>("GameObject");
                //Object[] k = EditorUtility.CollectDependencies(sceneAssets);
                //for (int x = 0; x < k.Length; x++)
                //{
                //    dependiences.Add(k[x]);
                //    //Debug.Log(dependiences[i].ToString());
                //}



                for (int j = 0; j < scenesToRun.Length; j++)
                {
                    if (scenesToRun[j])
                    {
                        Repaint();
                        progressValue++;
                        EditorApplication.Beep();
                        Debug.Log("Scanning: " + allScenesNames[j]);
                        EditorApplication.OpenScene(allScenesNames[j]);


                        EditorUtility.DisplayProgressBar(
                        "Scanning Assets...",
                        "Scanning Assets for scene: " + allScenesNames[j].ToString(),
                        (float)j / (float)scenesToRun.Length);



                        sceneAssets = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
                        Object[] o = EditorUtility.CollectDependencies(sceneAssets);

                        for (int i = 0; i < o.Length; i++)
                        {
                            dependiences.Add(o[i]);
                            //Debug.Log(dependiences[i].ToString());
                        }

                        for (int i = 0; i < dependiences.Count; i++)
                        {
                            for (int x = 0; x < projectAssets.Count; x++)
                            {
                                if (dependiences[i] == projectAssets[x].obj)
                                {
                                    projectAssets[x].isUsed = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                runReferenceCheck = true;
                runScan = false;
                EditorUtility.ClearProgressBar();
            }
            

            if (runReferenceCheck)
            {
                if (GUILayout.Button("Move all Trash"))
                {
                    MoveToTrash();
                }
            }

            scrollView = GUILayout.BeginScrollView(scrollView);


            if (hideUsedObject)
            {
                for (int i = 0; i < projectAssets.Count; i++)
                {
                    GUILayout.BeginHorizontal();

                    if (!projectAssets[i].isUsed)
                    {
                        projectAssets[i].isUsed = GUILayout.Toggle(projectAssets[i].isUsed, "");
                        EditorGUILayout.ObjectField(projectAssets[i].obj, typeof(Object), false);
                    }
                    GUILayout.EndHorizontal();
                }
            }
            else
            {
                for (int i = 0; i < projectAssets.Count; i++)
                {
                    GUILayout.BeginHorizontal();


                    projectAssets[i].isUsed = GUILayout.Toggle(projectAssets[i].isUsed, "");
                    EditorGUILayout.ObjectField(projectAssets[i].obj, typeof(Object), false);

                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndScrollView();
        }
    }


    private void MoveToTrash()
    {
        if (System.IO.Directory.Exists("Assets/Temp"))
        {
            Debug.Log("Temp Folder Exists");
        }
        else
        {
            Debug.Log("Creating Temp Folder...");
            AssetDatabase.CreateFolder("Assets", "Temp");
        }

        for (int i = 0; i < projectAssets.Count; i++)
        {
            if(!projectAssets[i].isUsed)
            {
                string oldPath = AssetDatabase.GetAssetPath(projectAssets[i].obj);
                Debug.Log(Path.GetExtension(projectAssets[i].obj.name + ": " + oldPath));

                AssetDatabase.MoveAsset(oldPath, "Assets/Temp/" + projectAssets[i].obj.name + Path.GetExtension(oldPath));
            }
        }
    }





    public  string[] GetAllAssets()
    {
        string[] tmpAssets1 = Directory.GetFiles(Application.dataPath, "*.*", SearchOption.AllDirectories);
        string[] tmpAssets2 = System.Array.FindAll(tmpAssets1, name => !name.EndsWith(".meta"));
        string[] allAssets;
        List<string> listAssets = new List<string>();

        allAssets = System.Array.FindAll(tmpAssets2, name => !name.EndsWith(".unity"));

        for (int i = 0; i < allAssets.Length; i++)
        {
            System.IO.DirectoryInfo p = System.IO.Directory.GetParent(allAssets[i]);

            //!p.FullName.Contains("Resources") && 
            if (p.FullName.Contains("Resources"))
            {
                Debug.Log(allAssets[i]);
                projectAssets.Add(new ProjectAsset(GetObject(allAssets[i])));
                projectAssets[projectAssets.Count - 1].isUsed = true;
            }
            else if (!p.FullName.Contains("Plugin") && !p.FullName.Contains("Plugins") && !p.FullName.Contains("Editor") && !p.FullName.Contains("Temp"))
            {
                allAssets[i] = allAssets[i].Substring(allAssets[i].IndexOf("/Assets") + 1);
                allAssets[i] = allAssets[i].Replace(@"\", "/");

                listAssets.Add(allAssets[i]);
            }
        }
        allAssets = new string[listAssets.Count];
        for (int i = 0; i < listAssets.Count; i++)
        {
            allAssets[i] = listAssets[i];
        }

        return allAssets;
    }
    public static Object GetObject(string path)
    {
        Object objToFind = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
        return objToFind;
    }
    public static string[] GetAllScenes()
    {
        string[] tmpAssets1 = Directory.GetFiles(Application.dataPath, "*.*", SearchOption.AllDirectories);
        string[] allAssets;

        allAssets = System.Array.FindAll(tmpAssets1, name => name.EndsWith(".unity"));

        for (int i = 0; i < allAssets.Length; i++)
        {
            allAssets[i] = allAssets[i].Substring(allAssets[i].IndexOf("/Assets") + 1);
            allAssets[i] = allAssets[i].Replace(@"\", "/");
        }

        return allAssets;
    }
}

public class ProjectAsset
{
    public ProjectAsset(Object obj)
    {
        this.obj = obj;
        isUsed = false;
    }
    public Object obj;
    public bool isUsed;
    public List<string> scenesUsedIn = new List<string>();
}
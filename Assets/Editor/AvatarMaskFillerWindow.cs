using UnityEditor;
using UnityEngine;

public class AvatarMaskFillerWindow : EditorWindow
{
    GameObject root;
    AvatarMask mask;

    [MenuItem("Tools/Avatar Mask Filler")]
    static void Open()
    {
        GetWindow<AvatarMaskFillerWindow>("Avatar Mask Filler");
    }

    void OnGUI()
    {
        root = (GameObject)EditorGUILayout.ObjectField("Skeleton Root", root, typeof(GameObject), true);
        mask = (AvatarMask)EditorGUILayout.ObjectField("Target Avatar Mask", mask, typeof(AvatarMask), false);

        if (GUILayout.Button("Fill Transform Paths"))
        {
            if (root == null || mask == null)
            {
                Debug.LogError("Root와 Avatar Mask를 모두 지정하세요.");
                return;
            }

            Transform[] transforms = root.GetComponentsInChildren<Transform>(true);
            mask.transformCount = transforms.Length;

            for (int i = 0; i < transforms.Length; i++)
            {
                string path = AnimationUtility.CalculateTransformPath(transforms[i], root.transform);
                mask.SetTransformPath(i, path);
                mask.SetTransformActive(i, true);
            }

            EditorUtility.SetDirty(mask);
            AssetDatabase.SaveAssets();
            Debug.Log($"{transforms.Length}개 Transform 경로를 {mask.name}에 채워넣음.");
        }
    }
}
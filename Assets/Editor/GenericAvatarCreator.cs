using UnityEditor;
using UnityEngine;

public class GenericAvatarCreator
{
    [MenuItem("Tools/Create Generic Avatar From Selection")]
    static void CreateAvatar()
    {
        GameObject go = Selection.activeGameObject;
        if (go == null)
        {
            Debug.LogError("리그 루트 GameObject를 Hierarchy에서 선택하세요.");
            return;
        }

        Avatar avatar = AvatarBuilder.BuildGenericAvatar(go, "");
        avatar.name = go.name + "_Avatar";

        if (!avatar.isValid)
        {
            Debug.LogError("Avatar 생성 실패. 계층구조를 확인하세요.");
            return;
        }

        string path = "Assets/" + avatar.name + ".asset";
        AssetDatabase.CreateAsset(avatar, path);
        AssetDatabase.SaveAssets();
        Debug.Log("Avatar 생성 완료: " + path, avatar);
    }
}
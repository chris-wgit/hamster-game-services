using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

public class GameDataEditor : OdinMenuEditorWindow
{
    [MenuItem("Tools/Game Data")]
    private static void OpenDataWindow()
    {
        GetWindow<GameDataEditor>().Show();
    }
    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();

        tree.AddAllAssetsAtPath("Character Data", "Assets/Resources/CharacterSO", typeof(CharacterDataSO));

        tree.AddAllAssetsAtPath("Weapon Data", "Assets/Resources/WeaponSO", typeof(WeaponDataSO));

        return tree;
    }
}

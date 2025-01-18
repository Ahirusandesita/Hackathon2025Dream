// --------------------------------------------------------- 
// SceneManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    private static SceneChanger _instance = default;

    // Scene
    [SerializeField,HideInInspector]
    private string _titleScene = default;
    [SerializeField,HideInInspector]
    private string _gameScene = default;

#if UNITY_EDITOR
    [SerializeField]
    private UnityEditor.SceneAsset _titleSceneAsset = default;
    [SerializeField]
    private UnityEditor.SceneAsset _gameSceneAsset = default;

    private void OnValidate()
    {
        if (_titleSceneAsset != null)
        {
            _titleScene = _titleSceneAsset.name;
        }
        if (_gameSceneAsset != null)
        {
            _gameScene = _gameSceneAsset.name;
        }
    }
#endif

    #region singleton
    public static SceneChanger Get()
    {
        _instance = _instance == default ? FindAnyObjectByType<SceneChanger>() : _instance;
        if(_instance == default)
        {
            GameObject obj = new GameObject("Scene Changer");
            _instance = obj.AddComponent<SceneChanger>();
        }
        return _instance;
    }
    #endregion

    #region Unity Method
    /// <summary>
    /// èâä˙âªèàóù
    /// </summary>
    private void Awake()
    {
        if(_instance == default)
        {
            Destroy(this.gameObject);
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    #region method
    public void GoTitleScene()
    {
        SceneManager.LoadScene(_titleScene);
    }

    public void GoGameScene()
    {
        SceneManager.LoadScene(_gameScene);
    }
    #endregion
}
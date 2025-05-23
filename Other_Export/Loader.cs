using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        TitleScene ,
        ClassroomScene ,
        LabScene , 
        LoadingScene ,
        LastScene
    }

    private static Scene targetScene;

    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallBack()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}

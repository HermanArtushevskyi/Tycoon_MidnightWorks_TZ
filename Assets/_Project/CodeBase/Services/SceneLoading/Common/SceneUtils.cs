namespace _Project.CodeBase.Services.SceneLoading.Common
{
    public static class SceneUtils
    {
        public static string ToSceneName(this SceneId sceneId)
        {
            return sceneId.ToString();
        }
    }
}
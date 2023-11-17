public class ScenePersist : Singleton<ScenePersist>
{
    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}

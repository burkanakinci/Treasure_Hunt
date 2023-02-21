
public interface IPooledObject
{
    void SetPooledTag(string _pooledTag);
    void OnObjectSpawn();
    void OnObjectDeactive();
    CustomBehaviour GetGameObject();
}
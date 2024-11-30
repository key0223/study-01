using UnityEngine;

public class ResourceManager : SingletonMonobehaviour<ResourceManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);
            
            // ������� ������Ʈ�� ������ Ǯ���� ���ܿ´�.
            GameObject go = PoolManager.Instance.GetOriginal(name);
            if (go != null)
                return go as T;
        }

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        if (original.GetComponent<Poolable>() != null)
            return PoolManager.Instance.Pop(original, parent).gameObject;

        // Ǯ�� ����� �ƴ϶��
        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        return go;


    }
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        // Ǯ���� �ʿ��ϴٸ� poolManager�� ��Ź
        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            PoolManager.Instance.Push(poolable);
            return;
        }

        Object.Destroy(go);
    }
}

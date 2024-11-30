using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    Transform _monsterParent;
    int _countToSpawn = 5;

    //Player Info
    public string PlayerName { get; set; }
    public float PlayTime { get; set; }
    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    void Init()
    {
        EventManager.Instance.SpawnMonsterEvent -= SpwanMonster;
        EventManager.Instance.SpawnMonsterEvent += SpwanMonster;
    }
    
    void SpwanMonster()
    {
        for (int i = 0; i < _countToSpawn; i++)
        {
            GameObject monster = ResourceManager.Instance.Instantiate("Monster");

            int randomX = Random.Range(-5, 3);
            int randomZ = Random.Range(-5, 3);
            monster.transform.position = new Vector3(randomX, 0.5f, randomZ);
        }
    }

    public void Clear()
    {
        SceneManagerEx.Instance.Clear();
        PoolManager.Instance.Clear();
        
    }
}

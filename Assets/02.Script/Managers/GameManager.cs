using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    Transform _monsterParent;
    int _countToSpawn = 5;
    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    void Init()
    {
        _monsterParent =GameObject.FindGameObjectWithTag(Tags.MonstersParent).transform;
        EventManager.Instance.SpawnMonsterEvent -= SpwanMonster;
        EventManager.Instance.SpawnMonsterEvent += SpwanMonster;
    }
    
    void SpwanMonster()
    {
        for (int i = 0; i < _countToSpawn; i++)
        {
            GameObject monster = ResourceManager.Instance.Instantiate("Monster");
            monster.transform.parent = _monsterParent;

            int randomX = Random.Range(-5, 3);
            monster.transform.position = new Vector3(randomX, 0.5f, 0);
        }
    }
}

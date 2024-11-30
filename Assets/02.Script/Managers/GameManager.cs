using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    Transform _monsterParent;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    void Init()
    {
        _monsterParent = GameObject.Find(Tags.MonstersParent).transform;
    }
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject monster = ResourceManager.Instance.Instantiate("Monster");
            monster.transform.parent = _monsterParent;

            int randomX = Random.Range(-3, 3);
            monster.transform.position = new Vector3(randomX, 0, 0);
        }

    }
}

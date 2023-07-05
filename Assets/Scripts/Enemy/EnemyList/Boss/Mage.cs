using System.Collections;
using UnityEngine;

/// <summary>
/// 패턴 1: 흡수
/// 패턴 2: 마법탄 발사
/// </summary>
public class Mage : Boss
{
    enum MagicMode { Bolt, Drain }
    MagicMode magicMode;

    EnemyDrain enemyDrain;
    EnemyBolt enemyBolt;

    protected override void Awake()
    {
        enemyData = GameManager.Resource.Load<EnemyData>("Boss/Mage");
        magicMode = MagicMode.Bolt;
        enemyDrain = GameManager.Resource.Load<EnemyDrain>("EnemyAttack/EnemyDrain");
        enemyBolt = GameManager.Resource.Load<EnemyBolt>("EnemyAttack/EnemyBolt");
        base.Awake();
    }

    public override void ChangeToClose()
    {
        magicMode = MagicMode.Drain;
    }

    public override void ChangeToFar()
    {
        animator.SetBool("Drain", false);
        magicMode = MagicMode.Bolt;
    }

    protected override IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (attack)
            {
                switch (magicMode)
                {
                    case MagicMode.Drain:
                        animator.SetBool("Drain", true);
                        EnemyDrain drainAttack = GameManager.Resource.Instantiate(enemyDrain, attackTransform.position, Quaternion.identity, true);
                        drainAttack.StartDrain(this);
                        yield return new WaitForSeconds(enemyData.floatdatas[0]);
                        animator.SetBool("Drain", false);
                        GameManager.Resource.Destroy(enemyDrain);
                        break;
                    case MagicMode.Bolt:
                        animator.SetTrigger("Bolt");
                        for(int i = 0; i < 5; i++)
                        {
                            EnemyBolt boltAttack = GameManager.Resource.Instantiate(enemyBolt, attackTransform.position, Quaternion.identity, true);
                            boltAttack.Shot(GameManager.Data.Player.transform.position, damage);
                            yield return new WaitForSeconds(enemyData.AttackSpeed * 0.2f);
                        }
                        break;
                }
                yield return new WaitForSeconds(enemyData.AttackSpeed);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}

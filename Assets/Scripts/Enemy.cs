
using UnityEngine;

public abstract class Enemy : MonoBehaviour, ITakeDamage
{
    protected GameObject Antagonist;
    protected int Health=5;
    public bool Alerted=false;

    private void OnEnable()
    {
        AlertArea.EnemyAlerted += GetPlayer;
    }
    
    
    private void OnDisable()
    {
        AlertArea.EnemyAlerted -= GetPlayer;
    }

    private void GetPlayer(GameObject player)
    {
        Antagonist = player;
    }

    public virtual bool IsAlerted()
    {
        return Alerted;
    }

    public virtual void TakeDamage()
    {
        Health--;
    }
}
using UnityEngine;

public class ActivateBossScript : MonoBehaviour
{
    public BossMove bossMove;
    public FinalBossMove finalBoss;
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && bossMove != null)
        {
            bossMove.enabled = true;
        }
        if (other.CompareTag("Player") &&  finalBoss != null)
        {
            finalBoss.enabled = true;
            col.enabled = false;
        }
    }
}

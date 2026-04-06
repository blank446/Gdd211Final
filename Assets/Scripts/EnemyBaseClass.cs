using UnityEngine;

public class EnemyBaseClass : MonoBehaviour
{
    //[SerializeField]private bool possessed;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            //possessed = true;
            TakeOver();
        }
    }
    public virtual void TakeOver()
    {
        Debug.Log("Put new code here");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContaminated : MonoBehaviour
{
    public Transform barril;
    public bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D hits = Physics2D.OverlapCircle(barril.transform.position, 2f,  1<<10);
        Debug.Log(hits);
        if (hits != null && !attacking  && hits.CompareTag("Player"))
        {
            hits.GetComponent<Player>().TakeDamage(1);
            StartCoroutine(attack());
        }
    }
    IEnumerator attack()
    {
        attacking = true;
        yield return new WaitForSeconds(0.5f);
        attacking = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 2);
    }
}

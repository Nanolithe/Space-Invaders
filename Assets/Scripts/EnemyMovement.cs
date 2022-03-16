using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject[] prefabs;
    public int rows = 5;
    public int columns = 11; 
    public float speed = 10.0f;
    public float enemyBullet = 1.0f;
    public Bullet Bulletprefab;
    public int killCount { get; private set; }
    public int enemyTotal => this.rows * this.columns;
    public float amountAlive => this.enemyTotal - this.killCount;
    public float percentKilled => (float) this.killCount / (float) this.enemyTotal;

    private Vector3 _direction = Vector2.right;
    private void Awake()
    {
        for( int row = 0; row < this.rows; row++)
        {
            float width = 2.0f * (this.columns - 1);
            float height = 2.0f * (this.rows - 1);
            Vector2 centering = new Vector2(-width/2, -height/2);
            Vector3 rowPosition = new Vector3(0.0f, row * 2.0f, 0.0f);
            
            for( int col = 0; col < this.columns; col++)
            {
                Enemy invader = Instantiate(this.prefabs[row], this.transform).GetComponent<Enemy>();
                Vector3 position = new Vector3(centering.x, centering.y) + rowPosition; 
                position.x += col * 2.0f; 
                invader.transform.localPosition = position;                
            }
        }
    }
    
    private void Update()
    {
        this.transform.position += _direction * this.speed * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach(Transform invader in this.transform)
        {
            if(!invader.gameObject.activeInHierarchy){
                continue;
            }

            if(_direction == Vector3.right && invader.position.x >=(rightEdge.x - 1.0f)) {
                AdvanceRow();
            } else if (_direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f)){
                AdvanceRow();
            }
        }
    }

    private void AdvanceRow()
    {
        _direction.x *= -1.0f;

        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }

    private void EnemyKill()
    {
        this.killCount++;
    }
}

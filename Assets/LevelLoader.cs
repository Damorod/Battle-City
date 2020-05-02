using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator trans;
    bool stage0;
    bool stage1;
    bool stage2;
    // Update is called once per frame
    void Update()
    {
        //if (GameObject.FindGameObjectWithTag("EnemyTriangule") == null && GameObject.FindGameObjectsWithTag("BossTriangule") == null)
        //{
        //    StartCoroutine(LoadScene(1));
        //    stage0 = true;
        //}
        //if (GameObject.FindGameObjectWithTag("EnemyCircle") == null && GameObject.FindGameObjectsWithTag("BossCube") == null && stage0)
        //{
        //    StartCoroutine(LoadScene(2));
        //    stage1 = true;
        //}

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    LoadNextScene();
        //}
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadScene(int index)
    {
        trans.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
    }
}

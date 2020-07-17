using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator trans;
    public GameObject player;
    public GameObject wonScreen;
    public GameObject deadScreen;
    public GameObject timer;
    public bool bossDead;
    public bool stage2;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            stage2 = true;
        }
        if(!stage2)
        {
            wonScreen = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!stage2 && Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        if (player.GetComponent<HealthSystem>().GetCurrentHealth() <= 0 ||
            timer.GetComponent<Timer>().getMinutes() >= 3)
        {
            Time.timeScale = 0f;
            deadScreen.SetActive(true);
        }
        else if (bossDead && !stage2)
        {
            LoadNextScene();
        }
        else if (stage2 && bossDead)
        {
            Time.timeScale = 0f;
            wonScreen.SetActive(true);
        }
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

    //public void Pause()
    //{
    // CAMBIAR A STATIC EL STAGE2 BOOLEAN!!!!!!
    //}
}

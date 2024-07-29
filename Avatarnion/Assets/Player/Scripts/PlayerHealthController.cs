using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{

    public static Action<float> OnHealthChange;



    [Header("Player health characteristics")]
    public float playerHealth;

    [Header("UI variables")]
    [SerializeField]
    private Slider playerHealthSlider;

    void OnEnable()
    {
        OnHealthChange += ChangeHealth;
    }

    void OnDisable()
    {
        OnHealthChange -= ChangeHealth;
    }

    void Start()
    {
        playerHealthSlider.value = playerHealth;
    }

    void Update()
    {
        if (playerHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ChangeHealth(float buffer)
    {
        playerHealth += buffer;
        playerHealthSlider.value = playerHealth;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : CharacterStats
{
    public override void Die()
    {
        base.Die();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


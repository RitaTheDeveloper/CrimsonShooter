using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField]
    List<Ability> abilities;

    [Header("Кол-во выбираемых абилок")]
    [SerializeField]
    int numberOfSelectableAbilities = 3;

    List<AbilityPlayer> abilityPlayers; // список абилок с переменной Bool активирована или не активирована
    List<AbilityPlayer> abilitiesForLevel;
    public List<AbilityPlayer> AbilitiesForLevel { get { return abilitiesForLevel; } }

    private void Start()
    {
        abilityPlayers = new List<AbilityPlayer>();
        CreateListOfAllAbilities();
    }

    public class AbilityPlayer
    {
        public bool Activated { get; set; }
       
        public Ability ability { get; }

        public AbilityPlayer(Ability ability, bool activated)
        {
            this.ability = ability;
            this.Activated = activated;
        }
    }

    private void CreateListOfAllAbilities()
    {
        foreach (var ability in abilities)
        {           
            AbilityPlayer abilityPlayer = new AbilityPlayer(ability, false);
            abilityPlayers.Add(abilityPlayer);
        }
        Debug.Log("создаем список всех абилок, список равен = " + abilityPlayers.Count);
    }

    // создать список абилок на выбор
    public void CreateListOfAbilitiesToChooseFrom()
    {
        // список абилок для выбора
        abilitiesForLevel = new List<AbilityPlayer>();

        // создаем список невыбранных абилок
        List<AbilityPlayer> unselectedAbilities = new List<AbilityPlayer>();

        foreach (var ability  in abilityPlayers)
        {
            if (ability.Activated == false) unselectedAbilities.Add(ability);
        }

        if (unselectedAbilities.Count >= numberOfSelectableAbilities)
        {
            Debug.Log("создаем список абилок для выбора");
            for (int i = 0; i < numberOfSelectableAbilities; i++)
            {
                // выбираем рандомную абилку из списка невыбранных
                int idAbility = Random.Range(0, unselectedAbilities.Count);

                //добавляем ее в список для выбора
                abilitiesForLevel.Add(unselectedAbilities[idAbility]);

                // удаляем ее из списка невыбранных
                unselectedAbilities.Remove(unselectedAbilities[idAbility]);
            }
        }

        //return abilitiesForLevel;
        
    }

    public void AbilitySelected(int id)
    {
        abilitiesForLevel[id].Activated = true;
        abilitiesForLevel[id].ability.ActivateAbility();       
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class GameData
{
    private string playerName;
    private string tigerName;
    private SettingsData settings;
    private List<Achievement> achievements;
    private List<ResourceData> resources;
    private List<Character> characters;

    public string PlayerName { get => playerName; set => playerName = value; }    public string TigerName { get => tigerName; set => tigerName = value; }
    public SettingsData Settings { get => settings; set => settings = value; }
    public List<Achievement> Achievements { get => achievements; set => achievements = value; }
    public List<ResourceData> Resources { get => resources; set => resources = value; }
    public List<Character> Characters { get => characters; set => characters = value; }

    public GameData()
    {
        PlayerName = "New Player";
        Settings = new SettingsData();

        //TODO Change Achievements
        Achievements = new List<Achievement>()
        {
            new Achievement("Beginner's Luck", "Complete Alex's campaign."),
            new Achievement("Woman's Charm", "Complete Sarah's campaign."),
            new Achievement("Welcome to the Jacobson's", "Complete TJ's campaign."),
            new Achievement("Lost in Time", "Complete Admiral Jacobson's Campaign."),
            new Achievement("World Savior", "Save the world."),
            new Achievement("Optional Objectives for the win", "Complete your first optional objective."),
            new Achievement("00%", "Start your journey to save the world."),
            new Achievement("25%", "Have 25% of the stars."),
            new Achievement("50%", "Have 50% of the stars."),
            new Achievement("100%", "Have 100% of the stars.")
        };

        Resources = new List<ResourceData>();
        Characters = new List<Character>()
        {
            new Character(PlayerName, 5, 5, 5, new List<BodyPart>()
            {
                new BodyPart(PartType.RightArm, WeaponType.Melee),
                new BodyPart(PartType.LeftArm, WeaponType.Melee),
                new BodyPart(PartType.RightLeg, WeaponType.Melee),
                new BodyPart(PartType.LeftLeg, WeaponType.Melee),
                new BodyPart(PartType.Torso, WeaponType.Melee),
                new BodyPart(PartType.RightEye, WeaponType.Melee),
                new BodyPart(PartType.LeftEye, WeaponType.Melee),
                new BodyPart(PartType.Heart, WeaponType.Melee),
                new BodyPart(PartType.Lungs, WeaponType.Melee),
                new BodyPart(PartType.Brain, WeaponType.Melee, false),
            }),
            new Character(TigerName, 7, 6, 5, new List<BodyPart>()
            {
                new BodyPart(PartType.RightArm, WeaponType.Melee),
                new BodyPart(PartType.LeftArm, WeaponType.Melee, false),
                new BodyPart(PartType.RightLeg, WeaponType.Melee),
                new BodyPart(PartType.LeftLeg, WeaponType.Melee, false),
                new BodyPart(PartType.Torso, WeaponType.Melee),
                new BodyPart(PartType.RightEye, WeaponType.Melee),
                new BodyPart(PartType.LeftEye, WeaponType.Melee, false),
                new BodyPart(PartType.Heart, WeaponType.Melee),
                new BodyPart(PartType.Lungs, WeaponType.Melee),
                new BodyPart(PartType.Brain, WeaponType.Melee, false),
            }),
        };
    }

    public GameData(string playerName, SettingsData settings, List<Achievement> achievements, List<ResourceData> resources)
    {
        PlayerName = playerName;
        Settings = settings;
        Achievements = achievements;
        Resources = resources;
    }

    public List<Achievement> GetAchievements(List<string> achievementNames)
    {
        List<Achievement> foundachievements = achievements.Where(achievement => achievementNames.Contains(achievement.Name)).ToList();
        return foundachievements;
    }

    public Achievement GetAchievements(string achievementName)
    {
        Achievement foundachievement = achievements.Where(achievement => achievement.Name == achievementName).FirstOrDefault();
        return foundachievement;
    }

    public void CompleteAchievement(string achievementName)
    {
        achievements.Where(achievement => achievement.Name == achievementName).FirstOrDefault().IsCompleted = true;
    }

    public int GetCompletedAchievements()
    {
        int numCompleted = 0;
        foreach (Achievement achievement in achievements)
        {
            if (achievement.IsCompleted)
            {
                numCompleted++;
            }
        }
        return numCompleted;
    }

    public void addResources(List<ResourceData> resources)
    {
        foreach (ResourceData resource in resources)
        {
            for (int i = 0; i < Resources.Count; i++)
            {
                if (Resources[i].ResourceName.Equals(resource.ResourceName))
                {
                    Resources[i].ResourceAmount += resource.ResourceAmount;
                }
                else
                {
                    Resources.Add(resource);
                }
            }
        }
    }
}

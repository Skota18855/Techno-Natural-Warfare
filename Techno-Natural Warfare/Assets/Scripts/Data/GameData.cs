using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class GameData
{
    private string playerName;
    private SettingsData settings;
    private List<Achievement> achievements;

    public string PlayerName { get => playerName; set => playerName = value; }
    public SettingsData Settings { get => settings; set => settings = value; }
    public List<Achievement> Achievements { get => achievements; set => achievements = value; }

    public GameData()
    {
        PlayerName = "New Player";
        Settings = new SettingsData();

        //TODO Change Achievements
        achievements = new List<Achievement>()
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
    }

    public GameData(string playerName, SettingsData settings, List<Achievement> achievements)
    {
        PlayerName = playerName;
        Settings = settings;
        Achievements = achievements;
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
}

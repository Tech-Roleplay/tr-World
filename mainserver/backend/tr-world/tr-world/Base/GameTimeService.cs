using System;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using AltV.Net;

namespace trWorld.Base;

public static class GameTimeService
{
    private static DateTime _realStartTime;
    
    private const int RealMsPerGameDay = 21600000; // 6 Stunden
    private const int GameMinutesPerDay = 1440;
    private const int DaysPerMonth = 30;
    private const int MonthsPerYear = 12;

    public static (int hour, int minute, int day, int month, int year) GetCurrentGameDateTime()
    {
        var elapsed = DateTime.UtcNow - _realStartTime;
        var elapsedMs = elapsed.TotalMilliseconds;

        var msToday = elapsedMs % RealMsPerGameDay;
        var gameMinute = (int)((msToday / RealMsPerGameDay) * GameMinutesPerDay);
        int hour = gameMinute / 60;
        int minute = gameMinute % 60;

        if (hour >= 24)
        {
            hour = 23;
            minute = 59;
        }

        int totalDays = (int)(elapsedMs / RealMsPerGameDay);
        int year = totalDays / (MonthsPerYear * DaysPerMonth) + 1;
        int dayOfYear = totalDays % (MonthsPerYear * DaysPerMonth);
        int month = (dayOfYear / DaysPerMonth) + 1;
        int day = (dayOfYear % DaysPerMonth) + 1;

        // Absicherung:
        if (year < 1) year = 1;
        if (month < 1) month = 1;
        else if (month > 12) month = 12;

        int maxDay = DateTime.DaysInMonth(year, month);
        if (day < 1) day = 1;
        else if (day > maxDay) day = maxDay;

        if (hour < 0) hour = 0;
        else if (hour > 23) hour = 23;

        if (minute < 0) minute = 0;
        else if (minute > 59) minute = 59;

        return (hour, minute, day, month, year);
    }

    public static void Init()
    {
        var cmd = Databank.SqlConnection.CreateCommand();
        cmd.CommandText = "SELECT start_time FROM gametime WHERE id = 0";

        using(var reader = cmd.ExecuteReader())
        {
            if(reader.Read())
            {
                _realStartTime = reader.GetDateTime("start_time"); // Spalte 0
            }
            else
            {
                _realStartTime = DateTime.UtcNow;
                var insertCmd = Databank.SqlConnection.CreateCommand();
                insertCmd.CommandText = "INSERT INTO gametime (start_time) VALUES (@start_time)";
                insertCmd.Parameters.AddWithValue("@start_time", _realStartTime);
                insertCmd.ExecuteNonQuery();
            }
        }
    }

    public static void SetStartTime(DateTime newStartTime)
    {
        _realStartTime = newStartTime;

        var updateCmd = Databank.SqlConnection.CreateCommand();
        updateCmd.CommandText = "UPDATE gametime SET start_time = @start_time";
        updateCmd.Parameters.AddWithValue("@start_time", newStartTime);
        updateCmd.ExecuteNonQuery();

        Alt.Log($"[GameTimeService] Startzeit wurde auf {newStartTime} gesetzt.");
    }
    
    private static DateTime LoadOrCreateStartTime()
    {
        string path = "gametime.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var saved = JsonSerializer.Deserialize<DateTime>(json);
            return saved;
        }
        else
        {
            var now = DateTime.UtcNow;
            File.WriteAllText(path, JsonSerializer.Serialize(now));
            return now;
        }
    }
}

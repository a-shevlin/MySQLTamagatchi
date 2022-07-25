using System;
using System.Timers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using MySql.Data.MySqlClient;

namespace Tamagatchi.Models
{
  public class BaseTamagatchi
  {
    public string Name { get; set; }
    public int Hunger { get; set; }
    public int Happy { get; set; }
    public int Training { get; set; }
    public int Discipline { get; set; }
    public int Age { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public int Id { get; set; }
    public string Status { get; set; }
    public string CheckFood { get; set; }
    public string CheckHappy { get; set; }
    public int StartTime { get; set; }
    public int CurrentTime { get; set; }

    public BaseTamagatchi(string name, int hunger, int happy, int training, int discipline, int age, int height, int weight)
    {
      Name = name;
      //random 1=10 for Hunger, Happy, Training, and Discipline
      Hunger = hunger;
      Happy = happy;
      Training = training;
      Discipline = discipline;
      Age = age; //connect to local machine time
      //random 25-40 for height
      Height = height;
      //random 20-30 for weight
      Weight = weight;

      // Status = this.CheckAlive();
      // CheckFood = this.Feed();
      // CheckHappy = this.MakeHappy();
      //Id = id;

      DateTime start = DateTime.Now;
      
      StartTime = (start.Hour * 100) + start.Minute;
    }

    // public BaseTamagatchi(string name, int id)
    // {
    //   Name = name;
    //   //random 1=10 for Hunger, Happy, Training, and Discipline
    //   Hunger = this.GetStat(1, 10);
    //   Happy = this.GetStat(1, 10);
    //   Training = this.GetStat(1, 10);
    //   Discipline = this.GetStat(1, 10);
    //   Age = 0; //connect to local machine time
    //   //random 25-40 for height
    //   Height = this.GetStat(25, 40);
    //   //random 20-30 for weight
    //   Weight = this.GetStat(20, 30);

    //   Status = this.CheckAlive();
    //   CheckFood = this.Feed();
    //   CheckHappy = this.MakeHappy();
    //   Id = id;

    //   DateTime start = DateTime.Now;
      
    //   StartTime = (start.Hour * 100) + start.Minute;
    // }

    public BaseTamagatchi(string name, int id, int hunger, int happy, int training, int discipline, int age, int height, int weight)
    {
      Name = name;
      //random 1=10 for Hunger, Happy, Training, and Discipline
      Hunger = hunger;
      Happy = happy;
      Training = training;
      Discipline = discipline;
      Age = age; //connect to local machine time
      //random 25-40 for height
      Height = height;
      //random 20-30 for weight
      Weight = weight;

      // Status = this.CheckAlive();
      // CheckFood = this.Feed();
      // CheckHappy = this.MakeHappy();
      Id = id;

      DateTime start = DateTime.Now;
      
      StartTime = (start.Hour * 100) + start.Minute;
    }

    public override bool Equals(System.Object otherTama)
      {
        if (!(otherTama is BaseTamagatchi))
        {
          return false;
        }
        else
        {
          BaseTamagatchi newTama = (BaseTamagatchi) otherTama;
          bool idEquality = (this.Id == newTama.Id);
          bool nameEquality = (this.Name == newTama.Name);
          return (nameEquality && idEquality);
        }
      }


    public static List<BaseTamagatchi> GetAll()
    {
      List<BaseTamagatchi> allMinions = new List<BaseTamagatchi> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT * FROM minionStats;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
          int minionId = rdr.GetInt32(0);
          string minionName = rdr.GetString(1);
          int hunger = rdr.GetInt32(2);
          int happy = rdr.GetInt32(2);
          int training = rdr.GetInt32(2);
          int discipline = rdr.GetInt32(2);
          int age = rdr.GetInt32(2);
          int height = rdr.GetInt32(2);
          int weight = rdr.GetInt32(2);
          
          
          BaseTamagatchi newTama = new BaseTamagatchi(minionName, minionId, hunger, happy, training, discipline, age, height, weight);
          // newTama.Happy = rdr.GetInt32(3);
          // newTama.Training = rdr.GetInt32(4);
          // newTama.Discipline = rdr.GetInt32(5);
          // newTama.Age = rdr.GetInt32(6);
          // newTama.Height = rdr.GetInt32(7);
          // newTama.Weight = rdr.GetInt32(8);
          allMinions.Add(newTama);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allMinions;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = "INSERT INTO minionStats (name, hunger, happy, training, discipline, age, height, weight) VALUES (@BaseTamagatchi.Name, @BaseTamagatchi.Hunger, @BaseTamagatchi.Happy, @BaseTamagatchi.Training, @BaseTamagatchi.Discipline, @BaseTamagatchi.Age, @BaseTamagatchi.Height,@BaseTamagatchi.Weight);";
      MySqlParameter param = new MySqlParameter();
      MySqlParameter param1 = new MySqlParameter();
      MySqlParameter param2 = new MySqlParameter();
      MySqlParameter param3 = new MySqlParameter();
      MySqlParameter param4 = new MySqlParameter();
      MySqlParameter param5 = new MySqlParameter();
      MySqlParameter param6 = new MySqlParameter();
      MySqlParameter param7 = new MySqlParameter();

      param.ParameterName = "@BaseTamagatchi.Name";
      param.Value = this.Name;
      param1.ParameterName = "@BaseTamagatchi.Hunger";
      param1.Value = this.Hunger;
      param2.ParameterName = "@BaseTamagatchi.Happy";
      param2.Value = this.Happy;
      param3.ParameterName = "@BaseTamagatchi.Training";
      param3.Value = this.Training;
      param4.ParameterName = "@BaseTamagatchi.Discipline";
      param4.Value = this.Discipline;
      param5.ParameterName = "@BaseTamagatchi.Age";
      param5.Value = this.Age;
      param6.ParameterName = "@BaseTamagatchi.Height";
      param6.Value = this.Height;
      param7.ParameterName = "@BaseTamagatchi.Weight";
      param7.Value = this.Weight;      
      cmd.Parameters.Add(param);  
      cmd.Parameters.Add(param1);
      cmd.Parameters.Add(param2);
      cmd.Parameters.Add(param3);
      cmd.Parameters.Add(param4);
      cmd.Parameters.Add(param5);
      cmd.Parameters.Add(param6);
      cmd.Parameters.Add(param7);  
      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;

      // End new code

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static int GetStat(int min, int max)
    {
      Random rnd = new Random();
      int stat = rnd.Next(min, max);
      return stat;
    }

    // public int TenStat(min, max)
    // {
    //   int random = BaseTamagatchi.GetStat(min, max);
    //   return random;
    // }

    public static BaseTamagatchi Find(int searchId)
    {

      //return _types[searchId - 1];
      // We open a connection.
      MySqlConnection conn = DB.Connection();
      conn.Open();

      // We create MySqlCommand object and add a query to its CommandText property. We always need to do this to make a SQL query.
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT * FROM minionStats WHERE id = @ThisId;";

      // We have to use parameter placeholders @ThisId and a `MySqlParameter` object to prevent SQL injection attacks. This is only necessary when we are passing parameters into a query. We also did this with our Save() method.
      MySqlParameter param = new MySqlParameter();
      param.ParameterName = "@ThisId";
      param.Value = searchId;
      cmd.Parameters.Add(param);

      // We use the ExecuteReader() method because our query will be returning results and we need this method to read these results. This is in contrast to the ExecuteNonQuery() method, which we use for SQL commands that don't return results like our Save() method.
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int statId = 0;
      string statName = "";
      int statHunger = 0;
      int statHappy = 0;
      int statTraining = 0;
      int statDiscipline = 0;
      int statAge = 0;
      int statHeight = 0;
      int statWeight = 0;
      while (rdr.Read())
      {
        statId = rdr.GetInt32(0);
        statName = rdr.GetString(1);
        statHunger= rdr.GetInt32(2);
        statHappy= rdr.GetInt32(3);
        statTraining = rdr.GetInt32(4);
        statDiscipline = rdr.GetInt32(5);
        statAge = rdr.GetInt32(6);
        statHeight = rdr.GetInt32(7);
        statWeight = rdr.GetInt32(8);
      }
      BaseTamagatchi foundMinion = new BaseTamagatchi(statName, statId, statHunger, statHappy, statTraining, statDiscipline, statAge, statHeight, statWeight);

      // We close the connection.
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundMinion;
    }

    public string CheckAlive()
    { //true == alive && false == dead;
      if (Hunger + Happy == 0)
      {
        return "Dead";
      }
      else
      {
        return "Alive";
      }
    }

    public string Feed()
    {
      Status = this.CheckAlive();
      if (Hunger < 10 && Hunger > 0)
        {
          Hunger = Hunger + 1;
          CheckFood = "You fed " + Name;
          return CheckFood;
        }
        else if (Hunger == 10)
        {
          CheckFood = Name + " is full!";
          return CheckFood;
        }
        else
        {
          CheckFood = Name + " is " + Status + " you can't feed a corpse.";
          return CheckFood;
        }
    }

    public string MakeHappy()
    {
      Status = this.CheckAlive();
      if (Happy < 10 && Happy > 0)
      {
        Happy = Happy + 1;
        CheckHappy = "You played with " + Name;
        return CheckHappy;
      }
      else if (Happy == 10)
      {
        CheckHappy = Name + " is happy!";
        return CheckHappy;
      }
      else
      {
        CheckHappy = Name + " is " + Status + "! Please don't touch a dead body.";
        return CheckHappy;
      }
    }

    public int GetTime()
    {
      DateTime start = DateTime.Now;
      int current = (start.Hour * 100) + start.Minute;
      return current;
    }

    public int GetAge()
    {
        CurrentTime = this.GetTime();

          //  57  = 1604    - 1547;
        int _age = CurrentTime - StartTime;
        Age = _age;
        return Age;
    }

    public void Timer()
    {
      System.Timers.Timer newTimer = new System.Timers.Timer();
      newTimer.Interval = 60000;
      newTimer.Elapsed += new ElapsedEventHandler(Kill);
      newTimer.AutoReset = true;
      newTimer.Enabled = true;
    }

    private void Kill(object source, ElapsedEventArgs e)
    {
      if( Hunger > 0 && Happy > 0)
      {
        Hunger = Hunger - 1;
        Happy = Happy - 1;
      }
      else
      {
        Hunger = 0;
        Happy = 0;
      }
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "DELETE FROM minionStats;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    

  }
}
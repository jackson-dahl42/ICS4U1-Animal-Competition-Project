using System;

class Program {
  static void Main () {
    Environment arena = new Environment("Arena", "Desert");
    Animal shark = new Animal("Shark", 100, 1, arena);
    Animal kangaroo = new Animal("Kangaroo", 100, 2, arena);
    while (shark.HealthPoints > 0 && kangaroo.HealthPoints > 0) {
      shark.Attack(kangaroo);
      Console.WriteLine(kangaroo.HealthPoints);
    }
  }
}

class Animal
{
  public string Name;
  public int HealthPoints;
  public int AttackPoints;
  public Environment CurrentEnvironment;

  public Animal(string name, int health, int damage, Environment environment)
  {
    Name = name;
    HealthPoints = health;
    AttackPoints = damage;
    CurrentEnvironment = environment;
  }

  // can turn into interface later
  public void TakeDamage(int damage) {
    HealthPoints = HealthPoints - damage;
    if (HealthPoints < 0) {
      HealthPoints = 0;
    }
  }
  
  // can turn into interface later
  public void Attack(Animal other) {
    other.TakeDamage(AttackPoints);
  }

  
}

class Environment 
{
  public string Name;
  public string Biome;
  public Environment(string name, string biome)
  {
    Name = name;
    Biome = biome;
  }
}




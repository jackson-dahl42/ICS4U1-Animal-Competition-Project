using System;

class Program {
  static void Main () {
    Animal shark = new Animal("Shark", 100, 1);
    Animal kangaroo = new Animal("Kangaroo", 100, 2);
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

  public Animal(string name, int health, int damage)
  {
    Name = name;
    HealthPoints = health;
    AttackPoints = damage;
    
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




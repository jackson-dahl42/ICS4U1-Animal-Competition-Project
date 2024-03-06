using System;

class Program {
  Animal shark = new Animal("Shark", 0, 1);
  public static void Main (string[] args) {
    Console.WriteLine ();
  }
}

class Animal
{
  public string Name;
  public int Health;
  public int Damage;

  public Animal(string name, int health, int damage)
  {
    Name = name;
    Health = health;
    Damage = damage;
    
  }

  public void TakeDamage(int damage) {
    //fix this so it doesnt go under 0
    Health = Health - damage;
  }

  public void Attack(Animal other) {
    other.TakeDamage(Damage);
  }
}



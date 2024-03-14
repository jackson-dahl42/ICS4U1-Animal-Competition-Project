using System;

class Program {
    static void Main() {
        ConfigureUI configUI = new ConfigureUI();
        Animal firstAnimal = configUI.ConfigureAnimal();
        Animal secondAnimal = configUI.ConfigureAnimal(firstAnimal.Name);
        Environment environment = configUI.ConfigureEnvironment();

        Console.WriteLine("Press 'a' to attack or 'c' to reconfigure");
        while (true) {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.A) {
                Console.WriteLine("Let the battle begin!");
                while (firstAnimal.HealthPoints > 0 && secondAnimal.HealthPoints > 0) {
                    firstAnimal.Attack(secondAnimal, environment);
                    if (secondAnimal.HealthPoints > 0) {
                        secondAnimal.Attack(firstAnimal, environment);
                    }
                    Console.WriteLine($"{firstAnimal.Name} HP: {firstAnimal.HealthPoints}, {secondAnimal.Name} HP: {secondAnimal.HealthPoints}");
                    Console.WriteLine("Press 'a' to attack or 'c' to reconfigure.");
                    if (Console.ReadKey(true).Key != ConsoleKey.A) {
                        break;
                    }
                }
                Console.WriteLine(firstAnimal.HealthPoints <= 0 ? $"{secondAnimal.Name} wins!" : $"{firstAnimal.Name} wins!");
            }
            else if (key == ConsoleKey.C) {
                Console.WriteLine("Reconfiguring...");
                firstAnimal = configUI.ConfigureAnimal();
                secondAnimal = configUI.ConfigureAnimal(firstAnimal.Name);
                environment = configUI.ConfigureEnvironment();
                Console.WriteLine("Press 'a' to attack or 'c' to reconfigure");
            }
            else if (key == ConsoleKey.Q) {
                break;
            }
        }
    }
}

class ConfigureUI {
    public Animal ConfigureAnimal(string otherName = null) {
        Console.WriteLine("Configure an animal:");
        string name;
        do {
            Console.Write("Enter animal name: ");
            name = Console.ReadLine();
        } while (name == otherName); 
        Console.Write("Enter animal health points: ");
        int health = int.Parse(Console.ReadLine());
        Console.Write("Enter animal attack points: ");
        int attack = int.Parse(Console.ReadLine());
        return new Animal(name, health, attack);
    }

    public Environment ConfigureEnvironment() {
        Console.WriteLine("Configure environment:");
        Console.Write("Enter environment name: ");
        string name = Console.ReadLine();
        Console.Write("Enter environment biome: ");
        string biome = Console.ReadLine();
        return new Environment(name, biome);
    }
}

class Animal {
    public string Name;
    public int HealthPoints;
    public int AttackPoints;

    public Animal(string name, int health, int attack) {
        Name = name;
        HealthPoints = health;
        AttackPoints = attack;
    }

    public void TakeDamage(int damage) {
        HealthPoints -= damage;
        if (HealthPoints < 0) {
            HealthPoints = 0;
        }
    }

    public void Attack(Animal target, Environment environment) {
        int damage = AttackPoints;
        target.TakeDamage(damage);
        Console.WriteLine($"{Name} attacks {target.Name} and deals {damage} damage!");
    }
}

class Environment {
    public string Name;
    public string Biome;

    public Environment(string name, string biome) {
        Name = name;
        Biome = biome;
    }
}



using System;

class Program {
    static void Main() {
        ConfigureUI configUI = new ConfigureUI();
        Animal firstAnimal = configUI.ConfigureAnimal();
        Animal secondAnimal = configUI.ConfigureAnimal();
        Environment environment = configUI.ConfigureEnvironment();

        Console.WriteLine("Press 'a' to attack or 'c' to reconfigure");
        while (true) {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.A) {
                while (firstAnimal.HealthPoints > 0 && secondAnimal.HealthPoints > 0) {
                    firstAnimal.Attack(secondAnimal, environment);
                    if (secondAnimal.HealthPoints > 0) {
                        secondAnimal.Attack(firstAnimal, environment);
                    }
                    Console.WriteLine($"{firstAnimal.Name} HP: {firstAnimal.HealthPoints}, {secondAnimal.Name} HP: {secondAnimal.HealthPoints}");
                    Console.WriteLine("Press 'a' to attack or 'c' to reconfigure");
                    if (Console.ReadKey(true).Key != ConsoleKey.A) {
                        break;
                    }
                }
                if (firstAnimal.HealthPoints <= 0) {
                    Console.WriteLine($"{secondAnimal.Name} wins");
                } else {
                    Console.WriteLine($"{firstAnimal.Name} wins");
                }
            }
            if (key == ConsoleKey.C) {
                Console.WriteLine("Reconfiguring");
                firstAnimal = configUI.ConfigureAnimal();
                secondAnimal = configUI.ConfigureAnimal();
                environment = configUI.ConfigureEnvironment();
                Console.WriteLine("Press 'a' to attack or 'c' to reconfigure");
            }
        }
    }
}

class ConfigureUI {
    public Animal ConfigureAnimal() {
        Console.WriteLine("Configure an animal:");
        Console.Write("Enter animal type as kangaroo, shark, or gorilla: ");
        string type = Console.ReadLine();
        Animal animal;
        if (type == "kangaroo") {
            animal = new Kangaroo();
        }
        else if (type == "shark") {
            animal = new Shark();
        }
      else if (type == "gorilla") {
            animal = new Gorilla();
        }
        else {
            Console.WriteLine("Invalid animal type, setting to kanagaroo");
            animal = new Kangaroo();
        }
        Console.Write("Enter animal name: ");
        animal.Name = Console.ReadLine();
        Console.Write("Enter animal health points: ");
        animal.HealthPoints = int.Parse(Console.ReadLine());
        Console.Write("Enter animal attack points: ");
        animal.AttackPoints = int.Parse(Console.ReadLine());
        return animal;
    }

    public Environment ConfigureEnvironment() {
        Console.WriteLine("Configure environment:");
        Console.Write("Enter environment name: ");
        string name = Console.ReadLine();
        Console.Write("Enter environment biome as ocean, desert, or jungle: ");
        string biome = Console.ReadLine();
        return new Environment(name, biome);
    }
}

class Environment {
    public string Name { get; set; }
    public string Biome { get; set; }

    public Environment(string name, string biome) {
        Name = name;
        Biome = biome;
    }
}

abstract class Animal {
    public string Name { get; set; }
    public int HealthPoints { get; set; }
    public int AttackPoints { get; set; }

    public abstract void Attack(Animal target, Environment environment);
    public abstract void TakeDamage(int damage);
}

class Kangaroo : Animal {
    public override void Attack(Animal target, Environment environment) {
        int damage = AttackPoints;
        if (environment.Biome == "desert") {
          damage = AttackPoints * 10;
        }
        target.TakeDamage(damage);
        Console.WriteLine($"{Name} the kangaroo attacks {target.Name} and deals {damage} damage");
    }

    public override void TakeDamage(int damage) {
        HealthPoints -= damage;
        if (HealthPoints < 0) {
            HealthPoints = 0;
        }
    }
}

class Shark : Animal {
    public override void Attack(Animal target, Environment environment) {
        int damage = AttackPoints;
        if (environment.Biome == "ocean") {
          damage = AttackPoints * 10;
        }
        target.TakeDamage(damage);
        Console.WriteLine($"{Name} the shark attacks {target.Name} and deals {damage} damage");
    }

    public override void TakeDamage(int damage) {
        HealthPoints -= damage;
        if (HealthPoints < 0) {
            HealthPoints = 0;
        }
    }
}

class Gorilla : Animal {
    public override void Attack(Animal target, Environment environment) {
        int damage = AttackPoints;
        if (environment.Biome == "jungle") {
          damage = AttackPoints * 10;
        }
        target.TakeDamage(damage);
        Console.WriteLine($"{Name} the gorilla attacks {target.Name} and deals {damage} damage");
    }

    public override void TakeDamage(int damage) {
        HealthPoints -= damage;
        if (HealthPoints < 0) {
            HealthPoints = 0;
        }
    }
}






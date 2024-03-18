using System;

class Program {
    static void Main() {
        ConfigureUI configUI = new ConfigureUI();
        IFightable firstFighter = configUI.ConfigureFighter();
        IFightable secondFighter = configUI.ConfigureFighter();
        Environment environment = configUI.ConfigureEnvironment();

        Console.WriteLine("Press 'a' to attack or 'c' to reconfigure");
        while (true) {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.A) {
                while (firstFighter.HealthPoints > 0 && secondFighter.HealthPoints > 0) {
                    firstFighter.Attack(secondFighter, environment);
                    if (secondFighter.HealthPoints > 0) {
                        secondFighter.Attack(firstFighter, environment);
                    }
                    Console.WriteLine($"{firstFighter.Name} HP: {firstFighter.HealthPoints}, {secondFighter.Name} HP: {secondFighter.HealthPoints}");
                    Console.WriteLine("Press 'a' to attack or 'c' to reconfigure");
                    if (Console.ReadKey(true).Key != ConsoleKey.A) {
                        break;
                    }
                }
                if (firstFighter.HealthPoints <= 0) {
                    Console.WriteLine($"{secondFighter.Name} wins");
                } else {
                    Console.WriteLine($"{firstFighter.Name} wins");
                }
            }
            if (key == ConsoleKey.C) {
                Console.WriteLine("Reconfiguring");
                firstFighter = configUI.ConfigureFighter();
                secondFighter = configUI.ConfigureFighter();
                environment = configUI.ConfigureEnvironment();
                Console.WriteLine("Press 'a' to attack or 'c' to reconfigure");
            }
        }
    }
}

class ConfigureUI {
    public IFightable ConfigureFighter() {
        Console.WriteLine("Configure a fighter:");
        Console.Write("Enter fighter type as kangaroo, shark, or gorilla: ");
        string type = Console.ReadLine();
        IFightable fighter;
        if (type == "kangaroo") {
            fighter = new Kangaroo();
        }
        else if (type == "shark") {
            fighter = new Shark();
        }
        else if (type == "gorilla") {
            fighter = new Gorilla();
        }
        else {
            Console.WriteLine("Invalid fighter type, setting to kangaroo");
            fighter = new Kangaroo();
        }
        Console.Write("Enter fighter name: ");
        fighter.Name = Console.ReadLine();
        Console.Write("Enter fighter health points: ");
        fighter.HealthPoints = int.Parse(Console.ReadLine());
        Console.Write("Enter fighter attack points: ");
        fighter.AttackPoints = int.Parse(Console.ReadLine());
        Console.Write("Enter fighter luck: ");
        fighter.Luck = int.Parse(Console.ReadLine());
        Console.Write("Enter fighter defense: ");
        fighter.Defense = int.Parse(Console.ReadLine());
        return fighter;
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

interface IFightable {
    string Name { get; set; }
    int HealthPoints { get; set; }
    int AttackPoints { get; set; }
    int Luck { get; set; }
    int Defense { get; set; }

    void Attack(IFightable target, Environment environment);
    void TakeDamage(int damage);
}

abstract class Animal : IFightable {
    public string Name { get; set; }
    public int HealthPoints { get; set; }
    public int AttackPoints { get; set; }
    public int Luck { get; set; }
    public int Defense { get; set; }

    public abstract void Attack(IFightable target, Environment environment);
    public abstract void TakeDamage(int damage);
}

class Kangaroo : Animal {
    public override void Attack(IFightable target, Environment environment) {
        Random rand = new Random();
        int luckModifier = rand.Next(0, Luck);
        int damage = (AttackPoints + luckModifier) - target.Defense;
        if (environment.Biome == "desert") {
            damage = (AttackPoints + luckModifier) * 10 - target.Defense;
        }
        if (damage < 0) damage = 0; // Ensure damage is not negative
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
    public override void Attack(IFightable target, Environment environment) {
        Random rand = new Random();
        int luckModifier = rand.Next(0, Luck);
        int damage = (AttackPoints + luckModifier) - target.Defense;
        if (environment.Biome == "ocean") {
            damage = (AttackPoints + luckModifier) * 10 - target.Defense;
        }
        if (damage < 0) damage = 0; 
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
    public override void Attack(IFightable target, Environment environment) {
        Random rand = new Random();
        int luckModifier = rand.Next(0, Luck);
        int damage = (AttackPoints + luckModifier) - target.Defense;
        if (environment.Biome == "jungle") {
            damage = (AttackPoints + luckModifier) * 10 - target.Defense;
        }
        if (damage < 0) damage = 0; 
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







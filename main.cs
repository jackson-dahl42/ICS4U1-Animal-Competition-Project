using System;

// main program class
class Program {
    static void Main() {
        // creating instance of ConfigureUI class
        ConfigureUI configUI = new ConfigureUI();
        // configuring first fighter
        IFightable firstFighter = configUI.ConfigureFighter();
        // configuring second fighter
        IFightable secondFighter = configUI.ConfigureFighter();
        // configuring environment
        Environment environment = configUI.ConfigureEnvironment();

        // asking user for action
        Console.WriteLine("Press 'a' to attack or 'c' to reconfigure");
        while (true) {
            var key = Console.ReadKey(true).Key;
            // battle loop
            if (key == ConsoleKey.A) {
                while (firstFighter.HealthPoints > 0 && secondFighter.HealthPoints > 0) {
                    // first fighter attacks second fighter
                    firstFighter.Attack(secondFighter, environment);
                    // if second fighter still has health, it attacks back
                    if (secondFighter.HealthPoints > 0) {
                        secondFighter.Attack(firstFighter, environment);
                    }
                    // displays health points of both fighters
                    Console.WriteLine($"{firstFighter.Name} HP: {firstFighter.HealthPoints}, {secondFighter.Name} HP: {secondFighter.HealthPoints}");
                    // askingfor next action
                    Console.WriteLine("Press 'a' to attack or 'c' to reconfigure");
                    // if user presses any key other than 'a', exiting attack phase
                    if (Console.ReadKey(true).Key != ConsoleKey.A) {
                        break;
                    }
                }
                // determining the winner based on health points
                if (firstFighter.HealthPoints <= 0) {
                    Console.WriteLine($"{secondFighter.Name} wins");
                } else {
                    Console.WriteLine($"{firstFighter.Name} wins");
                }
            }
            // reconfiguration
            if (key == ConsoleKey.C) {
                Console.WriteLine("Reconfiguring");
                // reconfiguring fighters and environment
                firstFighter = configUI.ConfigureFighter();
                secondFighter = configUI.ConfigureFighter();
                environment = configUI.ConfigureEnvironment();
                // asking user for next action
                Console.WriteLine("Press 'a' to attack or 'c' to reconfigure");
            }
        }
    }
}

// class responsible for configuring fighters and environment
class ConfigureUI {
    // method to configure fighter
    public IFightable ConfigureFighter() {
        Console.WriteLine("Configure a fighter:");
        // asking user for fighter type
        Console.Write("Enter fighter type as kangaroo, shark, or gorilla: ");
        string type = Console.ReadLine();
        IFightable fighter;
        // creating instance of fighter based on user input
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
        // asking user for fighter details
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

    // method to configure environment
    public Environment ConfigureEnvironment() {
        Console.WriteLine("Configure environment:");
        // Prompting user for environment details
        Console.Write("Enter environment name: ");
        string name = Console.ReadLine();
        Console.Write("Enter environment biome as ocean, desert, or jungle: ");
        string biome = Console.ReadLine();
        return new Environment(name, biome);
    }
}

// class representing environment
class Environment {
    public string Name { get; set; }
    public string Biome { get; set; }

    // constructor for environment class
    public Environment(string name, string biome) {
        Name = name;
        Biome = biome;
    }
}

// interface for fightable entities
interface IFightable {
    string Name { get; set; }
    int HealthPoints { get; set; }
    int AttackPoints { get; set; }
    int Luck { get; set; }
    int Defense { get; set; }

    void Attack(IFightable target, Environment environment);
    void TakeDamage(int damage);
}

// abstract class representing an animal
abstract class Animal : IFightable {
    // properties for animal
    public string Name { get; set; }
    public int HealthPoints { get; set; }
    public int AttackPoints { get; set; }
    public int Luck { get; set; }
    public int Defense { get; set; }

    // abstract methods for animal
    public abstract void Attack(IFightable target, Environment environment);
    public abstract void TakeDamage(int damage);
}

// Class representing a Kangaroo
class Kangaroo : Animal {
    // method for kangaroo attacking
    public override void Attack(IFightable target, Environment environment) {
        Random rand = new Random();
        int luckModifier = rand.Next(0, Luck);
        int damage = (AttackPoints + luckModifier) - target.Defense;
        // modifying damage based on environment
        if (environment.Biome == "desert") {
            damage = (AttackPoints + luckModifier) * 10 - target.Defense;
        }
        if (damage < 0) damage = 0; // Ensure damage is not negative
        // inflicting damage on the target
        target.TakeDamage(damage);
        Console.WriteLine($"{Name} the kangaroo attacks {target.Name} and deals {damage} damage");
    }

    // method for kangaroo taking damage
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




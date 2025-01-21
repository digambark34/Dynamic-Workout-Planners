using System;
using System.Collections.Generic;
using System.Linq;

public enum FitnessGoal
{ WeightLoss, MuscleGain, GeneralFitness }

public abstract class Workout
{
    public string Name { get; set; }
    public int DurationMinutes { get; set; }

    public abstract void Display();
}

public class CardioWorkout : Workout
{
    public int CaloriesBurned { get; set; }

    public override void Display()
    {
        Console.WriteLine($"Cardio: {Name}, Duration: {DurationMinutes} mins, Calories Burned: {CaloriesBurned}");
    }
}

public class StrengthWorkout : Workout
{
    public string Equipment { get; set; }

    public override void Display()
    {
        Console.WriteLine($"Strength: {Name}, Duration: {DurationMinutes} mins, Equipment: {Equipment}");
    }
}

public class UserProfile
{
    public string Name { get; set; }
    public int Age { get; set; }
    public double Weight { get; set; }
    public FitnessGoal Goal { get; set; }
    public List<Workout> WorkoutPlan { get; set; } = new List<Workout>();
}

public class WorkoutPlanner
{
    private List<UserProfile> users = new List<UserProfile>();

    public void AddUser()
    {
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();
        Console.Write("Enter your age: ");
        int age = int.Parse(Console.ReadLine());
        Console.Write("Enter your weight (kg): ");
        double weight = double.Parse(Console.ReadLine());

        Console.WriteLine("Select your fitness goal: 1. Weight Loss, 2. Muscle Gain, 3. General Fitness");
        FitnessGoal goal = (FitnessGoal)(int.Parse(Console.ReadLine()) - 1);

        UserProfile user = new UserProfile
        {
            Name = name,
            Age = age,
            Weight = weight,
            Goal = goal
        };

        users.Add(user);
        Console.WriteLine("Profile created successfully!");
    }

    public void GenerateWorkoutPlan()
    {
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();
        var user = users.FirstOrDefault(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (user == null)
        {
            Console.WriteLine("User not found.");
            return;
        }

        Console.WriteLine("Generating workout plan...");
        if (user.Goal == FitnessGoal.WeightLoss)
        {
            user.WorkoutPlan.Add(new CardioWorkout { Name = "Running", DurationMinutes = 30, CaloriesBurned = 300 });
            user.WorkoutPlan.Add(new CardioWorkout { Name = "Cycling", DurationMinutes = 20, CaloriesBurned = 200 });
        }
        else if (user.Goal == FitnessGoal.MuscleGain)
        {
            user.WorkoutPlan.Add(new StrengthWorkout { Name = "Bench Press", DurationMinutes = 45, Equipment = "Barbell" });
            user.WorkoutPlan.Add(new StrengthWorkout { Name = "Squats", DurationMinutes = 40, Equipment = "Dumbbells" });
        }
        else
        {
            user.WorkoutPlan.Add(new CardioWorkout { Name = "Yoga", DurationMinutes = 30, CaloriesBurned = 100 });
        }

        Console.WriteLine("Workout plan generated!");
    }

    public void ViewWorkoutPlan()
    {
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();
        var user = users.FirstOrDefault(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (user == null || !user.WorkoutPlan.Any())
        {
            Console.WriteLine("No workout plan found.");
            return;
        }

        Console.WriteLine($"Workout Plan for {user.Name}:");
        foreach (var workout in user.WorkoutPlan)
        {
            workout.Display();
        }
    }
}

public class Program
{
    public static void Main()
    {
        WorkoutPlanner planner = new WorkoutPlanner();
        while (true)
        {
            Console.WriteLine("\n1. Add User");
            Console.WriteLine("2. Generate Workout Plan");
            Console.WriteLine("3. View Workout Plan");
            Console.WriteLine("4. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    planner.AddUser();
                    break;

                case "2":
                    planner.GenerateWorkoutPlan();
                    break;

                case "3":
                    planner.ViewWorkoutPlan();
                    break;

                case "4":
                    Console.WriteLine("Thank you for using the Workout Planner!");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}
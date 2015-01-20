using System;
using NRules.Fluent;

namespace NRules.Samples.SimpleRules
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var dwelling = new Dwelling { Address = "1 Main Street, New York, NY", Type = DwellingTypes.SingleHouse };
            var dwelling2 = new Dwelling { Address = "2 Main Street, New York, NY", Type = DwellingTypes.SingleHouse };
            var policy1 = new Policy { Name = "Silver", PolicyType = PolicyTypes.Home, Price = 1200, Dwelling = dwelling };
            var policy2 = new Policy { Name = "Gold", PolicyType = PolicyTypes.Home, Price = 2300, Dwelling = dwelling2 };
            var johnDo = new Customer { Name = "John Do", Age = 21, Sex = SexTypes.Male, Policy = policy1 };
            var emilyBrown = new Customer { Name = "Emily Brown", Age = 32, Sex = SexTypes.Female, Policy = policy2 };

            // create a rules evaluation context
            var repository = new RuleRepository();
            repository.Load(x => x.From(typeof(Program).Assembly));
            var ruleSets = repository.GetRuleSets();
            var compiler = new RuleCompiler();
            var factory = compiler.Compile(ruleSets);
            var session = factory.CreateSession();

            // insert some known facts into the session
            session.Insert(policy1);
            session.Insert(policy2);
            session.Insert(johnDo);
            session.Insert(emilyBrown);
            session.Insert(dwelling);
            session.Insert(dwelling2);

            Console.WriteLine("Press enter to evaluate the rules based on what we have currently in session. ");
            Console.ReadLine();
            Console.WriteLine("Program :evaluating rules....");
            session.Fire();

            Console.WriteLine("Press enter to change John Doe's age to 10 and re-evaluate the rules.");
            Console.ReadLine();
            johnDo.Age = 10;
            session.Update(johnDo);
            Console.WriteLine("Program :evaluating rules....");
            session.Fire();
            Console.WriteLine("Program :rules evaluated.");
         

            Console.WriteLine("Press enter to restore John Doe's age to 21 and re-evaluate the rules.");
            Console.ReadLine();
            // restore John's age and re-evaluate the rules
            johnDo.Age = 21;
            session.Update(johnDo);
            Console.WriteLine("Program:evaluating rules....");
            session.Fire();
            Console.WriteLine("Program:rules evaluated.");

            Console.WriteLine("Press enter to remove Emily Brown from the session and re-evaluate the rules.");
            Console.ReadLine();
            // now remove emily from the session and also re-evaluate the rules
            session.Retract(emilyBrown);
            Console.WriteLine("Program:evaluating rules....");
            session.Fire();
            Console.WriteLine("Program:rules evaluated.");

            Console.WriteLine("Press enter to restore Emily Brown to the session and re-evaluate the rules.");
            Console.ReadLine();

            // insert emily back into the session and re-evaluate the rules
            session.Insert(emilyBrown);
            Console.WriteLine("Program:evaluating rules....");
            session.Fire();
            Console.WriteLine("Program:rules evaluated.");

            Console.ReadLine();
        }
    }
}
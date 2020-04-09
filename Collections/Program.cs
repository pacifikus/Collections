using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
	class Program
	{
		class Man
		{
			public string Name { get; set; }
			public int Age { get; set; }

			public override string ToString()
			{
				return $"{Name}: {Age}";
			}

			public override bool Equals(object obj)
			{
				if (obj == null) return false;
				var man = (Man)obj;
				return Name == man.Name && Age == man.Age;
			}

			public override int GetHashCode()
			{
				int hashcode = Age.GetHashCode();
				hashcode = 24 * hashcode + Name.GetHashCode();
				return hashcode;
			}
		}

		static void Main(string[] args)
		{
			IntSample();
			Console.WriteLine();
			ClassSample();
			Console.ReadLine();
		}

		private static void ClassSample()
		{
			var men = new HashSet<Man>();
			men.Add(new Man() { Age = 22, Name = "Alice" });
			men.Add(new Man() { Age = 11, Name = "Bob" });
			DisplayInfo(men);

			men.Add(new Man() { Age = 22, Name = "Alice" });
			DisplayInfo(men);

			men.Add(new Man() { Age = 33, Name = "Carl" });
			DisplayInfo(men);

			men.Remove(new Man() { Age = 22, Name = "Alice" });
			DisplayInfo(men);
		}

		private static void IntSample()
		{
			var nums = new HashSet<int>();

			for (int i = 0; i < 5; i++)
			{
				nums.Add(i * 2);
			}
			DisplayInfo(nums);

			nums.Add(0);
			DisplayInfo(nums);

			nums.Add(10);
			DisplayInfo(nums);

			nums.Remove(0);
			DisplayInfo(nums);
		}

		private static void DisplayInfo(HashSet<int> nums)
		{
			Console.WriteLine($"nums count = {nums.Count}");
			Console.WriteLine($"nums items = {String.Join(" ", nums.ToArray())}");
		}

		private static void DisplayInfo(HashSet<Man> men)
		{
			Console.WriteLine($"men count = {men.Count}");
			Console.WriteLine($"men items = {String.Join(", ", men.ToArray().Select(x => x.ToString()))}");
		}
	}
}

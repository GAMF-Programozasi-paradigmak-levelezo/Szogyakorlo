using System.Text;
namespace Szogyakorlo
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Szógyakorló");
			Dictionary<string, string> Szótár;
			// Megnézem, hogy létezik-e a csv file. Ha igen, beolvasom a szótárt onnan. Ha nem, akkor feltöltöm a szótárt, és lementem csv-be.
			if (!File.Exists("szoparok.csv"))
			{
				Szótár = Feltölt();
				Lement("szoparok.csv", Szótár);
			}
			else
			{
				Szótár = new Dictionary<string, string>();
				using (var sr = new StreamReader("szoparok.csv", Encoding.UTF8))
				{
					while (!sr.EndOfStream)
					{
						var sor = sr.ReadLine();
						if (sor == null || sor == "")
							continue;
						var részek = sor.Split(',');
						if (részek.Length != 2)
							continue;
						Szótár[részek[0]] = részek[1];
					}
				}
			}

			do
			{
				Console.Write("Tanító (T) vagy kikérdező (K) üzemmódban akarod-e használni? ");
				var üzemmód = Console.ReadLine();
				if (üzemmód == "T" || üzemmód == "t")
					Tanít(Szótár);
				else if (üzemmód == "K" || üzemmód == "k")
					Kikérdez(Szótár);
				else
					break;
			} while (true);

		}

		private static void Lement(string fájlnév, Dictionary<string, string> szótár)
		{
			using (var sw = new StreamWriter(fájlnév,false,Encoding.UTF8))
			{
				foreach (var item in szótár)
					sw.WriteLine($"{item.Key},{item.Value}");
				//sw.Close();
			}
		}

		private static void Kikérdez(Dictionary<string, string> Szótár)
		{
			// Kikérdező rész
			Random rnd = new Random();
			bool újszó;
			do
			{
				var index = rnd.Next(0, Szótár.Count);
				var kérdés = Szótár.ElementAt(index);
				Console.Write($"Mi a(z) {kérdés.Key} angolul? ");
				var válasz = Console.ReadLine();
				if (válasz == kérdés.Value)
					Console.WriteLine("Helyes válasz!");
				else
					Console.WriteLine($"Helytelen válasz! A helyes válasz: {kérdés.Value}");
				Console.Write("Szeretnél még egy szót gyakorolni? (i/n) ");
				újszó = Console.ReadLine() == "i";
			} while (újszó);
		}

		private static void Tanít(Dictionary<string, string> Szótár)
		{
			// Tanulási rész
			bool kilépés = false;
			do
			{
				Console.Write("Melyik szóra vagy kíváncsi? ");
				var szó = Console.ReadLine();
				if (szó == null || szó == "")
					continue;
				if (Szótár.ContainsKey(szó))
					Console.WriteLine($"{szó} angolul: {Szótár[szó]}");
				else
					Console.WriteLine("Nincs ilyen szó a szótárban.");
				Console.Write("Szeretnél még kilépni a tanulási részből? (i/n) ");
				kilépés = Console.ReadLine() == "i";
			} while (!kilépés);
		}

		private static Dictionary<string, string> Feltölt()
		{
			Dictionary<string, string> Szótár = new Dictionary<string, string>
			{
				{ "alma", "apple" },
				{ "körte", "pear" },
				{ "szilva", "plum" },
				{ "barack", "peach" },
				{ "szőlő", "grape" },
				//{ "barack", "apricot" }
			};
			Szótár["barack"] = "apricot";
			//foreach (var item in Szótár)
			//{
			//	Console.WriteLine($"{item.Key} - {item.Value}");
			//}
			return Szótár;
		}
	}
}

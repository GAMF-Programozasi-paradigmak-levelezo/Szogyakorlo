/*
 Készítsen egy C# konzol alkalmazást, amely magyar–angol szópárokat tartalmazó szótár segítségével segíti a felhasználót a szavak gyakorlásában. A programnak két üzemmódot kell biztosítania:

- **Tanító üzemmód (T)**: A felhasználó megad egy magyar szót, és a program kiírja annak angol megfelelőjét, ha az szerepel a szótárban.
- **Kikérdező üzemmód (K)**: A program véletlenszerűen kiválaszt egy magyar szót, és a felhasználónak meg kell adnia az angol fordítást. A program visszajelzést ad a válasz helyességéről.

A szótár adatait a program CSV fájlban tárolja (szoparok.csv). Ha a fájl létezik, a program beolvassa induláskor a szópárokat. Ha nem, létrehoz egy alapértelmezett szótárat, majd elmenti azt a fájlba.
 */
using System.Text;
namespace Szogyakorlo
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Szógyakorló");
			// Szótár létrehozása (magyar -> angol)
			Dictionary<string, string> Szótár;
			// Megnézzük, hogy létezik-e a csv file. Ha igen, beolvassuk a szótárt onnan. Ha nem, akkor feltöltjük a szótárt, és lementjük csv-be.
			if (File.Exists("szoparok.csv"))
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
			else
			{
				Szótár = Feltölt();
				Lement("szoparok.csv", Szótár);
			}

			// Üzemmód választása: Tanító(T) vagy Kikérdező(K)
			do
			{
					Console.Write("Tanító (T) vagy kikérdező (K) üzemmódban akarod-e használni? ");
					var üzemmód = Console.ReadLine()?.ToLower();
					if (üzemmód == "t")
						Tanít(Szótár);
					else if (üzemmód == "k")
						Kikérdez(Szótár);
					else
						break;
			} while (true);

		}

		/// <summary>
		/// Szótár mentése CSV fájlba.
		/// </summary>
		private static void Lement(string fájlnév, Dictionary<string, string> szótár)
		{
			using (var sw = new StreamWriter(fájlnév,false,Encoding.UTF8))
			{
				foreach (var item in szótár)
					sw.WriteLine($"{item.Key},{item.Value}");
			}
		}


		/// <summary>
		/// Kikérdező üzemmód: véletlenszerű magyar szót kérdez, felhasználó megadja az angol megfelelőjét.
		/// </summary>
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

		/// <summary>
		/// Tanító üzemmód: felhasználó megad egy magyar szót, program kiírja az angol megfelelőjét.
		/// </summary>
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
				Console.Write("Szeretnél kilépni a tanulási részből? (i/n) ");
				kilépés = Console.ReadLine() == "i";
			} while (!kilépés);
		}

		/// <summary>
		/// Alapértelmezett szótár feltöltése.
		/// </summary>
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
			// Utólag módosítjuk a "barack" fordítását
			Szótár["barack"] = "apricot";
			// Kiírjuk a szótár tartalmát
			foreach (var item in Szótár)
			{
				Console.WriteLine($"{item.Key} - {item.Value}");
			}
			return Szótár;
		}
	}
}

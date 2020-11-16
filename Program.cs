using System;
using System.Collections.Generic;
using System.Linq;

using OOP_Laba3;

namespace OOP_Laba11 {
	class Program {
		static readonly List<Action> Tasks = new List<Action> {
			new Action(Task1),
			new Action(Task2),
			new Action(Task3)
		};

		static void Main() {
			while (true) {
				Console.Write(
					"1 - месяцы" +
					"\n2 - запросы к коллекции абитуриентов" +
					"\n3 - оператор Join" +
					"\n0 - выход" +
					"\nВыберите действие: "
					);
				if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > Tasks.Count) {
					Console.WriteLine("Нет такого действия");
					Console.ReadKey();
					Console.Clear();
					continue;
				}
				if (choice == 0) {
					Console.WriteLine("Выход...");
					return;
				}

				Tasks[choice - 1]();

				Console.ReadKey();
				Console.Clear();
			}
		}

		static void Task1() {
			string[] months = { "Декабрь", "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь" };

			// Фильтр по длине
			int n;
			do {
				Console.Write("\nВведите длину строки: ");
			} while (!int.TryParse(Console.ReadLine(), out n));

			var monthsN = months.Where(item => item.Length == n);
			if (monthsN.Count() != 0) {
				foreach (var item in monthsN)
					Console.Write(item + " ");
				Console.WriteLine();
			}
			else
				Console.WriteLine($"Месяцев с длинной строки {n} нет");
			Console.ReadKey();

			// Только зимние и летние
			var winterAndSummer = months.Where(item =>
				months.ToList().IndexOf(item) < 3 ||
				months.ToList().IndexOf(item) > 5 && months.ToList().IndexOf(item) < 9);

			Console.WriteLine("\nЗимние и летние месяцы:");
			foreach (var item in winterAndSummer)
				Console.Write(item + " ");
			Console.WriteLine();
			Console.ReadKey();

			// В алфавитом порядке
			Console.WriteLine("\nМесяцы в алфавитном порядке:");
			foreach (var item in months.OrderBy(item => item))
				Console.Write(item + " ");
			Console.WriteLine();
			Console.ReadKey();

			// Кол-во содержащих 'и' и длинной > 3
			var amt1 = months.Count(item =>
				item.Any(letter => letter == 'и' || letter == 'И') &&
				item.Length > 3);
			Console.WriteLine("\nКол-во месяцев содержащих \"и\" и длинной не менее 4: " + amt1);
		}

		static void Task2() {
			var list = new List<Abiturient>(Generator.Generate(50));

			while (true) {
				Console.Clear();
				Console.Write(
					"1 - вывод всех абитуриентов" +
					"\n2 - неудовлетворительные оценки" +
					"\n3 - сумма баллов выше заданной" +
					"\n4 - с 10 по определённому предмету" +
					"\n5 - по алфавиту" +
					"\n6 - 4 с самой низкой успеваемостью" +
					"\n7 - абитуриенты с положительными оценками по группам" +
					"\n0 - назад" +
					"\nВыберите действие: "
					);
				if (!int.TryParse(Console.ReadLine(), out int choice)) {
					Console.WriteLine("Нет такого действия");
					Console.ReadKey();
					continue;
				}

				switch (choice) {
					case 1:
						Console.WriteLine("Список всех абитуриентов");
						Abiturient.PrintList(list);
						break;
					case 2: {
						var res = Abiturient.GetUnderperforming(list);
						Console.WriteLine("Список абитуриентов с неудовлетворительными оценками");
						Abiturient.PrintList(res);
					}
					break;
					case 3: {
						int sum;
						do {
							Console.Write("Введите сумму баллов: ");
						} while (!int.TryParse(Console.ReadLine(), out sum));
						var res = Abiturient.GetMoreThan(list, sum);
						Console.WriteLine("Список абитуриентов с суммой оценок выше " + sum);
						Abiturient.PrintList(res);
					}
					break;
					case 4: {
						Console.Write("Введите название предмета: ");
						string subject = Console.ReadLine();
						try {
							var res = Abiturient.GetAmtOfSmartInSubject(list, subject);
							Console.WriteLine($"Кол-во абитуриентов с 10 по {subject}: {res}");
						}
						catch {
							Console.WriteLine("Название предмета введено неправильно");
						}
					}
					break;
					case 5: {
						var res = Abiturient.GetOrdered(list);
						Console.WriteLine("Список абитуриентов по алфавиту");
						Abiturient.PrintList(res);
					}
					break;
					case 6: {
						var res = Abiturient.GetWorst(list);
						Console.WriteLine("Список абитуриентов с худшей успеваемостью");
						Abiturient.PrintList(res);
					}
					break;
					case 7: {
						var res = Abiturient.GetGoodByGroup(list);
						Console.WriteLine("Список абитуриентов с положительными оценками по группам");
						foreach (var group in res) {
							Console.WriteLine("\nГруппа " + group.Key);
							foreach (var abit in group.Value)
								Console.WriteLine($"{string.Concat(abit.Surname, abit.Name),-25} {abit.Marks.Values.Average(),4:F2}");
						}
					}
					break;
					case -1:
						try {
							Console.Write("Новое число абитуриентов: ");
							list = Generator.Generate(Convert.ToInt32(Console.ReadLine()));
						}
						catch { };
						break;
					case 0:
						return;
					default:
						Console.WriteLine("Действие не распознанно");
						continue;
				}

				Console.ReadKey();
			}
		}

		private class Item1 {
			public string Prop1 { get; private set; }
			public string Prop2 { get; private set; }
			public Item1(string prop1, string prop2) {
				Prop1 = prop1;
				Prop2 = prop2;
			}
		}

		private class Item2 {
			public string Prop1 { get; private set; }
			public string Prop2 { get; private set; }
			public Item2(string prop1, string prop2) {
				Prop1 = prop1;
				Prop2 = prop2;
			}
		}
		static void Task3() {
			var items1 = new List<Item1> {
				new Item1("123", "qwe"),
				new Item1("345", "asd"),
				new Item1("789", "zxc")
			};
			var items2 = new List<Item2> {
				new Item2("rty", "345"),
				new Item2("fgh", "123"),
				new Item2("vbn", "789")
			};
			var res = items1.Join(items2, k1 => k1.Prop1, k2 => k2.Prop2,
				(k1, k2) => new { k1.Prop1, Prop2 = k2.Prop1});
			foreach (var item in res)
				Console.WriteLine($"{item.Prop1} {item.Prop2}");
		}
	}
}

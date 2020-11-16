using System;
using System.Collections.Generic;
using System.IO;

namespace OOP_Laba3 {
	class Generator {
		private static readonly Random rndm = new Random();

		private static readonly List<string> surnames = new List<string>();
		private static readonly List<string> names = new List<string>();
		private static readonly List<string> patronymics = new List<string>();

		static Generator() {
			LoadSurnames();
			LoadNames();
			LoadPatronymics();
		}

		private static void LoadSurnames() {
			using var sr = new StreamReader("surnames.txt");
			string surname;
			while ((surname = sr.ReadLine()) != null)
				surnames.Add(surname);
		}

		private static void LoadNames() {
			using var sr = new StreamReader("names.txt");
			string name;
			while ((name = sr.ReadLine()) != null)
				names.Add(name);
		}

		private static void LoadPatronymics() {
			using var sr = new StreamReader("patronymics.txt");
			string patronymic;
			while ((patronymic = sr.ReadLine()) != null)
				patronymics.Add(patronymic);
		}

		// Генерация абитуриентов
		public static List<Abiturient> Generate(int amt) {
			var abiturients = new List<Abiturient>();

			string id, surname, name, patronymic;
			int group;

			for (int i = 0; i < amt; i++) {
				group = rndm.Next(1, 7);
				bool pass;
				// Генерация id
				do {
					pass = true;
					id = rndm.Next(100000, 1000000).ToString();
					// Проверка на повторяемость
					foreach (var item in abiturients) {
						if (item.Id == id) {
							pass = false;
							break;
						}
					}
				} while (!pass);

				// Генерация ФИО
				surname = surnames[rndm.Next(surnames.Count)];
				name = names[rndm.Next(names.Count)];
				patronymic = patronymics[rndm.Next(patronymics.Count)];

				// Генерация оценок
				int[] minVals = { 1, 4, 7 }, maxVals = { 6, 8, 10 };
				// Определение уровня интеллекта
				int iqBorder = rndm.Next(1, 6),
					iqLvl = iqBorder == 1 ? 0 : iqBorder == 5 ? 2 : 1;
				// Создание словаря
				var marks = new Dictionary<string, int?> {
					{"ДиЮПИ", rndm.Next(minVals[iqLvl], maxVals[iqLvl])},
					{"Печатные процессы", rndm.Next(minVals[iqLvl], maxVals[iqLvl])},
					{"Математика", rndm.Next(minVals[iqLvl], maxVals[iqLvl])},
					{"КС", rndm.Next(minVals[iqLvl], maxVals[iqLvl])},
					{"ООП", rndm.Next(minVals[iqLvl], maxVals[iqLvl])},
					{"Дискретная математика", rndm.Next(minVals[iqLvl], maxVals[iqLvl])},
					{"ИГиГ", rndm.Next(minVals[iqLvl], maxVals[iqLvl])}
				};

				var abiturient = new Abiturient(id, surname, name, patronymic, group, marks);
				abiturients.Add(abiturient);
			}
			return abiturients;
		}
	}
}
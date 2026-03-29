using System.Xml.Linq;

namespace Lab8.Green
{
    public class Task1
    {
        public abstract class Participant // поменяли структуру на класс
        {
            // поля
            private string _surname;
            private string _group;
            private string _trainer;
            private double _result;
            protected double Normat;
            private static int _passed;

            // свойства
            public string Surname => _surname;
            public string Group => _group;
            public string Trainer => _trainer;
            public double Result => _result;
            public static int PassedTheStandard => _passed;

            public bool HasPassed // Публичное логическое свойство только для чтения,
                                  // которое возвращает прошла участница норматив или нет.

            {
                get
                {
                    if (_result == 0)
                        return false;

                    return _result <= Normat; // сравниваем результат участницы с нормативом 

                }
            }
            // конструктор
            static Participant()
            {

                _passed = 0; // Счётчик участниц, прошедших норматив
            }
            public Participant(string surname, string group, string trainer)
            {
                _surname = surname;
                _group = group;
                _trainer = trainer;

            }
            public void Run(double result)
            {
                if (_result == 0)
                {
                    _result = result; // проверяем, первый ли это результат участницы.
                                      // Если да , то мы записываем его в поле _result.

                    if (result <= Normat) // кол-во прошедших норматив увеличивается
                        _passed++;
                }
            }
            public static Participant[] GetTrainerParticipants(Participant[] participants, Type participantType, string trainer) 
                       // возвращает всех участников выбранного тренера в выбранном забеге.
            {
                Participant[] newArray = [];

                foreach (var participant in participants)
                {
                    if (participant.Trainer == trainer && participant.GetType() == participantType)
                    {
                        Array.Resize(ref newArray, newArray.Length + 1);
                        newArray[newArray.Length - 1] = participant;
                    }
                }

                return newArray;
            }

            public void Print()
            {
                Console.WriteLine($"Фамилия: {Surname}");
                Console.WriteLine($"Группа: {Group}");
                Console.WriteLine($"Тренер: {Trainer}");
                Console.WriteLine($"Результат: {Result}");
                Console.WriteLine($"Прошла  ли норматив? : {(HasPassed ? "Да" : "Нет")}");
            }
            
        }
        public class Participant100M : Participant
        {
            public Participant100M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                Normat = 12;
            }
        }
        public class Participant500M : Participant
        {
            public Participant500M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                Normat = 90;
            }
        }
    }
}

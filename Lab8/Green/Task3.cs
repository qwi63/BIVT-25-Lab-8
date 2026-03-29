using System.Collections;
using System.Diagnostics.Metrics;

namespace Lab8.Green
{
    public class Task3
    {
        public class Student
        {
            // поля
            private string _name; // Устанавливаются в конструкторе,читаются через свойства
            private string _surname;
            private int[] _marks;
            private bool _status = false;
            private int _id;
            static int count;

            // свойства

            public string Name => _name;
            public string Surname => _surname;
            public int[] Marks => _marks.ToArray(); // получаем копию массива
            public bool IsExpelled => _status;
            public int ID => _id;
           

            public double AverageMark // возвращает среднее значение оценок студента
            {
                get
                {
                    int validMarksCount = 0;
                    double sum = 0;

                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] > 0) // если оценка есть, то суммируем значения и считаем кол-во
                        {
                            sum += _marks[i];
                            validMarksCount++;
                        }
                    }

                    if (validMarksCount == 0) // если оценок нет
                        return 0;

                    return sum / validMarksCount;
                }
            }
            static Student() // Статический конструктор в классе Student должен устанавливать поле для номера первого студенческого билета равным 1.
            {
                 count = 1;
            }

            public Student(string name, string surname) // конструктор, принимает 2 строковых поля
            {
                _name = name; // сохраняет переданное имя в ранее созданное поле _name
                _surname = surname;
                _marks = new int[3];
                _id = count;
                count++;// 3 экзамена сдают студенты, 3 места для оценок
            }
            public void Exam(int mark) // заменяет оценку по предмету
                                       // новой оценкой, если эта оценка выше «2». 
            {
                if (_status) return;// сразу проверим, не отчислился ли
                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        if (mark > 2) // если оценка больше 2, то заполняем в массив оценок _marks
                        {
                            _marks[i] = mark;
                            break;
                        }

                        if (mark == 2) // если оценка == 2, то отчислен
                        {
                            _status = true; // отчислен? да
                            _marks[i] = mark;
                            break;
                        }
                    }
                }
            }
            public static void SortByAverageMark(Student[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].AverageMark < array[j + 1].AverageMark)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }
            public void Restore() // меняет статус студента с “отчисленного” на “не отчисленного”.
            {
                if (_status)
                    _status = false;
            }
            public void Print() // для вывода информации о необходимых полях структуры
            {
                Console.WriteLine($"Name: {Name}");
                Console.WriteLine($"Surname: {Surname}");
                Console.WriteLine($"Marks: {Marks}");
                Console.WriteLine($"Average mark: {AverageMark}");
                Console.WriteLine($"Is expelled: {IsExpelled}");
            }
        }
        public class Commission
        {
            public static void Sort(Student[] students) // сортирует студентов по номеру их студ.билета
            {
                for (int i = 0; i < students.Length - 1; i++)
                {
                    for (int j = 0; j < students.Length - 1 - i; j++)
                    {
                        if (students[j].ID > students[j + 1].ID)
                        {
                            (students[j], students[j + 1]) = (students[j + 1], students[j]);
                        }
                    }
                }
            }
            public static Student[] Expel(ref Student[] students) // возвращает массив исключенных студентов
            {
                Student[] expelledStudents = students.Where(s => s.IsExpelled).ToArray();
                students = students.Where(s => !s.IsExpelled).ToArray();

                return expelledStudents;
            }
            public static void Restore(ref Student[] students, Student restored) // позволяет добавить студента в массив на место в соответствии с номером его студ.билета
            {
                
                bool flag = false; // будет сигнализировать, найден ли студент в массиве
                foreach (var student in students) // ищем студента в массиве
                {
                    if (student == restored) 
                    {
                        flag = true; // если студент найден
                        break; 
                    }
                }
                if (!flag) // если студент не найден, то добавляем его
                {
                    Array.Resize(ref students, students.Length + 1);
                    students[students.Length - 1] = restored;
                }
                Sort(students);
            }
        }
    }
}

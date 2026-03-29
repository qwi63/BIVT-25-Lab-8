using System.Diagnostics.Metrics;

namespace Lab8.Green
{
    public class Task3
    {
        public class Student
        {
            private string _name;
            private string _surname;
            private int[] _marks;
            private bool _status = false;
            private int _id;
            static int counter;

            public string Name => _name;
            public string Surname => _surname;
            public int ID => _id;
            public int[] Marks => _marks.ToArray();
            public bool IsExpelled => _status;

            public double AverageMark
            {
                get
                {
                    int validMarksCount = 0;
                    double sum = 0;

                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] > 0)
                        {
                            sum += _marks[i];
                            validMarksCount++;
                        }
                    }

                    if (validMarksCount == 0) 
                        return 0;

                    return sum / validMarksCount;
                }
            }

            static Student()
            {
                int counter = 1;
            }
            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[3];
                _id = counter;
                counter++;
            }

            public void Exam(int mark)
            {
                if (_status)
                    return;
                
                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        if(mark > 2)
                        {
                            _marks[i] = mark;
                            break;
                        }

                        if (mark == 2)
                        {
                            _status = true;
                            _marks[i] = mark;
                            break;
                        }
                    }
                }
            }

            public void Restore()
            {
                if (_status)
                    _status = false;
            }

            public static void SortByAverageMark(Student[] array)
            {
                Array.Sort(array, (a, b) => b.AverageMark.CompareTo(a.AverageMark));
            }

            public void Print()
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
            public static void Sort(Student[] students)
            {
                Array.Sort(students, (a, b) => a.ID.CompareTo(b.ID));
            }

            public static Student[] Expel(ref Student[] students)
            {
                Student[] expelledStudents = students.Where(s => s.IsExpelled).ToArray();
                students = students.Where(s => !s.IsExpelled).ToArray();
                
                return expelledStudents;
            }

            public static void Restore(ref Student[] students, Student restored)
            {
                bool flag = false;
                foreach (var student in students)
                {
                    if (student == restored)
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    Array.Resize(ref students,students.Length + 1);
                    students[students.Length - 1] = restored;
                }
                
                Sort(students);
            }
        }
    }
}

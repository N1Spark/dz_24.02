using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz_24._02
{
    class Program
    {
        public enum Type { Professor, AssociateProfessor, Lecturer }
        public enum Sex { Male, Female }
        public class University
        {
            public List<Faculty> Faculties { get; set; }
            public University() { }
            public University(List<Faculty> faculties) => Faculties = faculties;
        }
        public class Faculty
        {
            public string NameFaculty { get; set; }
            public List<Department> Departments { get; set; }
            public Faculty() { }
            public Faculty(string nameFaculty, List<Department> departments)
            {
                NameFaculty = nameFaculty;
                Departments = departments;
            }

        }
        public class Department
        {
            public string NameDepartment { get; set; }
            public Teachers HeadTeach { get; set; }
            public bool Profil { get; set; }
            public List<Group> Groups { get; set; }
            public List<Teachers> Teachers { get; set; }
            public Department()
            {
                NameDepartment = null;
                HeadTeach = null;
                Profil = false;
                Groups = null;
                Teachers = null;
            }
            public Department(string nameDepartment, Teachers head, bool isProfiling, List<Group> studentGroups, List<Teachers> teachers)
            {
                NameDepartment = nameDepartment;
                HeadTeach = head;
                Profil = isProfiling;
                Groups = studentGroups;
                Teachers = teachers;
            }
        }
        public class Human
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public Sex Sex { get; set; }
            public string Passport { get; set; }
            public string Address { get; set; }
            public Human()
            {
                Name = null;
                Surname = null;
                Passport = null;
                Address = null;
            }
            public Human(string name, string surname, Sex sex, string passport, string address)
            {
                Name = name;
                Surname = surname;
                Sex = sex;
                Passport = passport;
                Address = address;
            }
            public override string ToString()
            {
                return $"Name: {Name} {Surname}\nGender: {Sex}";
            }

        }
        public class Student : Human
        {
            public List<Parents> Parents { get; set; }
            public Group Group { get; set; }
            public Student() : base()
            {
                Parents = null;
                Group = null;
            }
            public Student(string name, string surname, Sex sex, string passport, string address, Group group, List<Parents> parents) : base(name, surname, sex, passport, address)
            {
                Group = group;
                Parents = parents;
            }
            public Student(string name, string surname, Sex sex, string passport, string address, Group group) : base(name, surname, sex, passport, address)
            {
                Group = group;
                Parents = null;
            }
            public override string ToString()
            {
                return base.ToString() + $"\nГруппа: {Group.NameGroup}";
            }
        }
        public class Teachers : Human
        {
            public Department Department { get; set; }
            public Type Type { get; set; }

            public Teachers() : base() => Department = null;
            public Teachers(string name, string surname, Sex sex, string passport, string address, Department department, Type position) : base(name, surname, sex, passport, address)
            {
                Department = department;
                Type = position;
            }
            public override string ToString()
            {
                return base.ToString() + $"\nFaculty: {Department.NameDepartment}\nHead: {Type}";
            }
        }
        public class Group
        {
            public string NameGroup { get; set; }
            public Student GroupLeader { get; set; }
            public Department Profile { get; set; }
            public Group()
            {
                NameGroup = null;
                GroupLeader = null;
                Profile = null;
            }
            public Group(string nameGroup, Department profile, Student groupLeader)
            {
                NameGroup = nameGroup;
                Profile = profile;
                GroupLeader = groupLeader;
            }
        }
        public class Parents : Human
        {
            public List<Student> Children { get; set; }
            public Parents() : base() => Children = null;
            public Parents(string name, string surname, Sex sex, string passport, string address, List<Student> children) : base(name, surname, sex, passport, address)
            {
                Children = children;
            }
            public override string ToString()
            {
                return base.ToString();
            }
        }
        public class Directory
        {
            public List<Student> Students { get; set; }
            public List<Teachers> Teachers { get; set; }
            public List<Parents> Parents { get; set; }
            public List<Faculty> Faculties { get; set; }
            public List<Student> listChildren { get; set; }

            public Directory()
            {
                Students = new List<Student>();
                Teachers = new List<Teachers>();
                Parents = new List<Parents>();
                Faculties = new List<Faculty>();
                listChildren = new List<Student>();
            }
            public List<Student> GetSortedStudents(string sortBy)
            {
                switch (sortBy)
                {
                    case "Name":
                        return Students.OrderBy(s => s.Name).ThenBy(s => s.Surname).ToList();
                    case "Faculties":
                        return Students.OrderBy(s => s.Group.Profile).ThenBy(s => s.Group.NameGroup).ToList();
                    case "Group":
                        return Students.OrderBy(s => s.Group.NameGroup).ThenBy(s => s.Surname).ThenBy(s => s.Name).ToList();
                    default:
                        return Students.OrderBy(s => s.Surname).ThenBy(s => s.Name).ToList();
                }
            }
            public List<Teachers> GetSortedTeachers(string sortBy)
            {
                switch (sortBy)
                {
                    case "Name":
                        return Teachers.OrderBy(t => t.Surname).ThenBy(t => t.Name).ToList();
                    case "Faculties":
                        return Teachers.OrderBy(t => t.Department.Profil).ThenBy(t => t.Department.NameDepartment).ThenBy(t => t.Surname).ThenBy(t => t.Name).ToList();
                    case "Department":
                        return Teachers.OrderBy(t => t.Department.NameDepartment).ThenBy(t => t.Surname).ThenBy(t => t.Name).ToList();
                    default:
                        return Teachers.OrderBy(t => t.Surname).ThenBy(t => t.Name).ToList();
                }
            }

            public List<Teachers> GetDepartmentHeads()
            {
                return Teachers.Where(t => t.Department.Profil).ToList();
            }
            public List<Student> GetChildrenOfParent(Parents parent)
            {
                return Students.Where(s => s.Parents.Contains(parent)).ToList();
            }
            public List<Student> GetTeachersWithStudent()
            {
                return listChildren.OrderBy(s => s.Name).ThenBy(s => s.Surname).ToList();
            }
        }

        static void Main(string[] args)
        {

        }
    }
}

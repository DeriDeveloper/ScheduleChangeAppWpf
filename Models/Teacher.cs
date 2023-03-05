using System.Collections.Generic;

namespace ScheduleChangeAppWpf.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
        public int Age { get; set; }
        public string NameInitials
        {
            get
            {
                return $"{Surname} {(Name.Length > 0 ? Name[0].ToString().ToUpper() : "")}. {(Patronymic.Length > 0 ? Patronymic[0].ToString().ToUpper() : "")}."; 
            }
        }
        public string FullName
        {
            get
            {
                return $"{Surname} {Name} {Patronymic}";
            }
        }

    }
}

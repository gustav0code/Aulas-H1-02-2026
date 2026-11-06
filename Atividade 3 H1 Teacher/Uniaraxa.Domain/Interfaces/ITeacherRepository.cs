using System;
using System.Collections.Generic;
using System.Text;
using Uniaraxa.Domain.Entities;

namespace Uniaraxa.Domain.Interfaces
{
    public interface ITeacherRepository 
    {
        public void AddTeacher(Teacher teacher);

        public void UpdateTeacher(Teacher teacher);

        public void DeleteTeacher(int id);

        public Teacher GetTeacherById(int id);
        public List<Teacher> GetAllTeachers();

    }
}
